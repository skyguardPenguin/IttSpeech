using IttSpeech.Agreement;
using AudioListener.Recording;
using AudioListener.Speech;
using IttSpeech.ApiModels;

// Creaci�n de referencia en memor�a para almacenar metadatos de la aplicaci�n
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

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
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: MyAllowSpecificOrigins,
                          policy =>
                          {
                              policy.WithOrigins("http://127.0.0.1:5501",
                                                  "*");
                          });
    });
    app = builder.Build();
    
}

// Punteros de servicios e intereses
{
    app.UseCors(MyAllowSpecificOrigins);
    app.UseMiddleware<Contract>();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    
    app.Run();
}