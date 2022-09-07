using SiteParser.Controllers;
using SiteParser.Models;
using SiteParser.Services.FrequencyCalculator;
using System;
using Xunit;

namespace UnitTests
{
    public class SiteParserTests
    {
        [Fact]
        public void Test_2_Words()
        {
            var calculator = new ExpressionFrequencyCalculator();
            var text = "«Split PDF document online is a web service that allows you to split your PDF document into separate\r\npages. This simple application has several modes of operation, you can split your PDF document into\r\nseparate pages, i.e. each page of the original document will be a separate PDF document, you can split\r\nyour document into even and odd pages, this function will come in handy if you need to print a document in\r\nthe form of a book, you can also specify page numbers in the settings and the Split PDF application will\r\ncreate separate PDF documents only with these pages and the fourth mode of operation allows you to\r\ncreate a new PDF document in which there will be only those pages that you specified.»";
           
            var results = calculator.CalculateFrequencies(text, 2);
       
            Assert.Equal(5, results.FirstOrDefault(x => x.Expression == "PDF document")?.Count);    //PDF document 5 раз  
        }
        [Fact]
        public void Test_1_Words()
        {
            var calculator = new ExpressionFrequencyCalculator();
            var text = "«Split PDF document online is a web service that allows you to split your PDF document into separate\r\npages. This simple application has several modes of operation, you can split your PDF document into\r\nseparate pages, i.e. each page of the original document will be a separate PDF document, you can split\r\nyour document into even and odd pages, this function will come in handy if you need to print a document in\r\nthe form of a book, you can also specify page numbers in the settings and the Split PDF application will\r\ncreate separate PDF documents only with these pages and the fourth mode of operation allows you to\r\ncreate a new PDF document in which there will be only those pages that you specified.»";          

            var results = calculator.CalculateFrequencies(text, 1);

            Assert.True(5 == results.Where(x => x.Count == 3).Count()); // 5 слов по 3  раза повторяются
        }

        [Fact]
        public void ControllerTest()
        {
            var controller = new ParserController();
            var request = new ParseTextRequest { Top = 25, WordsNumber = 2, Url = "https://www.aspose.com/" };

            var response = controller.ParseText(request).Result;

            Assert.True(response.Message == "Success");
        }
    }
}