using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;
using DeepAI;
using TwitterByAIBOT.Data;
using Tweetinvi.Core.DTO;

namespace TwitterAiBotWPF
{
    public partial class MainWindow : Window
    {
        private readonly string apiKey = "eeyY559HWRNz6W26MP4FHiloN";
        private readonly string apiSecretKey = "d2mkgnh4aWV2ZKjzlNcuoMa8Yklk9XBOdKAECdEFAqRPkUh7aa";
        private readonly string accessToken = "1654397663163424768-WUr1I8aiF51Yn5CYXK3qmxe6xBlGbZ";
        private readonly string accessTokenSecret = "XHXZprk1ou4H2ICOv9ouSuUrYkSXr39sCPRKdPqSW10sP";

        private readonly TwitterClient userClient;
        private readonly ImageGenerator imageGenerator;
        private string generatedImageUrl;

        public MainWindow()
        {
            InitializeComponent();

            var userCredentials = new TwitterCredentials(apiKey, apiSecretKey, accessToken, accessTokenSecret);
            userClient = new TwitterClient(userCredentials);
            imageGenerator = new ImageGenerator("d84c939d-a897-4b0a-a7e1-78a859905e8c");
        }

        private async void GenerateImageBtn_Click(object sender, RoutedEventArgs e)
        {
            string topic = TopicTextBox.Text;

            // progress bar gets shown when button clicked
            ImageProgressBar.Visibility = Visibility.Visible;

            await Task.Run(() =>
            {
                generatedImageUrl = imageGenerator.GenerateImage(topic);
            });

            // hide the progress bar
            ImageProgressBar.Visibility = Visibility.Collapsed;

            if (!string.IsNullOrEmpty(generatedImageUrl))
            {
                using (WebClient webClient = new WebClient())
                {
                    string imagePath = $"{topic}.jpg";
                    webClient.DownloadFile(generatedImageUrl, imagePath);

                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.UriSource = new Uri(Path.GetFullPath(imagePath));
                    bitmapImage.EndInit();

                    GeneratedImage.Source = bitmapImage;
                }

                TweetBtn.IsEnabled = true;
                TweetTextBox.IsEnabled = true;

            }
            else
            {
                MessageBox.Show("Error: Unable to generate an image from the given topic.");
            }

        }


        private async void TweetBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(generatedImageUrl))
            {
                MessageBox.Show("Error: No image available to tweet.");
                return;
            }

            var imagePath = $"tweet_image_{generatedImageUrl}.jpg";
            using (var wc = new WebClient())
            {
                wc.DownloadFile(generatedImageUrl, imagePath);
            }

            var media = await userClient.Upload.UploadTweetImageAsync(File.ReadAllBytes(imagePath));
            var tweet = await userClient.Tweets.PublishTweetAsync(new PublishTweetParameters
            {
                Text = "Check out this AI-generated image!",
                Medias = new List<IMedia> { media }
            });

            MessageBox.Show("Tweet posted successfully!");
        }
    }
}
