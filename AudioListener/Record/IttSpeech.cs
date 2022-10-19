using AudioListener.Speech;
using AudioListener.Recording;
namespace AudioListener.Record
{
    public class IttSpeech
    {
        Recorder Recorder;
        Analizer Analizer;
        public IttSpeech()
        {
            Recorder = new Recorder(DateTime.Now.AddSeconds(3), DateTime.Now.AddSeconds(7), Directory.GetCurrentDirectory());
            Thread.Sleep(15000);
            Analizer = new Analizer(false);
        }

    
        public string Analize()
        {
            return Analizer.Analize("AudioListener/audio.wav");
        }
    }
}
