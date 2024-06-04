using System.Linq.Expressions;

namespace HandwrittenTextRecognitionSystem.Specification
{
    public interface ISpecification<T> where T : class
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; }
    }
}
