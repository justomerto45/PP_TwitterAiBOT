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
        private TwitterClient client;
        private string accessToken;
        private string accessTokenSecret;

        public TwitterBot(string apiKey, string apiSecretKey, string accessToken, string accessTokenSecret)
        {
            this.accessToken = accessToken;
            this.accessTokenSecret = accessTokenSecret;

            var userCredentials = new TwitterCredentials(apiKey, apiSecretKey, accessToken, accessTokenSecret);
            this.client = new TwitterClient(userCredentials);
        }

        public async Task PostImageTweet(string message, string imagePath, string hashtag)
        {
            var media = await this.client.Upload.UploadTweetImageAsync(File.ReadAllBytes(imagePath));
            var tweet = await client.Tweets.PublishTweetAsync(new PublishTweetParameters
            {
                Text = message,
                Medias = new List<IMedia> { media }
            });

            Console.WriteLine($"Tweet posted with ID: {tweet.Id}");
        }

        // zusatzfunktion, reply with ai generated pic

        //public async Task ReplyToMention(ITweet mention, string message)
        //{
        //    var mentionText = mention.FullText;

        //    var username = mention.CreatedBy.ScreenName;

        //    // tweetid of mention
        //    var tweetId = mention.Id;

        //    var reply = await this.client.Tweets.PublishTweetAsync(new PublishTweetParameters
        //    {
        //        Text = message + " @" + username,
        //        InReplyToTweetId = tweetId
        //    });

        //    Console.WriteLine($"Reply posted with ID: {reply.Id}");
        //}
    }
}