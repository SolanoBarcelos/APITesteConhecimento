using APITesteConhecimento.Interfaces;
using APITesteConhecimento.Repository;
using APITesteConhecimento.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(CoreContato.Models.Contato).Assembly);
builder.Services.AddAutoMapper(typeof(CoreContato.Mappings.ContatoProfile));


builder.Services.AddScoped<IContatoRepository, ContatoRepository>();
builder.Services.AddScoped<IContatoService, ContatoService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
{
    app.Urls.Add("http://0.0.0.0:7070");
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
