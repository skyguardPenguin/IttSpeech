using Microsoft.AspNetCore.Http;

using System.Text.Json;

namespace IttSpeech.Agreement
{
    /// <summary>
    ///     Gestiona un intermediario entre el hilo de petición, el proceso del servidor y el hilo de réplica
    /// </summary>
    public class Contract
    {
        RequestDelegate Delegador;
        /// <summary>
        ///     Constructor del mediador
        /// </summary>
        /// <param name="_delegador"> Recibe un delegador contextual HTTP </param>
        public Contract(RequestDelegate _delegador)
        {
            Delegador = _delegador;
        }
        /// <summary>
        ///     👾Maneja los saltos entre hilos de peticiones
        /// </summary>
        /// <param name="_contexto"> Recibe un contexto de petición HTTP </param>
        /// <returns> Regresa una acción de salto entre peticiones HTTP </returns>
        public async Task Invoke(HttpContext _contexto)
        {
            _contexto.Response.OnStarting(() =>
            {
                _contexto.Response.ContentType = "application/json";

                return Task.CompletedTask;
            });
            await Delegador(_contexto);
        }
    }
}
