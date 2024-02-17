using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using traffic_lights_sytem_app.Controllers;
using traffic_lights_sytem_app.Modal;

namespace traffic_lights_sytem_app.Tests
{
    public class TrafficLightControllerTests
    {
        [Test]
        public async Task GetTrafficData_Returns_OkResult()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<TrafficLightController>>();

            var trafficLights = new List<TrafficLight>
            {
                new TrafficLight { Id = 1, Direction = TrafficDirection.North, Color = LightColor.Red },
                new TrafficLight { Id = 2, Direction = TrafficDirection.South, Color = LightColor.Green }
            };

            var mockDbSet = new Mock<DbSet<TrafficLight>>();
            mockDbSet.As<IQueryable<TrafficLight>>().Setup(m => m.Provider).Returns(trafficLights.AsQueryable().Provider);
            mockDbSet.As<IQueryable<TrafficLight>>().Setup(m => m.Expression).Returns(trafficLights.AsQueryable().Expression);
            mockDbSet.As<IQueryable<TrafficLight>>().Setup(m => m.ElementType).Returns(trafficLights.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<TrafficLight>>().Setup(m => m.GetEnumerator()).Returns(trafficLights.AsQueryable().GetEnumerator());

            var mockContext = new Mock<TrafficLightDbContext>();
            mockContext.Setup(m => m.TrafficLights).Returns(mockDbSet.Object);

            var controller = new TrafficLightController(mockContext.Object, mockLogger.Object);

            // Act
            var result = await controller.GetTrafficData();

            // Assert
            // Assert that the result is an OkObjectResult
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GetTrafficData_Handles_Exception()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<TrafficLightController>>();
            var mockContext = new Mock<TrafficLightDbContext>();

            // Mock the TrafficLightDbContext to throw an exception
            mockContext.Setup(m => m.TrafficLights).Throws(new Exception("Test exception"));

            var controller = new TrafficLightController(mockContext.Object, mockLogger.Object);

            // Act
            var result = await controller.GetTrafficData();

           // Assert that the result is a StatusCodeResult
            Assert.That(result, Is.InstanceOf<StatusCodeResult>());

            // Cast the result to StatusCodeResult to access the StatusCode property
            var statusCodeResult = (StatusCodeResult)result;

            // Assert that the status code is 500
  
            Assert.Equals(500, statusCodeResult.StatusCode);
        }
    }
}
