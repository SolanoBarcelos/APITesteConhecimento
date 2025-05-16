var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(CoreContato.Mappings.ContatoProfile).Assembly);
builder.Services.AddAutoMapper(typeof(CoreContato.Models.Contato).Assembly);
builder.Services.AddAutoMapper(typeof(CoreContato.Mappings.ContatoProfile));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
