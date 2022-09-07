using SiteParser.Services.FrequencyCalculator;

namespace SiteParser.Models
{
    public class ResponseBase
    {
        public int Code { get; set; }
        public string Message { get; set; }
        
    }
    public class ParseTextResponse : ResponseBase
    {
        public List<CalculationResult> CalculationResults { get; set; }
    }
    
}
