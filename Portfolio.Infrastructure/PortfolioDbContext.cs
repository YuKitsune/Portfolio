using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Portfolio.Application.Abstractions;
using Portfolio.Domain;

namespace Portfolio.Infrastructure
{
    public class PortfolioDbContext : DbContext, IPortfolioDbContext
    {
        public DbSet<Project> Projects { get; set; }
    }
}