using AudioListener.Recording;
using Syn.Logging;
using Syn.Speech.Api;
using AudioListener.Syntactic;
namespace AudioListener.Speech
{
    public class Analizer
    {
        private Configuration _configuration;
        private StreamSpeechRecognizer _speechRecognizer;
        private string _modelsDirectory;
        Recorder _recorder;

        public Analizer(bool withGrammar)
        {
            _modelsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Models");
            if (!withGrammar)
                _configuration = new Configuration
                {
                    AcousticModelPath = _modelsDirectory,
                    DictionaryPath = Path.Combine(_modelsDirectory, "cmudict-en-us.dict"),
                    LanguageModelPath = Path.Combine(_modelsDirectory, "en-us.lm.dmp"),
                };
            else
                _configuration = new Configuration
                {
                    AcousticModelPath = _modelsDirectory,
                    DictionaryPath = Path.Combine(_modelsDirectory, "cmudict-en-us.dict"),
                    LanguageModelPath = Path.Combine(_modelsDirectory, "en-us.lm.dmp"),
                    UseGrammar = true,
                    GrammarPath = @"C:\Users\sinoa\source\repos\IttSpeech\Grammars",
                    GrammarName= "g1"
                };
           
            _speechRecognizer = new StreamSpeechRecognizer(_configuration);
    
            //Logger.LogReceived += Logger_LogReceived;


            
        }

        public async Task<string> Analize(string path)
        {
           
            string value = "";
            _speechRecognizer.StartRecognition(new FileStream(path, FileMode.Open));

            var result = _speechRecognizer.GetResult();
            _speechRecognizer.StopRecognition();
            if (result != null )
            {
                Console.WriteLine("Speech Recognized: " + result.GetHypothesis());
                value += result.GetHypothesis() == "" ? ";" :result.GetHypothesis() ;
                value = await SyntacticAnalizer.AnalizeEntry("localhost", 4989, value);
            }
            return value;
        }
        static void Logger_LogReceived(object sender, LogReceivedEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

    }
}
