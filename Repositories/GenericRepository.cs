using Hospital_Clinic_Appointment_System.App_Context;
using Hospital_Clinic_Appointment_System.Entities;
using Hospital_Clinic_Appointment_System.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hospital_Clinic_Appointment_System.Repositories;

public class GenericRepository<T>(DBContext context) : IGenericRepository<T> where T : class, IEntity
{
    protected readonly DBContext _context = context;
    protected readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task<IEnumerable<T>> GetAllWithIncludesAsync(params Expression<Func<T, object?>>[] includes)
    {
        IQueryable<T> query = _dbSet;
        if (includes?.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        return await query.ToListAsync();
    }

    public Task<T?> GetByIdAsync(int id)
    {
        return _dbSet.FindAsync(id).AsTask();
    }

    public async Task<T?> FirstOrDefaultWithIncludesAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object?>>[] includes)
    {
        IQueryable<T> query = _dbSet;
        if (includes?.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        return await query.FirstOrDefaultAsync(predicate);
    }

    public Task AddAsync(T entity)
    {
        return _dbSet.AddAsync(entity).AsTask();
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}