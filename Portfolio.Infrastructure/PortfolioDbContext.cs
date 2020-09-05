using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portfolio.Application.Abstractions;
using Portfolio.Domain;

namespace Portfolio.Infrastructure
{
    public class PortfolioDbContext : IdentityDbContext, IPortfolioDbContext
    {
        public DbSet<Project> Projects { get; set; }
    }
}