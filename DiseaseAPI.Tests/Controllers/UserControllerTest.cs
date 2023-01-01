using diseaseAPI_DotNet6.Controllers;
using Domain.Interfaces;
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
    public  class UserControllerTest
    {
        private readonly IUnitOfWork _UnitOfWork;

        public UserControllerTest() 
        { 
            _UnitOfWork = A.Fake<IUnitOfWork>();
        }
        [Fact]
        public void GetAll_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arrange   
            var controller = new UserController(_UnitOfWork);

            //Act
            var result = controller.GetAll();
            var status = result.Result;

            //Assert
            status.Should().BeOfType<OkObjectResult>();
            
            

        }
        [Fact]
        public void GetUserById_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arrange   
            var controller = new UserController(_UnitOfWork);

            //Act
            var result = controller.GetUserById(1);
            var status = result.Result;

            //Assert
            status.Should().BeOfType<OkObjectResult>();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void GetUserById_ShouldReturnBadRequest_WhenInvalidInput(int id)
        {
            //Arrange   
            var controller = new UserController(_UnitOfWork);
            

            //Act
            var result = controller.GetUserById(id);
            var status = result.Result;

            //Assert
            status.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
