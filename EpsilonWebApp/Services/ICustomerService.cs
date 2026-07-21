using EpsilonWebApp.Models;

namespace EpsilonWebApp.Services
{
    public interface ICustomerService
    {
        Task<PagedResult<Customer>> GetPagedAsync(int page, int pageSize);
        Task<Customer?> GetByIdAsync(Guid id);
        Task<Customer> CreateAsync(Customer customer);
        Task<Customer?> UpdateAsync(Guid id, Customer customer);
        Task<bool> DeleteAsync(Guid id);
    }

    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = [];
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}
