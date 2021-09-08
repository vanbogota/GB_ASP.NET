using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace MetricsManagerTests
{
    public class AgentsControllerUnitTests
    {
        private AgentsController controller;
        private ValuesHolder values;

        public AgentsControllerUnitTests()
        {
            controller = new AgentsController(values);
        }

        [Fact]
        public void RegisterAgent_ReturnsOk()
        {
            //Arrange
            var agentInfo = new AgentInfo();
            //Act
            var result = controller.RegisterAgent(agentInfo);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }        

        [Fact]
        public void EnableAgentById_ReturnsOk()
        {
            //Arrange
            int agentId = 1;
            //Act
            var result = controller.EnableAgentById(agentId);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }  
        
        [Fact]
        public void DisableAgentById_ReturnsOk()
        {
            //Arrange
            int agentId = 1;
            //Act
            var result = controller.EnableAgentById(agentId);

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }        

        [Fact]
        public void RegistredAgents_ReturnsOk()
        {
            //Arrange            
            //Act
            var result = controller.RegistredAgents();

            //Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
