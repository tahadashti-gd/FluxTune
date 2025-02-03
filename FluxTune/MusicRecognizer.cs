using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace FluxTune
{
    public class MusicRecognizer
    {
        private readonly static string ApiToken = "audd.io API token";
        private static string chanelLink = "Your channel Link";
        private static string chanelName = "Your channel Name";


        private static readonly HttpClient client = new HttpClient();

        public static async Task<string> RecognizeMusicFromURL(string fileUrl, string localFilePath)
        {
            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("api_token", ApiToken),
                    new KeyValuePair<string, string>("url", fileUrl)
                });

                var response = await client.PostAsync("https://api.audd.io/", content);
                var responseString = await response.Content.ReadAsStringAsync();
                var jsonResponse = JObject.Parse(responseString);

                bool isRecognized = jsonResponse["result"] != null;

                if (jsonResponse["status"]?.ToString() == "success" && jsonResponse["result"].ToString() != "")
                {
                    return FormatCaption(
                        jsonResponse["result"]["title"]?.ToString(),
                        jsonResponse["result"]["artist"]?.ToString(),
                        jsonResponse["result"]["album"]?.ToString(),
                        jsonResponse["result"]["song_link"]?.ToString()
                    );
                }
                else
                {
                    Console.WriteLine("Downloading music");
                    try
                    {
                        using (var dresponse = await client.GetAsync(fileUrl))
                        {
                            response.EnsureSuccessStatusCode();
                            byte[] fileBytes = await dresponse.Content.ReadAsByteArrayAsync();
                            Directory.CreateDirectory("DownloadedMusic");
                            await System.IO.File.WriteAllBytesAsync(localFilePath, fileBytes);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error downloading file: {ex.Message}");
                    }
                    return ExtractMetadata(localFilePath);
                }
            }
        }
        private static string ExtractMetadata(string filePath)
        {
            try
            {
                var file = TagLib.File.Create(filePath);
                string title = file.Tag.Title ?? "Unknown Title";
                string artist = file.Tag.Performers.Length > 0 ? file.Tag.Performers[0] : "Unknown Artist";
                string album = file.Tag.Album ?? "Unknown Album";
                string duration = file.Properties.Duration.ToString(@"mm\:ss");

                return FormatCaption(title, artist, album, null);
            }
            catch (Exception ex)
            {
                return $"Unable to extract metadata: {ex.Message}";
            }
        }
        static string EscapeMarkdownV2(string text)
        {
            string pattern = @"([_*\[\]()~`>#\+\-=|{}.!])";
            return Regex.Replace(text, pattern, @"\_");
        }
        static string ConvertToHashtag(string text)
        {
            return @"\#" + text.Replace(" ", @"\_");
        }
        private static string FormatCaption(string title, string artist, string album, string songLink)
        {
            return $"""
            🎵 *Track:* {EscapeMarkdownV2(title)}
            🎤 *Artist:* {ConvertToHashtag(artist)}
            💿 *Album:* {EscapeMarkdownV2(album)}
            ✨ *Vibe: *

            👤 *Admin:* @{EscapeMarkdownV2(MainCore.adminID)}
            
            {(songLink != null ? $"🔗 [Music page]({songLink})" : "")}


            📢 [{chanelName}]({chanelLink})
            """;
        }
    }
}
