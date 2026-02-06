using System.Linq.Expressions;

namespace Hospital_Clinic_Appointment_System.Repositories.IRepositories
{
    public interface GenericRepository<T> where T : class  
    {
        Task<IEnumerable<T>> GetAllWithIncludesAsync(params Expression<Func<T, object>>[] includes);
        Task<T?> GetByIdAsync(int id); 
        Task AddAsync(T entity);

        Task<T?> FirstOrDefaultWithIncludesAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync(); 






    }
}
