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
    public class Doctor_infoControllerTest
    {
        private readonly IUnitOfWork _unitOfWork;
        public Doctor_infoControllerTest() 
        {
            _unitOfWork = A.Fake<IUnitOfWork>();
        
        }
        [Fact]
        public void GetDoctor_infoByDiseaseId_ShouldReturnOkReponse_WhenValidInput()
        {
            //Arrange
            var controller = new Doctor_infoController(_unitOfWork);

            //Act
            var result = controller.GetByDiseaseId(1);
            var status = result.Result;

            //Assert
            status.Should().BeOfType<OkObjectResult>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void GetDoctor_infosByDiseaseId_ShouldReturnBadRequest_WhenInValidInput(int id)
        {
            //Arrange
            var controller = new Doctor_infoController(_unitOfWork);

            //Act
            var result = controller.GetByDiseaseId(id);
            var status = result.Result;

            //Assert
            status.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void CreateDoctor_info_ShouldReturnOkResponse_WhenValidRequest()
        {
            //Arrange
            var controller = new Doctor_infoController(_unitOfWork);
            AddDoctor_InfoDTO Doctor_info = new AddDoctor_InfoDTO();
            Doctor_info.name= "tbib";
            Doctor_info.speciality= "tawlid";
            Doctor_info.location= "tatawin";
            Doctor_info.diseaseId= 1;

            //Act
            var result = controller.AddDoctor_info(Doctor_info);
            var status = result.Result;

            //Assert
            status.Should().BeOfType<OkObjectResult>();
         

        }


        [Theory]
        [InlineData(0,null)]
        [InlineData(-1, null)]
        public void CreateDoctor_info_ShouldReturnBadRequest_WhenInValidRequest(int id, string request)
        {
            //Arrange
            var controller = new Doctor_infoController(_unitOfWork);
            AddDoctor_InfoDTO Doctor_info = new AddDoctor_InfoDTO();
            Doctor_info.name = request;
            Doctor_info.speciality = request;
            Doctor_info.location = request;
            Doctor_info.diseaseId = id;

            //Act
            var result = controller.AddDoctor_info(Doctor_info);
            var status = result.Result;

            //Assert
            status.Should().BeOfType<BadRequestObjectResult>();


        }


    }
}
