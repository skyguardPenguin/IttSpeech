using IttSpeech.Agreement;
using AudioListener.Record;
using AudioListener.Speech;
using IttSpeech.ApiModels;

// Creaci�n de referencia en memor�a para almacenar metadatos de la aplicaci�n

WebApplication app;
// Construcci�n del ecosistema de ejecuci�n
{
    //Constructor de ecosistema
    var builder = WebApplication.CreateBuilder(args);
    // Agrega el servicio de controladores
    builder.Services.AddControllers();
    //Recuperaci�n de secretos dependiente del entorno de ejecuci�n
    if (builder.Environment.IsDevelopment())
    { /* Sacar los secretos del gestor interno */
       // FDASecretos FDASecretos = builder.Configuration.GetSection("FDASecretos").Get<FDASecretos>();
        //builder.Services.AddSingleton<FDASecretos>(FDASecretos);
    }
    else { /* Crear un json en la m�quina de producci�n con los secretos */ }
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