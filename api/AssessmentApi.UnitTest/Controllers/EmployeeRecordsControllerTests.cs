using AssessmentApi.Controllers;
using AssessmentApi.Model;
using AssessmentApi.Provider;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace AssessmentApi.UnitTest.Controllers
{
    public class EmployeeRecordsControllerTests
    {
        private readonly Mock<IExcelReader> _excelReader;

        public EmployeeRecordsControllerTests()
        {
            _excelReader = new Mock<IExcelReader>();
        }

        [Fact]
        public void GetEmployeeRecords_WithResult_ReturnResult()
        {
            //arrange
            var setupExcelReaderData = new List<Employee>() { 
                new Employee() {
                    EmployeeNumber = "001",
                    FirstName = "FirstName01",
                    LastName = "LastName01",
                    EmployeeStatus = "Regular"
                },
                new Employee() {
                    EmployeeNumber = "002",
                    FirstName = "FirstName02",
                    LastName = "LastName02",
                    EmployeeStatus = "Contractor"
                }
            };

            _excelReader.Setup(x => x.ReadFromExcel()).Returns(setupExcelReaderData);

            //action
            var sut = new EmployeeRecordsController(_excelReader.Object);
            var response = sut.Get();

            //assert
            var objectResult = response.As<ObjectResult>();
            objectResult.StatusCode.Should().Be(200);
            objectResult.Value.Should().Be(setupExcelReaderData);
            _excelReader.Verify(x => x.ReadFromExcel(), Times.Once);
        }

        [Fact]
        public void GetEmployeeRecords_NoResult_ReturnNoContentFound()
        {
            //arrange
            _excelReader.Setup(x => x.ReadFromExcel()).Returns(new List<Employee>());

            //action
            var sut = new EmployeeRecordsController(_excelReader.Object);
            var response = sut.Get();

            //assert
            var noContentResult = response.As<NoContentResult>();
            noContentResult.StatusCode.Should().Be(204);
            _excelReader.Verify(x => x.ReadFromExcel(), Times.Once);
        }

        [Fact]
        public void GetEmployeeRecords_ExceptionCaught_ReturnInternalServerError()
        {
            //arrange
            var isExceptionThrown = false;
            _excelReader.Setup(x => x.ReadFromExcel()).Throws(new Exception());

            //action
            var sut = new EmployeeRecordsController(_excelReader.Object);

            try
            {
                var response = sut.Get();
            }
            catch
            {
                isExceptionThrown = true;
            }

            //assert
            isExceptionThrown.Should().BeTrue();
        }
    }
}
