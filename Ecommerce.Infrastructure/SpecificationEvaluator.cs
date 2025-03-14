﻿using Ecommerce.Core.Specifications;
using System.Linq;

namespace Ecommerce.Infrastructure
{
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
        {
            var query = inputQuery;

            if (specification.Criteria is not null)
                query = query.Where(specification.Criteria);

            if (specification.OrderBy is not null)
                query = query.OrderBy(specification.OrderBy);

            if (specification.OrderByDescending is not null)
                query = query.OrderByDescending(specification.OrderByDescending);

            query = specification.Includes.Aggregate(query, (cur, include) => cur.Include(include));

            return query;
        }
    }
}
