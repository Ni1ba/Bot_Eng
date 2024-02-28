using Bot_Eng;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

using CancellationTokenSource cts = new();
BOT bot = new BOT();
bot.Start();
Console.WriteLine(bot.ConfigJson["token"]);
var botClient = new TelegramBotClient(bot.ConfigJson["token"]);


ReceiverOptions receiverOptions = new()
{
    AllowedUpdates = Array.Empty<UpdateType>() 
};

botClient.StartReceiving(
    updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);

var me = await botClient.GetMeAsync();
Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();

cts.Cancel();
// пока забить на него можно
//метод который ошибки ловит со стороны телеги 
Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    var ErrorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
    };

    Console.WriteLine(ErrorMessage);
    return Task.CompletedTask;
}
//всё что выше - просто принять как оно есть и не трогать 



//основная метод для обработки событий
async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    // какая то проверка
    if (update.Message is not { } message)
        return;
    if (message.Text is not { } messageText)
        return;

    
    var chatId = message.Chat.Id;
    //Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

    // using Telegram.Bot.Types.ReplyMarkups;

    ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
    {
    new KeyboardButton[] { "Help me", "Call me ☎️" },
})
    {
        ResizeKeyboard = true
    };

    Message sentMessage = await botClient.SendTextMessageAsync(
        chatId: chatId,
        text: "Choose a response",
        replyMarkup: replyKeyboardMarkup,
        cancellationToken: cancellationToken);





}






