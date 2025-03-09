using LoadFit.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LoadFit.Core.Specifications
{
    public class BaseSpecifications<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get ; set ; }
        public List<Expression<Func<T, object>>> Includes { get ; set ; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get ; set ; }
        public Expression<Func<T, object>> OrderByDesc { get ; set ; }

        public BaseSpecifications()
        {
            // criteria = null
        }

        public BaseSpecifications(Expression<Func<T,bool>> CriteriaExpression)
        {
            Criteria = CriteriaExpression;
        }


        public void AddOrderBy(Expression<Func<T, object>> orderByExpression)      // Just setter for OrderBy
        {
            OrderBy = orderByExpression;
        }

        public void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression)      // Just setter for OrderByDesc
        {
            OrderByDesc = orderByDescExpression;
        }


    }
}
