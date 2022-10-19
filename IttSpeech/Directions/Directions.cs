using Microsoft.AspNetCore.Mvc;
using IttSpeech.Controllers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using IttSpeech.ApiModels;
using AudioListener.Speech;

namespace IttSpeech.Directions
{
    [ApiController]
    public class Directions : ControllerBase
    {
        private readonly InAppFileSaver _fileSaver;
        private Analizer _analizer;
        private Controllers.Controllers _controller;
        public Directions(InAppFileSaver saver)
        {
            this._fileSaver = saver;
            _analizer = new Analizer(false);
            _controller = new Controllers.Controllers(_fileSaver);
        }
        [Route("/prueba"), HttpGet]
        public IActionResult Prueba()
        {
            return Ok("XD");
        }

        [Route("/analizar"), HttpPost]
        public async Task<IActionResult> Analizar([FromForm] IFormFile file)
        {
            return Ok(JsonConvert.SerializeObject( await _controller.Analizar(file)));
        }

        [Route("/recibir"), HttpPost]
        public async Task<IActionResult> Recibir([FromForm] IFormFile file)
        {
            string path = await _fileSaver.Save(file, "files");
            string result= _analizer.Analize(path);
            return Ok(result);
        }

    }
}
