using HandwrittenTextRecognitionSystem.Specification;
using Microsoft.EntityFrameworkCore;

namespace HandwrittenTextRecognitionSystem.Services
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllWithSpecificationAsync(ISpecification<T> spec);
        Task<T> GetByIdWithSpecificationAsync(ISpecification<T> spec);
    }
}
