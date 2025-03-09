using LoadFit.Core.Entities;
using LoadFit.Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadFit.Repository
{
    internal static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery (IQueryable<TEntity> innerQuery , ISpecification<TEntity> spec)
        {
            var query = innerQuery;    // _dbContext.Set<Vehicle>()  TEntity => Vehicle

            if(spec.Criteria is not null)   // Criteria =>   P => P.Id == id
            {
                query = query.Where(spec.Criteria);  // _dbContext.Set<Vehicle>().Where(P => P.Id == id)
            }

            if(spec.OrderBy is not null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            else if(spec.OrderByDesc is not null)
            {
                query = query.OrderByDescending(spec.OrderByDesc);
            }

            query = spec.Includes.Aggregate(query, (CurrentQuery, IncludesExpression) => CurrentQuery.Include(IncludesExpression));

            return query;

        }

    }
}
