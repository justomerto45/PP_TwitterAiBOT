﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;
using DeepAI;
using TwitterByAIBOT.Data;
using Microsoft.VisualBasic;

class Program
{
    static async Task Main()
    {
        var apiKey = "eeyY559HWRNz6W26MP4FHiloN";
        var apiSecretKey = "d2mkgnh4aWV2ZKjzlNcuoMa8Yklk9XBOdKAECdEFAqRPkUh7aa";
        var accessToken = "1654397663163424768-WUr1I8aiF51Yn5CYXK3qmxe6xBlGbZ";
        var accessTokenSecret = "XHXZprk1ou4H2ICOv9ouSuUrYkSXr39sCPRKdPqSW10sP";
        var userCredentials = new TwitterCredentials(apiKey, apiSecretKey, accessToken, accessTokenSecret);
        var userClient = new TwitterClient(userCredentials);

        // bearer token = "AAAAAAAAAAAAAAAAAAAAAH0HnQEAAAAAnUnJRDnmd1FB7gOrPe%2FY%2Fo%2BvJbM%3DvXZLgv686GBcqcY7iQCYlvpxqVQGupacyON2gtTk47Y7JdLvo2"

        TweetinviEvents.BeforeExecutingRequest += (sender, args) =>
        {
            System.Console.WriteLine(args.Url);
        };

        var credentials = new TwitterCredentials(apiKey, apiSecretKey, accessToken, accessTokenSecret);
        var client = new TwitterClient(credentials);

        TweetinviEvents.SubscribeToClientEvents(client);

        var authenticatedUser = await client.Users.GetAuthenticatedUserAsync();
        System.Console.WriteLine(authenticatedUser);

        var publishedTweets = await client.Tweets.PublishTweetAsync("Hello world");
        Console.WriteLine(publishedTweets);

        var tweet = await client.Tweets.PublishTweetAsync("Hello tweetinvi world!");
        System.Console.WriteLine("You published the tweet: " + tweet);
        System.Console.WriteLine(authenticatedUser);
        System.Console.ReadLine();

        var user = await userClient.Users.GetAuthenticatedUserAsync();
        Console.WriteLine(user);

        //while (true)
        //{
        //    Console.Write("Enter a topic to generate an image: ");
        //    var topic = Console.ReadLine();

        //    var imageGenerator = new ImageGenerator("d84c939d-a897-4b0a-a7e1-78a859905e8c");
        //    var imageUrl = imageGenerator.GenerateImage(topic);

        //    if (string.IsNullOrEmpty(imageUrl))
        //    {
        //        Console.WriteLine("Error: Unable to generate an image from the given topic.");
        //        continue;
        //    }

        //    var imagePath = "image.jpg";
        //    using var wc = new WebClient();
        //    wc.DownloadFile(imageUrl, imagePath);

        //    var media = await userClient.Upload.UploadTweetImageAsync(File.ReadAllBytes(imagePath));
        //    var tweetPic = await userClient.Tweets.PublishTweetAsync(new PublishTweetParameters
        //    {
        //        Text = $"Here's an image of {topic} generated by AI! #{topic.Replace(" ", "")}",
        //        Medias = new List<IMedia> { media }
        //    });

        //    Console.WriteLine($"Tweet posted with ID: {tweetPic.Id}");
        //}
    }
}