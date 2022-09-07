using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace SiteParser.Services.FrequencyCalculator
{
    public class ExpressionFrequencyCalculator
    {
        private int _expressionWordsCount;       
        private List<string> _wordsList;
      
        private Dictionary<string, int> CountExpressions()
        {
            var expressionsDictionary = new Dictionary<string, int>(StringComparer.CurrentCultureIgnoreCase);            

            for (int i = 0; i < _wordsList.Count - _expressionWordsCount; i++)
            {
                var currentExpression = _wordsList[i];
                if (_expressionWordsCount != 0)
                    for (int k = 1; k < _expressionWordsCount; k++)
                        currentExpression += $" {_wordsList[i + k]}";

                expressionsDictionary.TryGetValue(currentExpression, out int currentCount);

                currentCount++;
                expressionsDictionary[currentExpression] = currentCount;
            }
            return expressionsDictionary;
        }
        protected virtual string FormatString(string s)
        {      
            s = Regex.Replace(s, "[^a-zA-Z ]", " ");

            s = Regex.Replace(s, @"\s+", " ").Trim();

            return s;
        }
        protected virtual IEnumerable<string> SplitString(string s)
        {
            return s.Split(' ');
        }
        public List<CalculationResult> CalculateFrequencies(string text, int WordsNumber, bool ignoreGrammarWords = false)
        {
            _expressionWordsCount = WordsNumber;
            _wordsList  = SplitString(FormatString(text)).ToList();

            if (ignoreGrammarWords == true)
            {
                var grammarWords = GetGrammarWords().Result;
                _wordsList = _wordsList.Where(item => !grammarWords.Contains(item, StringComparer.CurrentCultureIgnoreCase)).ToList();
            }             

            var expressionsDictionary = CountExpressions();
                        
            var result = new List<CalculationResult>();
            var totalCount = expressionsDictionary.Sum(x => x.Value);

            foreach (var expression in expressionsDictionary)
                result.Add(new CalculationResult { Expression = expression.Key, Frequency = (float)expression.Value / totalCount, Count = expression.Value });
            return result;
        }
        protected virtual async Task <List<string>> GetGrammarWords()
        {
            using FileStream stream = File.OpenRead("GrammаrWords.json");
            var words = await JsonSerializer.DeserializeAsync<GrammarWordsProxy>(stream);
            
            return words.GrammarWordsList;
        }
    }
}
