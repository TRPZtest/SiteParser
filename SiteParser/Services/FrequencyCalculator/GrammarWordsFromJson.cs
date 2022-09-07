using System.Text.Json.Serialization;

namespace SiteParser.Services.FrequencyCalculator
{
    public class GrammarWordsProxy
    {
        [JsonPropertyName("grammarWordsList")]
        public List<string> GrammarWordsList { get; set; }
    }
}
