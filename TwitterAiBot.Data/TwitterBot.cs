using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace TwitterByAIBOT.Data
{
    public class TwitterBot
    {
        private TwitterClient client; // Twitter-Client-Instanz
        private string accessToken; // Zugriffstoken für die Authentifizierung
        private string accessTokenSecret; // Zugriffstoken-Geheimcode für die Authentifizierung

        public TwitterBot(string apiKey, string apiSecretKey, string accessToken, string accessTokenSecret)
        {
            this.accessToken = accessToken;
            this.accessTokenSecret = accessTokenSecret;

            // Erstellen von Benutzeranmeldeinformationen mit den bereitgestellten API- und Zugriffstoken-Anmeldeinformationen
            var userCredentials = new TwitterCredentials(apiKey, apiSecretKey, accessToken, accessTokenSecret);

            // Initialisieren des Twitter-Clients mit den Benutzeranmeldeinformationen
            this.client = new TwitterClient(userCredentials);
        }

        public async Task PostImageTweet(string message, string imagePath, string hashtag)
        {
            // Lesen der Bilddatei als Bytes
            var media = await this.client.Upload.UploadTweetImageAsync(File.ReadAllBytes(imagePath));

            // Erstellen eines Tweets mit der bereitgestellten Nachricht und dem angehängten Bild
            var tweet = await client.Tweets.PublishTweetAsync(new PublishTweetParameters
            {
                Text = message,
                Medias = new List<IMedia> { media }
            });

            // Ausgabe der ID des geposteten Tweets
            Console.WriteLine($"Tweet mit ID {tweet.Id} gepostet.");
        }

        // zusatzfunktion, antworte mit KI-generiertem Bild

        //public async Task ReplyToMention(ITweet mention, string message)
        //{
        //    var mentionText = mention.FullText;
        //    var username = mention.CreatedBy.ScreenName;

        //    // Tweet-ID der Erwähnung
        //    var tweetId = mention.Id;

        //    // Erstellen eines Antwort-Tweets mit der bereitgestellten Nachricht und dem Benutzernamen der Erwähnung
        //    var reply = await this.client.Tweets.PublishTweetAsync(new PublishTweetParameters
        //    {
        //        Text = message + " @" + username,
        //        InReplyToTweetId = tweetId
        //    });

        //    // Ausgabe der ID des geposteten Antwort-Tweets
        //    Console.WriteLine($"Antwort mit ID {reply.Id} gepostet.");
        //}
    }
}
