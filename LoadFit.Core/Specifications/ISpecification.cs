using LoadFit.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LoadFit.Core.Specifications
{
    public interface ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }   // P => P.Id == id

        public List<Expression<Func<T, object>>> Includes { get; set; }   // {P => P.Brand,P => P.Type}

        public Expression<Func<T, object>> OrderBy { get; set; }      // OrderBy(P => P.Name)  Default Sort Asc
        public Expression<Func<T, object>> OrderByDesc { get; set; }      // OrderByDesc(P => P.Name)  Sort Desc

    }
}
