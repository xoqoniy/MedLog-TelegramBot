using MedLog_TelegramBot.Services;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add user secrets

builder.Services.AddScoped<TelegramService>();
// Retrieve the token from configuration
builder.Configuration.AddUserSecrets<Program>();

var botConfig = builder.Configuration.GetSection("BotConfiguration");
var telegramBotToken = botConfig["Token"];
// Register the Telegram Bot Client
builder.Services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(telegramBotToken));


//Adding CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("TelegramBotCors", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
