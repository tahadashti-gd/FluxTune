using Newtonsoft.Json.Linq;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace FluxTune
{
    public class MainCore
    {
        public static string adminID;
        private string chanelID = "Your channel ID";
        string BotToken = "Your bot token";
        private static readonly HttpClient client = new HttpClient();

        TelegramBotClient bot = new TelegramBotClient("Your bot token");
        public void StartBot()
        {
            bot.OnError += OnError;
            bot.OnMessage += OnMessage;
        }
        async Task OnError(Exception exception, HandleErrorSource source)
        {
            Console.WriteLine(exception);
        }
        async Task OnMessage(Message msg, UpdateType type)
        {
            if (msg.Type == MessageType.Audio || msg.Type == MessageType.Voice)
            {
                adminID = msg.Chat.Username;
                var fileId = msg.Audio.FileId;
                var fileInfo = await bot.GetFileAsync(fileId);
                var filePath = fileInfo.FilePath;
                var fileUrl = $"https://api.telegram.org/file/bot{BotToken}/{filePath}";
                string localFilePath = Path.Combine("DownloadedMusic", $"{fileId}.mp3");
               
                string result = await MusicRecognizer.RecognizeMusicFromURL(fileUrl, localFilePath);

                await bot.SendAudio(
                    chatId: chanelID,
                    audio: fileId,
                    caption: result,
                    parseMode: ParseMode.MarkdownV2
                );
            }
            else
            {
                await bot.SendMessage(msg.Chat.Id, "Unsupported format");
            }
        }
    }
}
