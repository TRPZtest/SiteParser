using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SiteParser.Models
{
    public class ParseTextRequest
    {
        [Required]
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [Required]
        [JsonPropertyName("wordsNumber")]
        public int WordsNumber { get; set; }
        [Required]
        [JsonPropertyName("top")]
        public int Top { get; set; }
        [Required]
        [JsonPropertyName("ingoreGrammarWords")]
        public bool IngoreGrammarWords { get; set; }
    }
}
