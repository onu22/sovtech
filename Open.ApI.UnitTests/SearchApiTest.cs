
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Open.API.Controllers;
using Open.API.Models;
using Open.API.Services;
using Xunit;

namespace Open.ApI.UnitTest
{

    public class SearchApiTest
    {
        private readonly Mock<ILogger<SearchController>> _loggerMock;
        private readonly Mock<ISwapiService> _swapiServiceMock;
        private readonly Mock<IChuckNorrisService> _chuckNorrisServiceMock;
        public SearchApiTest()
        {
            _swapiServiceMock = new Mock<ISwapiService>();
            _chuckNorrisServiceMock = new Mock<IChuckNorrisService>();
            _loggerMock = new Mock<ILogger<SearchController>>();
        }


        [Fact]
        public async void Search_result_should_contain_apis()
        {
            //Arrange
            var fakeJokeData = GetFakeJokeData();
            var swapData = GetFakeSwapData();

            _chuckNorrisServiceMock.Setup(x => x.SearchJokesAsync(It.IsAny<string>())).Returns(Task.FromResult(fakeJokeData));
            _swapiServiceMock.Setup(x => x.SearchPeopleAsync(It.IsAny<string>())).Returns(Task.FromResult(swapData));

            //Act
            var searchController = new SearchController(_chuckNorrisServiceMock.Object, _swapiServiceMock.Object, _loggerMock.Object);
            var response = await searchController.SearchAsync("testquery") as OkObjectResult;

            //Assert
            var searchData = Assert.IsType<SearchData>(response.Value);
            var chucknorrisValue = searchData.SourceApis[0];
            var swapiValue = searchData.SourceApis[1];

            Assert.Equal("chucknorris", chucknorrisValue);
            Assert.Equal("swapi", swapiValue);
        }

        [Fact]
        public async void Search_query_bad_request()
        {

            var searchController = new SearchController(_chuckNorrisServiceMock.Object, _swapiServiceMock.Object, _loggerMock.Object);
            var actionResult = await searchController.SearchAsync("") as BadRequestResult;

            Assert.Equal(actionResult.StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
        }


        private JokeData GetFakeJokeData()
        {
            return new JokeData()
            {
                Total = 2
            };
        }

        private SwapData GetFakeSwapData()
        {
            return new SwapData()
            {
                Count = 2
            };
        }

    }

}
