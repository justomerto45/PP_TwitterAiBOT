using Newtonsoft.Json.Linq; // Importieren der Newtonsoft.Json.Linq-Bibliothek zum Arbeiten mit JSON-Daten
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
        private readonly string apiKey = "eeyY559HWRNz6W26MP4FHiloN"; // API-Schlüssel für Twitter-Zugriff
        private readonly string apiSecretKey = "d2mkgnh4aWV2ZKjzlNcuoMa8Yklk9XBOdKAECdEFAqRPkUh7aa"; // API-Geheimschlüssel für Twitter-Zugriff
        private readonly string accessToken = "1654397663163424768-WUr1I8aiF51Yn5CYXK3qmxe6xBlGbZ"; // Zugriffstoken für Twitter-API
        private readonly string accessTokenSecret = "XHXZprk1ou4H2ICOv9ouSuUrYkSXr39sCPRKdPqSW10sP"; // Geheimtoken für Twitter-API

        private readonly TwitterClient userClient; // Twitter-Client für den Benutzer
        private readonly ImageGenerator imageGenerator; // Bildgenerator für KI-generierte Bilder
        private string generatedImageUrl; // Speichert die generierte Bild-URL

        public MainWindow()
        {
            InitializeComponent();

            var userCredentials = new TwitterCredentials(apiKey, apiSecretKey, accessToken, accessTokenSecret); // Erstellen der Twitter-Anmeldeinformationen mit den Zugriffsdaten
            userClient = new TwitterClient(userCredentials); // Erstellen des Twitter-Clients für den Benutzer
            imageGenerator = new ImageGenerator("d84c939d-a897-4b0a-a7e1-78a859905e8c"); // Erstellen des Bildgenerators mit dem API-Schlüssel
        }

        private async void GenerateImageBtn_Click(object sender, RoutedEventArgs e)
        {
            string topic = TopicTextBox.Text; // Den Text aus dem TopicTextBox-Feld abrufen

            // Die Fortschrittsleiste wird angezeigt, wenn der Button geklickt wird
            ImageProgressBar.Visibility = Visibility.Visible;

            await Task.Run(() =>
            {
                generatedImageUrl = imageGenerator.GenerateImage(topic); // Generieren eines Bildes mit dem angegebenen Topic
            });

            // Die Fortschrittsleiste wird ausgeblendet
            ImageProgressBar.Visibility = Visibility.Collapsed;

            if (!string.IsNullOrEmpty(generatedImageUrl))
            {
                using (WebClient webClient = new WebClient())
                {
                    string imagePath = $"{topic}.jpg"; // Pfad zum Speichern des heruntergeladenen Bildes
                    webClient.DownloadFile(generatedImageUrl, imagePath); // Herunterladen des generierten Bildes von der URL

                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.UriSource = new Uri(Path.GetFullPath(imagePath)); // Laden des Bildes in eine BitmapImage
                    bitmapImage.EndInit();

                    GeneratedImage.Source = bitmapImage; // Anzeigen des generierten Bildes in der Benutzeroberfläche
                }

                TweetBtn.IsEnabled = true; // Aktivieren des Tweet-Buttons
                TweetTextBox.IsEnabled = true; // Aktivieren des Tweet-Textfelds

            }
            else
            {
                MessageBox.Show("Error: Unable to generate an image from the given topic."); // Fehlermeldung, falls kein Bild generiert werden kann
            }

        }


        private async void TweetBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(generatedImageUrl))
            {
                MessageBox.Show("Error: No image available to tweet."); // Fehlermeldung, falls kein Bild zum Tweeten verfügbar ist
                return;
            }

            var imagePath = $"tweet_image_{generatedImageUrl}.jpg"; // Pfad zum Speichern des heruntergeladenen Bildes für den Tweet
            using (var wc = new WebClient())
            {
                wc.DownloadFile(generatedImageUrl, imagePath); // Herunterladen des Bildes für den Tweet
            }

            var media = await userClient.Upload.UploadTweetImageAsync(File.ReadAllBytes(imagePath)); // Hochladen des Bildes als Tweet-Medien
            var tweet = await userClient.Tweets.PublishTweetAsync(new PublishTweetParameters
            {
                Text = "Check out this AI-generated image!", // Text des Tweets
                Medias = new List<IMedia> { media } // Hinzufügen des Bildes als Medien zum Tweet
            });

            MessageBox.Show("Tweet posted successfully!"); // Erfolgsmeldung nach erfolgreichem Tweet
        }
    }
}
