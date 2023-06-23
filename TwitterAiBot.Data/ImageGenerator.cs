using DeepAI;
using System.Threading.Tasks;

namespace TwitterByAIBOT.Data
{
    public class ImageGenerator
    {
        private readonly string _apiKey;

        public ImageGenerator(string apiKey)
        {
            _apiKey = apiKey;
        }

        public string GenerateImage(string prompt)
        {
            DeepAI_API api = new DeepAI_API(apiKey: _apiKey);
            StandardApiResponse resp = api.callStandardApi("text2img", new { text = prompt });
            var imageUrl = resp.output_url;
            return imageUrl;
        }
    }
}