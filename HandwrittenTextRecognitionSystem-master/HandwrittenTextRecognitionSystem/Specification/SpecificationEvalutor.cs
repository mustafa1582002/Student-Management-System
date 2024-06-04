using Microsoft.EntityFrameworkCore;

namespace HandwrittenTextRecognitionSystem.Specification
{
    public class SpecificationEvalutor <T> where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> Spec)
        {
            var Query = inputQuery;
            if (Spec.Criteria is not null)
            {
                Query = Query.Where(Spec.Criteria);
            }
            Query = Spec.Includes.Aggregate(Query, (CurrentQuery, IncludeQuery) => CurrentQuery.Include(IncludeQuery));
            return inputQuery;
        }
    }
}
