using Recipe.Gateway;
using Recipe.Gateway.Client;
using Recipe.Gateway.Extensions.AutoMapper;
using Recipe.Gateway.Extensions.Business;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();


builder.Services.AddHttpClient<IRecipeClient, RecipeClient>((_, client) =>
{
    var settings = builder.Configuration.GetSection("ServiceSettings").Get<ClientSettings>();//ClientSettings, RecipeApiSettings.cs'deki class'ın ismi. Yani isimleri yanlış vermişim. RecipeApiSettings ismi aslında ClientSettings olacak ama şimdilik değiştirmedim hata almayayım diye.
    client.BaseAddress = new Uri(settings.RecipeServiceUrl);
});

builder.Services.AddScoped<ClientSettings>();

builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddAutoMapper(typeof(Program));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddTransient<IRecipeClient, RecipeClient>();
builder.Services.AddTransient<IRecipeService, RecipeService>();

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