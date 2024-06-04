using HandwrittenTextRecognitionSystem.Data;
using HandwrittenTextRecognitionSystem.Models;
using HandwrittenTextRecognitionSystem.Specification;
using Microsoft.EntityFrameworkCore;

namespace HandwrittenTextRecognitionSystem.Services
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Group))
            {

                return (IEnumerable<T>) await _dbContext.Groups.Include(x=>x.Teacher).ToListAsync();
            }
                return await _dbContext.Set<T>().ToListAsync();
        }
        //We Use Find As it used to search locally first then in db 
        //it gives warning as it can give null 
        // where + firstordefault can't be used as it search only in db
        public async Task<T> GetByIdAsync(int id)
        => await _dbContext.Set<T>().FindAsync(id);
        public async Task<IEnumerable<T>> GetAllWithSpecificationAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }
        public async Task<T> GetByIdWithSpecificationAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvalutor<T>.GetQuery(_dbContext.Set<T>(), spec);
        }
    }
}
