# Twitter AI Bot

![Twitter AI Bot](https://example.com/twitter-ai-bot.png)

Twitter AI Bot is a Windows Presentation Foundation (WPF) application that generates and tweets AI-generated images based on user-provided topics. It utilizes various APIs and libraries, including Tweetinvi, DeepAI, and Newtonsoft.Json.

## Features

- Generate AI-generated images based on user-provided topics.
- Display the generated image in the application.
- Tweet the generated image along with a predefined text to share it on Twitter.

## Prerequisites

Before running the application, make sure you have the following:

- Twitter API credentials: `apiKey`, `apiSecretKey`, `accessToken`, `accessTokenSecret`.
- DeepAI API key.

## Setup

1. Clone the repository: `git clone https://github.com/your-username/twitter-ai-bot.git`
2. Open the solution in Visual Studio.
3. In the `MainWindow.xaml.cs` file, replace the placeholders for the Twitter API credentials (`apiKey`, `apiSecretKey`, `accessToken`, `accessTokenSecret`) with your own credentials.
4. Replace the DeepAI API key placeholder with your own key.
5. Build and run the application.

## Usage

1. Enter a topic in the text box.
2. Click the "Generate Image" button.
3. The progress bar will be shown while the AI generates the image.
4. Once the image is generated, it will be displayed in the application.
5. If the image generation is successful, the "Tweet" button and the tweet text box will be enabled.
6. Click the "Tweet" button to post the image on Twitter with a predefined text.

## Reporting Issues

If you encounter any issues or have suggestions for improvements, please create an issue in the [GitHub repository](https://github.com/your-username/twitter-ai-bot/issues). We appreciate your feedback!

## License

This project is licensed under the [MIT License](https://opensource.org/licenses/MIT).

## Acknowledgements

- [Tweetinvi](https://github.com/linvi/Tweetinvi) - Twitter API library for .NET.
- [DeepAI](https://deepai.org/) - AI image generation API.
- [Newtonsoft.Json](https://www.newtonsoft.com/json) - JSON framework for .NET.

## Author

Twitter AI Bot is developed by [Your Name](https://github.com/your-username). Feel free to reach out with any questions or inquiries.

Enjoy generating and sharing AI-generated images on Twitter!
