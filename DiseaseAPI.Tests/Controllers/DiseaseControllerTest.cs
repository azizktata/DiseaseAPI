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
    public class DiseaseControllerTest
    {
        private readonly IUnitOfWork _unitOfWork;
        public DiseaseControllerTest() 
        {
            _unitOfWork = A.Fake<IUnitOfWork>();
        
        }
        [Fact]
        public void GetDiseaseById_ShouldReturnOkReponse_WhenValidInput()
        {
            //Arrange
            var controller = new DiseaseController(_unitOfWork);

            //Act
            var result = controller.GetById(1);
            var status = result.Result;

            //Assert
            status.Should().BeOfType<OkObjectResult>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void GetDiseaseById_ShouldReturnBadRequest_WhenInValidInput(int id)
        {
            //Arrange
            var controller = new DiseaseController(_unitOfWork);

            //Act
            var result = controller.GetById(id);
            var status = result.Result;

            //Assert
            status.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public void CreateDisease_ShouldReturnOkResponse_WhenValidRequest()
        {
            //Arrange
            var controller = new DiseaseController(_unitOfWork);
            AddDiseaseDto disease = new AddDiseaseDto();
            disease.diseaseName= "Test";

            //Act
            var result = controller.AddDisease(disease);
            var status = result.Result;

            //Assert
            status.Should().BeOfType<OkObjectResult>();
         

        }
        [Fact]
        public void CreateDisease_ShouldReturnBadRequest_WhenInValidRequest()
        {
            //Arrange
            var controller = new DiseaseController(_unitOfWork);
            AddDiseaseDto disease = new AddDiseaseDto();
            disease.diseaseName = null;

            //Act
            var result = controller.AddDisease(disease);
            var status = result.Result;

            //Assert
            status.Should().BeOfType<BadRequestObjectResult>();


        }


        

    }
}
