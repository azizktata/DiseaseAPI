using diseaseAPI_DotNet6;
using diseaseAPI_DotNet6.Controllers;
using Domain.Interfaces;
using Domain.Models;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseaseAPI.Tests.Controllers
{
    public class ArticleControllerTest
    {
        private readonly IUnitOfWork _unitOfWork;
        public ArticleControllerTest() 
        {
            _unitOfWork = A.Fake<IUnitOfWork>();
        
        }
        [Fact]
        public void GetArticlesByDiseaseId_ShouldReturnOkReponse_WhenValidInput()
        {
            //Arrange
            var controller = new ArticleController(_unitOfWork);

            //Act
            var result = controller.GetByDiseaseId(1);
            var status = result.Result;

            //Assert
            status.Should().BeOfType<OkObjectResult>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void GetArticlesByDiseaseId_ShouldReturnBadRequest_WhenInValidInput(int id)
        {
            //Arrange
            var controller = new ArticleController(_unitOfWork);

            //Act
            var result = controller.GetByDiseaseId(id);
            var status = result.Result;

            //Assert
            status.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void CreateArticle_ShouldReturnOkResponse_WhenValidRequest()
        {
            //Arrange
            var controller = new ArticleController(_unitOfWork);
            AddArticleDto article = new AddArticleDto();
            article.title= "Title";
            article.description= "Description";
            article.url= "URL";
            article.diseaseId= 1;

            //Act
            var result = controller.AddArticle(article);
            var status = result.Result;

            //Assert
            status.Should().BeOfType<OkObjectResult>();
         

        }


        [Theory]
        [InlineData(0,null)]
        [InlineData(-1, null)]
        public void CreateArticle_ShouldReturnBadRequest_WhenInValidRequest(int id, string request)
        {
            //Arrange
            var controller = new ArticleController(_unitOfWork);
            AddArticleDto article = new AddArticleDto();
            article.title = request;
            article.description = request;
            article.url = request;
            article.diseaseId = id;

            //Act
            var result = controller.AddArticle(article);
            var status = result.Result;

            //Assert
            status.Should().BeOfType<BadRequestObjectResult>();


        }


    }
}
