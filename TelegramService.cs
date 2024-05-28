using Telegram.Bot;

public class TelegramService
{
    private readonly ITelegramBotClient _botClient;
    private readonly ILogger<TelegramService> _logger;

    public TelegramService(ITelegramBotClient botClient, ILogger<TelegramService> logger)
    {
        _botClient = botClient;
        _logger = logger;
    }

    public async Task SendLocationAsync(long chatId, double latitude, double longitude)
    {
        try
        {
            await _botClient.SendLocationAsync(chatId, latitude, longitude);
            _logger.LogInformation($"Location sent to chat ID {chatId}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error sending location to chat ID {chatId}: {ex.Message}");
        }
    }

    public async Task SendTestMessageAsync(long chatId, string message)
    {
        try
        {
            await _botClient.SendTextMessageAsync(chatId, message);
            _logger.LogInformation($"Test message sent to chat ID {chatId}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error sending test message to chat ID {chatId}: {ex.Message}");
        }
    }
}
