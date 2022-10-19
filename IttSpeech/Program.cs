using IttSpeech.Agreement;
using AudioListener.Record;
using AudioListener.Speech;
using IttSpeech.ApiModels;

// Creación de referencia en memoría para almacenar metadatos de la aplicación

WebApplication app;
// Construcción del ecosistema de ejecución
{
    //Constructor de ecosistema
    var builder = WebApplication.CreateBuilder(args);
    // Agrega el servicio de controladores
    builder.Services.AddControllers();
    //Recuperación de secretos dependiente del entorno de ejecución
    if (builder.Environment.IsDevelopment())
    { /* Sacar los secretos del gestor interno */
       // FDASecretos FDASecretos = builder.Configuration.GetSection("FDASecretos").Get<FDASecretos>();
        //builder.Services.AddSingleton<FDASecretos>(FDASecretos);
    }
    else { /* Crear un json en la máquina de producción con los secretos */ }
    builder.Services.AddSingleton<InAppFileSaver>(new InAppFileSaver(builder.Environment));
    app = builder.Build();
    
}

// Punteros de servicios e intereses
{
    app.UseMiddleware<Contract>();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    
    app.Run();
}