using MedLog_TelegramBot.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TelegramController : ControllerBase
{
    private readonly TelegramService _telegramService;
    private readonly ILogger<TelegramController> _logger;

    public TelegramController(TelegramService telegramService, ILogger<TelegramController> logger)
    {
        _telegramService = telegramService;
        _logger = logger;
    }

    [HttpPost("sendLocation")]
    public async Task<IActionResult> SendLocation([FromQuery] long chatId, [FromQuery] double latitude, [FromQuery] double longitude)
    {
        try
        {
            _logger.LogInformation($"Sending location to chat ID {chatId} with coordinates ({latitude}, {longitude})");
            await _telegramService.SendLocationAsync(chatId, latitude, longitude);
            _logger.LogInformation("Location sent successfully.");
            return Ok("Location sent successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error sending location: {ex.Message}");
            return BadRequest($"Error sending location: {ex.Message}");
        }
    }

    [HttpPost("sendTestMessage")]
    public async Task<IActionResult> SendTextMessage([FromQuery] long chatId, [FromQuery] string message)
    {
        try
        {
            _logger.LogInformation($"Sending test message to chat ID {chatId} with message: {message}");
            await _telegramService.SendTextMessageAsync(chatId, message);
            _logger.LogInformation("Test message sent successfully.");
            return Ok("Test message sent successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error sending test message: {ex.Message}");
            return BadRequest($"Error sending test message: {ex.Message}");
        }
    }
}
