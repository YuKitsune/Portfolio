using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portfolio.Application;
using Portfolio.Domain;

namespace Portfolio.Api.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ProjectService service;
        private readonly ILogger<ProjectService> logger;
        
        [HttpPost]
        public async Task<IActionResult> AddProject(Project project)
        {
            try
            {
                await service.AddProjectAsync(project);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to add new project.");
                return BadRequest();
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> UpdateProject(Project project)
        {
            try
            {
                await service.UpdateProjectAsync(project);
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
                await service.RemoveProjectAsync(project);
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