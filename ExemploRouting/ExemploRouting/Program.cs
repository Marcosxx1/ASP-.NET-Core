var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Ativa roteamento
app.UseRouting();

// Criando endpoints
app.UseEndpoints(endpoints =>
{

});


app.Run();
