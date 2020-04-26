using System.Linq;
using System;
using KanbanManagement.API.Controllers;
using KanbanManagement.API.Dto.Response;
using KanbanManagement.API.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System.Collections.Generic;
using Common.Dto;
using KanbanManagement.API.Dto.Request;
using KanbanManagement.API.Consts;

namespace KanbanManagement.Test.Unit.Controller
{
    public class ProjectControllerUnitTest
    {
        private readonly ProjectController _controller;
        private readonly IProjectService _projectService;
        public ProjectControllerUnitTest()
        {
            _projectService = new ProjectServiceFake();
            _controller = new ProjectController(_projectService);
        }

        [Fact]
        public async void RetrieveProjectById_should_return_ProjectByIdResponseDto_dto() 
        {
            var result = await _controller.RetrieveProjectById(Guid.NewGuid().ToString()) as OkObjectResult;
            var project = Assert.IsType<ProjectByIdResponseDto>(result.Value);

            Assert.Equal("Name", project.Name);
            Assert.Equal("Description", project.Description);
            Assert.Equal(0, project.Assignments.Count);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void RetrieveAllProjects_should_return_array_of_ProjectResponseDto_dto() 
        {
            var result = await _controller.RetrieveAllProjects() as OkObjectResult;
            var projects = Assert.IsType<List<ProjectResponseDto>>(result.Value);

            Assert.Equal("Name", projects.ElementAt(0).Name);
            Assert.Equal("Description", projects.ElementAt(0).Description);
            Assert.Single(projects);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void DeleteProjectById_should_return_EntityDeletedSuccessfully_dto() 
        {
            var result = await _controller.DeleteProjectById(Guid.NewGuid().ToString()) as OkObjectResult;
            var response = Assert.IsType<EntityDeletedSuccessfully>(result.Value);

            Assert.Equal(DomainConsts.ENTITY_PROJECT, response.EntityType);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void CreateProject_should_return_ProjectResponseDto_dto() 
        {
            CreateProjectRequestDto projectDto = new CreateProjectRequestDto();
            projectDto.Name = "Name";
            projectDto.Description = "Description";
            projectDto.CreatedBy = Guid.Parse("ab2bd817-98cd-4cf3-a80a-53ea0cd9c100");

            var result = await _controller.CreateProject(projectDto) as OkObjectResult;
            var project = Assert.IsType<ProjectResponseDto>(result.Value);

            Assert.Equal("Name", project.Name);
            Assert.Equal("Description", project.Description);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void UpdateProject_should_return_ProjectResponseDto_dto() 
        {
            UpdateProjectRequestDto projectDto = new UpdateProjectRequestDto();
            projectDto.Name = "Name";
            projectDto.Description = "Description";
            projectDto.CreatedBy = Guid.Parse("ab2bd817-98cd-4cf3-a80a-53ea0cd9c100");

            var result = await _controller.UpdateProjectById(projectDto, Guid.NewGuid().ToString()) as OkObjectResult;
            var project = Assert.IsType<ProjectResponseDto>(result.Value);

            Assert.Equal(projectDto.Name, project.Name);
            Assert.Equal(projectDto.Description, project.Description);
            Assert.Equal(200, result.StatusCode);
        }
    }
}