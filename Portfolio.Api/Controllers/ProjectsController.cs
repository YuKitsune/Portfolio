using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portfolio.Application;
using Portfolio.Domain;

namespace Portfolio.Api.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly ProjectService projectService;
        private readonly ILogger<ProjectService> logger;

        public ProjectsController(ProjectService projectService, ILogger<ProjectService> logger)
        {
            this.projectService = projectService;
            this.logger = logger;
        }
        
        [HttpPost]
        public async Task<IActionResult> AddProject(Project project)
        {
            try
            {
                await projectService.AddProjectAsync(project);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to add new project.");
                return BadRequest();
            }
        }
        
        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> GetProjects()
        {
            try
            {
                IEnumerable<Project> projects = await projectService.GetProjectsAsync();
                return Json(projects);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to get projects.");
                return BadRequest();
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> UpdateProject(Project project)
        {
            try
            {
                await projectService.UpdateProjectAsync(project);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to update project.");
                return BadRequest();
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> RemoveProject(Project project)
        {
            try
            {
                await projectService.RemoveProjectAsync(project);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to remove project.");
                return BadRequest();
            }
        }
    }
}