using System.Linq;
using System;
using System.Collections.Generic;
using KanbanManagement.API.Controllers;
using KanbanManagement.API.Dto.Response;
using KanbanManagement.API.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using KanbanManagement.API.Dto.Request;
using Common.Dto;
using KanbanManagement.API.Consts;
using System.Net;

namespace KanbanManagement.Test.Unit.Controller
{
    public class AssignmentControllerUnitTest
    {
        private readonly List<AssignmentResponseDto> _assignments;
        private readonly Mock<IAssignmentService> _assignmentServiceMock;
        private readonly AssignmentController _controller;
        private string _projectId = "";
        private CreateAssignmentRequestDto _createAssignment = null;
        public AssignmentControllerUnitTest()
        {
            _projectId = Guid.NewGuid().ToString();

            AssignmentResponseDto assignmentOne = new AssignmentResponseDto();
            assignmentOne.Id = Guid.NewGuid();
            assignmentOne.Name = "Name";
            assignmentOne.Description = "Desc";
            assignmentOne.DateOfCreation = DateTime.Now;
            assignmentOne.CreatedBy = Guid.NewGuid();
            assignmentOne.Status = "TODO";

            AssignmentResponseDto assignmentTwo = new AssignmentResponseDto();
            assignmentOne.Id = Guid.NewGuid();
            assignmentOne.Name = "Name Two";
            assignmentOne.Description = "Desc Two";
            assignmentOne.DateOfCreation = DateTime.Now;
            assignmentOne.CreatedBy = Guid.NewGuid();
            assignmentOne.Status = "INPROGRESS";
            
            _assignments = new List<AssignmentResponseDto>();
            _assignments.Add(assignmentOne);
            _assignments.Add(assignmentTwo);

            _assignmentServiceMock = new Mock<IAssignmentService>();

            _createAssignment = new CreateAssignmentRequestDto();
            _createAssignment.Name = "Name";
            _createAssignment.Description = "Desc";
            _createAssignment.CreatedBy = _assignments.ElementAt(0).CreatedBy;
            _createAssignment.Status = "TODO";

            _assignmentServiceMock.Setup(svc => svc.RetrieveById(_assignments.ElementAt(0).Id.ToString()))
                .ReturnsAsync(_assignments.ElementAt(0));

            _assignmentServiceMock.Setup(svc => svc.RetrieveAll(_projectId))
                .ReturnsAsync(_assignments);

            _assignmentServiceMock.Setup(svc => svc.CreateAssignment(_createAssignment))
                .ReturnsAsync(_assignments.ElementAt(0));
            
            _controller = new AssignmentController(_assignmentServiceMock.Object);
        }

        [Fact]
        public async void RetrieveAssignmentById_should_return_AssignmentResponseDto_dto() 
        {
            var result = await _controller.RetrieveAssignmentById(_assignments.ElementAt(0).Id.ToString()) as OkObjectResult;
            var assignment = Assert.IsType<AssignmentResponseDto>(result.Value);

            var expectedAssignment = _assignments.ElementAt(0);

            Assert.Equal(expectedAssignment.Name, assignment.Name);
            Assert.Equal(expectedAssignment.Description, assignment.Description);
            Assert.Equal(expectedAssignment.AssignedTo, assignment.AssignedTo);
            Assert.Equal(expectedAssignment.CreatedBy, assignment.CreatedBy);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void RetrieveAllAssignments_should_return_array_of_AssignmentResponseDto_dto() 
        {
            var result = await _controller.RetrieveAllAssignments(_projectId) as OkObjectResult;
            var assignments = Assert.IsType<List<AssignmentResponseDto>>(result.Value);

            var expectedAssignments = _assignments;

            Assert.Equal(expectedAssignments.Count, assignments.Count);
            Assert.Equal(expectedAssignments.ElementAt(0).Description, assignments.ElementAt(0).Description);
            Assert.Equal(expectedAssignments.ElementAt(0).AssignedTo, assignments.ElementAt(0).AssignedTo);
            Assert.Equal(expectedAssignments.ElementAt(0).CreatedBy, assignments.ElementAt(0).CreatedBy);
            Assert.Equal(expectedAssignments.ElementAt(1).Description, assignments.ElementAt(1).Description);
            Assert.Equal(expectedAssignments.ElementAt(1).AssignedTo, assignments.ElementAt(1).AssignedTo);
            Assert.Equal(expectedAssignments.ElementAt(1).CreatedBy, assignments.ElementAt(1).CreatedBy);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void CreateAssignment_should_return_array_of_AssignmentResponseDto_dto() 
        {   
            var result = await _controller.CreateAssignment(_createAssignment) as OkObjectResult;
            var assignment = Assert.IsType<AssignmentResponseDto>(result.Value);

            var expectedAssignment = _assignments.ElementAt(0);
            
            Assert.Equal(expectedAssignment.Name, assignment.Name);
            Assert.Equal(expectedAssignment.Description, assignment.Description);
            Assert.Equal(expectedAssignment.AssignedTo, assignment.AssignedTo);
            Assert.Equal(expectedAssignment.CreatedBy, assignment.CreatedBy);
            Assert.Equal(200, result.StatusCode);
        }

    }
}