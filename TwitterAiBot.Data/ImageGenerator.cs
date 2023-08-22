using DeepAI;
using System.Threading.Tasks;

namespace TwitterByAIBOT.Data
{
    public class ImageGenerator
    {
        private readonly string _apiKey; // API-Schlüssel für den Zugriff auf den DeepAI-Dienst

        public ImageGenerator(string apiKey)
        {
            _apiKey = apiKey; // Initialisieren des API-Schlüssels im Konstruktor
        }

        public string GenerateImage(string prompt)
        {
            DeepAI_API api = new DeepAI_API(apiKey: _apiKey); // Erstellen einer DeepAI_API-Instanz mit dem API-Schlüssel
            StandardApiResponse resp = api.callStandardApi("text2img", new { text = prompt }); // Aufrufen der Standard-API
                                                                                               // "text2img" mit dem bereitgestellten Text
            var imageUrl = resp.output_url; // Extrahieren der generierten Bild-URL aus der API-Antwort
            return imageUrl; // Rückgabe der generierten Bild-URL
        }
    }
}
