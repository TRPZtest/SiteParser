﻿using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace SiteParser.Services.FrequencyCalculator
{
    public class ExpressionFrequencyCalculator
    {
        private int _expressionWordsCount;       
        private string _text;
      
        private Dictionary<string, int> countExpressions()
        {
            var expressionsDictionary = new Dictionary<string, int>(StringComparer.CurrentCultureIgnoreCase);
            var wordsList = SplitString(_text).ToArray();

            for (int i = 0; i < wordsList.Length - _expressionWordsCount; i++)
            {
                var currentExpression = wordsList[i] + " " + wordsList[i + 1];
                for (int k = 1; k < _expressionWordsCount; k++)
                    currentExpression += $" {wordsList[i + k]}";

                int currentCount;
                expressionsDictionary.TryGetValue(currentExpression, out currentCount);

                currentCount++;
                expressionsDictionary[currentExpression] = currentCount;
            }
            return expressionsDictionary;
        }
        protected virtual string FormatString(string s)
        {
            s = Regex.Replace(s, "[^a-zA-Z0-9 -]", " ");

            s = Regex.Replace(s, @"\s+", " ").Trim();

            return s;
        }
        protected virtual IEnumerable<string> SplitString(string s)
        {
            return s.Split(' ');
        }
        public List<CalculationResult> CalculateFrequencies(string text, int WordsNumber)
        {
            _expressionWordsCount = WordsNumber;
            _text = text;

            var expressionsDictionary = countExpressions();
            var result = new List<CalculationResult>();
            var totalCount = expressionsDictionary.Sum(x => x.Value);

            foreach (var expression in expressionsDictionary)
                result.Add(new CalculationResult { Expression = expression.Key, Frequency = expression.Value / totalCount });
            return result;
        }
    }
}