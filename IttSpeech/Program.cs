using IttSpeech.Agreement;
using AudioListener.Recording;
using AudioListener.Speech;
using IttSpeech.ApiModels;

// Creación de referencia en memoría para almacenar metadatos de la aplicación
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

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