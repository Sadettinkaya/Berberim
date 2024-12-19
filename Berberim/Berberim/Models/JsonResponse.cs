using System.Text.Json.Serialization;

namespace Berberim.Models
{
    public class JsonResponse
    {
        public List<ImageData> Data { get; set; }
    }

    public class ImageData
    {
        [JsonPropertyName("imageUrl")]
        public string Url { get; set; }
    }
}
