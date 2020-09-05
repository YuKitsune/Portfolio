using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Portfolio.Application.Abstractions;
using Portfolio.Domain;

namespace Portfolio.Application
{
    public class ProjectService
    {
        private readonly IPortfolioDbContext portfolioDbContext;

        public ProjectService(IPortfolioDbContext portfolioDbContext)
        {
            this.portfolioDbContext = portfolioDbContext;
        }
        
        public async Task AddProjectAsync(Project project)
        {
            if (await portfolioDbContext.Projects.AnyAsync(p => p.Url == project.Url))
            {
                throw new Exception($"Project with URL \"{project.Url}\" already exists.");
            }

            await portfolioDbContext.Projects.AddAsync(project);
            await portfolioDbContext.SaveChangesAsync();
        }
        
        public async Task UpdateProjectAsync(Project project)
        {
            Project existingProject = await portfolioDbContext.Projects.FirstOrDefaultAsync(p => p.Id == project.Id);
            if (existingProject == null)
            {
                throw new Exception($"No project with ID \"{project.Id}\" exists.");
            }

            existingProject.Url = project.Url;
            
            await portfolioDbContext.SaveChangesAsync();
        }
        
        public async Task RemoveProjectAsync(Project project)
        {
            Project existingProject = await portfolioDbContext.Projects.FirstOrDefaultAsync(p => p.Id == project.Id);
            if (existingProject == null)
            {
                throw new Exception($"No project with ID \"{project.Id}\" exists.");
            }

            portfolioDbContext.Projects.Remove(existingProject);
            await portfolioDbContext.SaveChangesAsync();
        }
    }
}