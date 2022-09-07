using System.ComponentModel.DataAnnotations;

namespace SiteParser.Models
{
    public class ParseTextRequest
    {
        [Required]
        public string Url { get; set; }
        [Required]
        public int WordsNumber { get; set; }
        [Required]
        public int Top { get; set; }
    }
}
