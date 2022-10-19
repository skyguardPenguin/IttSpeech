using AudioListener.Speech;
using IttSpeech.ApiModels;
namespace IttSpeech.Controllers
{
    public class Controllers
    {
        private readonly InAppFileSaver _fileSaver;
        private Analizer _analizer;
        public Controllers(InAppFileSaver inAppFileSaver)
        {
            _fileSaver = inAppFileSaver;
            _analizer = new Analizer(false);
        }
        public async Task< Response> Analizar( IFormFile file)
        {
            Response response = new Response();

            //string result;
            //AudioListener.Record.IttSpeech s = new AudioListener.Record.IttSpeech();
            //result = s.Analize();
            //response.response = result;
            //return new Response() ;
            string path = await _fileSaver.Save(file, "files");
            string result = _analizer.Analize(path);
            response.result = "OK";
            response.response = result;
            return response;
        }
       


    }
}
