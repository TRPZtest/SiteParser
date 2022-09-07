using Microsoft.AspNetCore.Mvc;
using SiteParser.Models;
using SiteParser.Services.FrequencyCalculator;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace SiteParser.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ParserController : ControllerBase
    {
        [HttpPost]
        public async Task<ParseTextResponse> ParseText(ParseTextRequest request)
        {
            try
            {
                var response = new HttpResponseMessage();
                using (HttpClient client = new())
                    response = await client.GetAsync(request.Url);
                var stringContent = await response.Content.ReadAsStringAsync();
          
                stringContent = FormatHtml(stringContent);

                var calculator = new ExpressionFrequencyCalculator();
                var results = calculator.CalculateFrequencies(stringContent, request.WordsNumber).OrderByDescending(item => item.Count).Take(request.Top);

                return new ParseTextResponse { Code = 0, Message = "Success", CalculationResults = results.ToList() };
            }
            catch (Exception ex)
            {
                return new ParseTextResponse() { Code = 1, Message = $"Error: {ex.Message} {ex.StackTrace}" };
            }            
        }

        private string FormatHtml(string html)
        {
            Regex rRemScript = new Regex(@"<script[^>]*>[\s\S]*?</script>");
            html = rRemScript.Replace(html, "");
            html = Regex.Replace(html, "<.*?>|&.*?;", string.Empty);
            return html;
        }
    }
}
