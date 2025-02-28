using OfficeOpenXml;

var builder = WebApplication.CreateBuilder(args);

// Definir o contexto de licença do EPPlus
ExcelPackage.LicenseContext = LicenseContext.Commercial;

// Adiciona CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFlutter", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Adiciona os serviços de controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Habilita o Swagger somente no ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("PermitirFlutter"); // Ativa o CORS

app.UseAuthorization();
app.MapControllers();

app.Run();
