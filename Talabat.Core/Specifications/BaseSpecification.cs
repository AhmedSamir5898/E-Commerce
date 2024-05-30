using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class BaseSpecification<T> : ISpecifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Critaria { get ; set ; }
        public List<Expression<Func<T, object>>> Includes { get ; set ; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> Orderby { get ; set ; }
        public Expression<Func<T, object>> OrderbyDesc { get ; set; }
        public int Skip { get ; set; }
        public int Take { get ; set; }
        public bool ISPaginationEnable { get; set; }

        public BaseSpecification()
        {
        }
        public BaseSpecification( Expression<Func<T,bool>> CritariaExpressions)
        {
            Critaria = CritariaExpressions;
        }
        public void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            Orderby = orderByExpression;
        }
        public void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderbyDesc = orderByDescExpression;
        }
        public void ApplyPagination(int skip ,int take)
        {
            ISPaginationEnable = true;
            Skip = skip;
            Take = take;

        }


    }
}
