using HtmlAgilityPack;
using System;
using Xunit;
using CoderGirl_MVCMovies.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace Test
{
    public class TestMovieRatingController
    {
        private MovieRatingController controller;

        public TestMovieRatingController()
        {
            controller = new MovieRatingController();
        }

        [Fact]
        public void Test_GET_Create_ContainsForm()
        {           
            ContentResult result = (ContentResult)controller.Create();

            Assert.Equal("text/html", result.ContentType);
            HtmlNode html = HtmlNode.CreateNode(result.Content);
            Assert.Equal("form", html.Name);
        }

        [Fact]
        public void Test_GET_Create_ContainsForm_WithMethodOfPost()
        {
            ContentResult result = (ContentResult)controller.Create();

            HtmlNode html = HtmlNode.CreateNode(result.Content);
            HtmlAttribute method = html.Attributes.FirstOrDefault(attr => attr.Name.ToLower() == "method");
            Assert.NotNull(method);
            Assert.Equal("post", method.Value.ToLower());
        }

        [Fact]
        public void Test_GET_Create_ContainsForm_WithNoAction_OrActionForCreateMovieRating()
        {
            ContentResult result = (ContentResult)controller.Create();

            HtmlNode html = HtmlNode.CreateNode(result.Content);
            HtmlAttribute formAction = html.Attributes.FirstOrDefault(attr => attr.Name.ToLower() == "action");
            Assert.True(formAction == null || formAction.Value.ToLower() == "/movierating/create");
        }

        [Fact]
        public void Test_GET_Create_ContainsForm_WithInputElement()
        {
            ContentResult result = (ContentResult)controller.Create();

            HtmlNode input = HtmlNode.CreateNode(result.Content).ChildNodes.FirstOrDefault(node => node.Name == "input");
            Assert.NotNull(input);
            Assert.Equal("movieName", input.Attributes.SingleOrDefault(attr => attr.Name == "name").Value);
        }

        [Fact]
        public void Test_GET_Create_ContainsForm_WithSelectElement_ContainingRatings()
        {
            string[] expectedRatings = { "1", "2", "3", "4", "5" };

            ContentResult result = (ContentResult)controller.Create();

            HtmlNode select = HtmlNode.CreateNode(result.Content).ChildNodes.FirstOrDefault(node => node.Name == "select");
            Assert.NotNull(select);
            Assert.Equal("rating", select.Attributes.SingleOrDefault(attr => attr.Name == "name").Value);
            var actualRatings = select.ChildNodes.Where(node => node.Name == "option").Select(opt => opt.InnerText).ToArray();
            Assert.Equal(expectedRatings, actualRatings);
        }

        [Fact]
        public void Test_GET_Create_ContainsForm_WithSubmitButton()
        {
            IActionResult result = controller.Create();

            ContentResult content = Assert.IsType<ContentResult>(result);
            HtmlNode submitBtn = HtmlNode.CreateNode(content.Content).ChildNodes.FirstOrDefault(node => node.Name == "button");
            Assert.NotNull(submitBtn);
            Assert.Equal("submit", submitBtn.Attributes.SingleOrDefault(attr => attr.Name == "type").Value.ToLower());
        }

        [Theory]
        [InlineData("Star Wars", "5")]
        [InlineData("The Princess Bride", "4")]
        public void Test_POST_Create_RedirectsTo_GETDetails_WithInputValues(string movieName, string rating)
        {
            IActionResult result = controller.Create(movieName, rating);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Details", redirect.ActionName);
            Assert.Equal(new KeyValuePair<string, object>("movieName", movieName), redirect.RouteValues.First());
            Assert.Equal(new KeyValuePair<string, object>("rating", rating), redirect.RouteValues.Skip(1).Take(1).First());
        }

        [Theory]
        [InlineData("Star Wars", "5")]
        [InlineData("The Princess Bride", "4")]
        public void Test_GET_Details(string movieName, string rating)
        {
            string expectedString = $"{movieName} has a rating of {rating}";

            IActionResult result = controller.Details(movieName, rating);

            ContentResult content = Assert.IsType<ContentResult>(result);
            Assert.Equal(expectedString, content.Content);
        }
    }
}
