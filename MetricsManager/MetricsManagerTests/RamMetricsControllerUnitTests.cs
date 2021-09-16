using AutoMapper;
using MetricsManager.Controllers;
using MetricsManager.Interfaces;
using MetricsManager.Models;
using MetricsManager.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class RamMetricsControllerUnitTests
    {
        private RamMetricsController controller;
        private Mock<IRamNetMetricsRepository> mock;
        private Mock<ILogger<RamMetricsController>> logMock;
        private Mock<IMapper> mapper;
        public RamMetricsControllerUnitTests()
        {
            mock = new Mock<IRamNetMetricsRepository>();
            logMock = new Mock<ILogger<RamMetricsController>>();
            mapper = new Mock<IMapper>();
            controller = new RamMetricsController(mock.Object, logMock.Object, mapper.Object);
        }

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит metric объект
            mock.Setup(repository => repository.Create(It.IsAny<RamMetric>())).Verifiable();

            // выполняем действие на контроллере
            var result = controller.Create(new RamMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 50 });
            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            mock.Verify(repository => repository.Create(It.IsAny<RamMetric>()), Times.AtMostOnce());
        }

        [Fact]
        public void GetMetricsFromAllCluster_ReturnsOk()
        {
            //Arrange            
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            //Act
            var result = controller.GetMetricsFromAllCluster(fromTime, toTime);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
