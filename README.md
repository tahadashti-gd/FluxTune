## **Music Recognition Telegram Bot**

### **About**
This Telegram bot is designed to recognize and process music files sent to it by users. The bot can extract detailed information about a song, including its title, artist, album, and duration, using advanced music recognition APIs and databases. It then formats this information into a structured message and forwards it to a specific Telegram channel where the bot is an admin.

### **Features**
1. **Music File Recognition**: 
   - The bot allows users to send music files (e.g., MP3, audio clips) to the bot.
   - The bot then uses the `Audd.io API` to recognize the song based on its content.

2. **Information Extraction**: 
   - If the song is recognized, the bot retrieves the following information:
     - **Title**: The name of the song.
     - **Artist**: The name of the artist who performed the song.
     - **Album**: The album to which the song belongs.
     - **Release Date**: The date the song or album was released.
     - **Duration**: The duration of the song (in timecode format).
     - **Song Link**: A direct link to the song on a platform such as Spotify or Apple Music.

3. **Custom Caption Creation**:
   - After recognizing the song, the bot formats the information into a **Telegram-friendly caption**:
     - The song title is hyperlinked.
     - The artist’s name is displayed, and if necessary, hashtags are added for better social media compatibility.
     - The album name and duration are also included in the caption.

4. **Message Forwarding**:
   - The bot forwards the formatted message, along with the song's original link, to a specific channel where it is an admin.
   - The forwarded message includes an admin mention, indicating who is managing the content.

5. **Fallback for Unknown Songs**:
   - If a song cannot be recognized via the API, the bot retrieves basic information directly from the audio file (such as title, artist, and album) and constructs a similar caption.


### **Technologies Used**
- **C#**: The bot is built using C# as the main programming language.
- **Telegram Bot API**: The bot interacts with the Telegram Bot API to send and receive messages.
- **Audd.io API**: This API is used for music recognition to fetch song details based on the audio file sent by the user.

### **Installation**

#### **Prerequisites**:
- Make sure you have a **Telegram bot token** from @BotFather.
- Install **.NET SDK** (version 5.0 or higher).
- Make sure you have access to the **Audd.io API** for music recognition.

#### **Steps**:
1. Clone this repository:
   ```bash
   git clone https://github.com/tahadashti-gd/ّFluxTune.git
   cd FluxTune
   ```

2. Install dependencies via NuGet:
   - Install `Telegram.Bot`:
     ```bash
     dotnet add package Telegram.Bot
     ```
   - Install `Newtonsoft.Json`:
     ```bash
     dotnet add package Newtonsoft.Json
     ```

3. Configure your bot:
   - Add your Telegram Bot API token to the `appsettings.json` or in your environment variables.
   - Configure the Audd.io API token in the code where the `MusicRecognizer` class makes API requests.

4. Run the bot:
   ```bash
   dotnet run
   ```

5. Send a music file to the bot to see it in action!

### **Usage**
1. **Send Music**: Users can send an audio file (MP3, or any supported format) to the bot.
2. **Bot Recognition**: The bot uses the `Audd.io` API to recognize the song.
3. **Song Information**: After the song is recognized, it will send back a message with detailed song information (title, artist, album, timecode, etc.) and forward it to the designated channel with a caption.
4. **Channel Admin**: A mention of the admin who manages the content is included in the forwarded message.

### **Bot Flow**
1. User sends an audio file to the bot.
2. The bot processes the file using the music recognition API.
3. If the song is recognized:
   - A formatted caption with song details is created.
   - The message is forwarded to a predefined Telegram channel where the bot is an admin.
4. If the song is not recognized:
   - Basic song details (like file name, artist, etc.) are extracted directly from the file.
   - The bot constructs a message based on the available information and forwards it.

---

### **Contact**
If you have any questions or need help with the bot, feel free to reach out to [@TahaDashti_GD](https://t.me/TahaDashti_GD).
