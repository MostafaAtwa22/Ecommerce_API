namespace Ecommerce.Infrastructure.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly ApplicationDbContext _context;

        public BasketRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerBasket?> GetAsync(string basketId)
            => await _context.CustomerBaskets
            .AsNoTracking()
            .Include(b => b.Items)
            .SingleOrDefaultAsync(b => b.Id == basketId);

        public async Task<CustomerBasket?> UpdateAsync(CustomerBasket basket)
        {
            var existingBasket = await _context.CustomerBaskets
                .Include(b => b.Items) 
                .FirstOrDefaultAsync(b => b.Id == basket.Id);

            if (existingBasket == null)
            {
                _context.CustomerBaskets.Add(basket);
            }
            else
            {
                _context.Entry(existingBasket).CurrentValues.SetValues(basket);

                existingBasket.Items.Clear();
                foreach (var item in basket.Items)
                {
                    existingBasket.Items.Add(item);
                }
            }

            await _context.SaveChangesAsync();
            return basket;
        }


        public async Task<bool> DeleteAsync(string basketId)
        {
            var basket = await _context.CustomerBaskets.FindAsync(basketId);

            if (basket is null)
                return false;

            _context.CustomerBaskets.Remove(basket);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
