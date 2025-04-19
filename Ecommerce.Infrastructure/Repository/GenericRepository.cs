using Ecommerce.Core.Specifications;

namespace Ecommerce.Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
            => await _context.Set<T>().ToListAsync();

        public async Task<T?> GetByIdAsync(int id)
            => await _context.Set<T>().FindAsync(id);

        public async Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec)
            => await ApplySpecification(spec).ToListAsync();

        public async Task<T?> GetWithSpec(ISpecification<T> spec)
            => await ApplySpecification(spec).FirstOrDefaultAsync();

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
            => SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);

        public async Task<int> CountAsync(ISpecification<T> spec)
            => await ApplySpecification(spec).CountAsync();

        public void Add(T entity)
            => _context.Set<T>().Add(entity);

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        } 

        public void Delete(T entity)
            => _context.Set<T>().Remove(entity);
    }
}
