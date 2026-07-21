using EpsilonWebApp.Data;
using EpsilonWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EpsilonWebApp.Services
{
    public class CustomerService(AppDbContext db) : ICustomerService
    {
        public async Task<PagedResult<Customer>> GetPagedAsync(int page, int pageSize)
        {
            var totalCount = await db.Customers.CountAsync();
            var items = await db.Customers
                .OrderBy(c => c.CompanyName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Customer>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }

        public async Task<Customer?> GetByIdAsync(Guid id)
            => await db.Customers.FindAsync(id);

        public async Task<Customer> CreateAsync(Customer customer)
        {
            db.Customers.Add(customer);
            await db.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer?> UpdateAsync(Guid id, Customer customer)
        {
            var existing = await db.Customers.FindAsync(id);
            if (existing is null) return null;

            existing.CompanyName = customer.CompanyName;
            existing.ContactName = customer.ContactName;
            existing.Address = customer.Address;
            existing.City = customer.City;
            existing.Region = customer.Region;
            existing.PostalCode = customer.PostalCode;
            existing.Country = customer.Country;
            existing.Phone = customer.Phone;

            await db.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await db.Customers.FindAsync(id);
            if (existing is null) return false;

            db.Customers.Remove(existing);
            await db.SaveChangesAsync();
            return true;
        }
    }
}
