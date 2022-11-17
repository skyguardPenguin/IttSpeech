using NAudio.Wave;

namespace AudioListener.Recording
{
    public class Recorder :IDisposable
    {

        private WaveFileWriter Writer;
        private WaveInEvent WaveIn;
        public string OutputFolder { get; set; }
        public string OutputFilePath { get; set; }
        public string FileName { get; set; }

        public DateTime Start;
        public DateTime End;
        private bool disposedValue;

        public Recorder()
        {
            FileName = "audio.wav";
            OutputFilePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            OutputFolder = "AudioListener";
        }

        public Recorder(DateTime start,DateTime end):this()
        {
            Start = start;
            End = end;
            Iniciar();
            
       
        }
        public Recorder(DateTime start, DateTime end, string outputFilePath)
        {
            FileName = "audio.wav";
            OutputFilePath =outputFilePath;
            OutputFolder = "AudioListener";
            Start = start;
            End = end;
            
            Iniciar();

        }
        public Recorder(DateTime start, DateTime end, string outputFilePath,string fileName)
        {
            FileName = fileName;
            OutputFilePath = outputFilePath;
            OutputFolder = "AudioListener";
            Start = start;
            End = end;

            Iniciar();

        }
        public void Iniciar()
        {
            OutputFolder = Path.Combine(OutputFilePath, OutputFolder);
            Directory.CreateDirectory(OutputFolder);
            OutputFilePath = Path.Combine(OutputFolder, FileName);
            WaveIn = new WaveInEvent();



            Writer = null;
            WaveIn.WaveFormat = new WaveFormat(16000, 1);
            WaveIn.DeviceNumber = 0;
            Writer = new WaveFileWriter(OutputFilePath, WaveIn.WaveFormat);
            WaveIn.StartRecording();

            WaveIn.DataAvailable += (s, a) =>
            {
                
                if (DateTime.Now < Start) return;
                else if (DateTime.Now > End) Detener();
                Console.WriteLine("Listening at " + DateTime.Now);
              
                Writer.Write(a.Buffer, 0, a.BytesRecorded);
                if (Writer.Position > WaveIn.WaveFormat.AverageBytesPerSecond * 30)
                {
                    WaveIn.StopRecording();
                }
            };
            WaveIn.RecordingStopped += (s, a) =>
            {
                Writer?.Dispose();
                Writer = null;



            };

        }
        public void Detener()
        {
            WaveIn.StopRecording();
            this.Dispose();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: eliminar el estado administrado (objetos administrados)
                    Writer.Dispose();
                    WaveIn.Dispose();
                }

                // TODO: liberar los recursos no administrados (objetos no administrados) y reemplazar el finalizador
                // TODO: establecer los campos grandes como NULL
                disposedValue = true;
            }
        }

        // // TODO: reemplazar el finalizador solo si "Dispose(bool disposing)" tiene código para liberar los recursos no administrados
        // ~Recorder()
        // {
        //     // No cambie este código. Coloque el código de limpieza en el método "Dispose(bool disposing)".
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en el método "Dispose(bool disposing)".
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}