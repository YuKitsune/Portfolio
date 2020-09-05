using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Portfolio.Domain;

namespace Portfolio.Application.Abstractions
{
    public interface IPortfolioDbContext
    {
        public DbSet<Project> Projects { get; set; }
        
        public int SaveChanges();
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}