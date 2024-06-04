using System.Linq.Expressions;

namespace HandwrittenTextRecognitionSystem.Specification
{
    public class BaseSpecification<T>:ISpecification<T> where T : class
    {
        public BaseSpecification()
        {
        }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
    }
}
