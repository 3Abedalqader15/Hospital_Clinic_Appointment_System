using System.Linq.Expressions;

namespace Hospital_Clinic_Appointment_System.Repositories.IRepositories
{
    public interface IGenericRepository<T> where T : class  
    {
        Task<IEnumerable<T>> GetAllAsync(); 
        Task<T?> GetByIdAsync(int id); // T? allows for null return if not found
        Task AddAsync(T entity);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate); // For filtering entities based on a condition

        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync(); 






    }
}
