using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    public static class SpecifcationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery , ISpecifications<TEntity> spec)
        {
            var query = inputQuery;

            if (spec.Critaria is not null)
                query = query.Where(spec.Critaria);

            if(spec.Orderby is not null)
                query =query.OrderBy(spec.Orderby);
            else if(spec.OrderbyDesc is not null)
                query = query.OrderByDescending(spec.OrderbyDesc);
           
            if(spec.ISPaginationEnable)
                query = query.Skip(spec.Skip).Take(spec.Take);
            query = spec.Includes.Aggregate(query, (CurrentQuery, includesExpressions) => CurrentQuery.Include(includesExpressions));
            return query;
        }

    }
}
