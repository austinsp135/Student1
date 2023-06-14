using Microsoft.EntityFrameworkCore;
using StudentManagement.Api.Model;
using StudentManagement.Api.Persistence;
using StudentManagement.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly StudentDbContext dbContext;
    private readonly DbSet<T> dbSet;

    public Repository(StudentDbContext _dbContext)
    {
        this.dbContext = _dbContext;
        this.dbSet = _dbContext.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await dbSet.ToListAsync();
    }

    public async Task<T> GetById(int id)
    {
        var student = await dbSet.FindAsync(id);
        if (student == null)
            return null;
        else
            return student;
    }

    public async Task<T> Create(T entity)
    {
        dbSet.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<T> Delete(int id)
    {
        var entity = await dbSet.FindAsync(id);
        if (entity == null)
            return null;
        dbSet.Remove(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<T> Update(T entity)
    {
        var idProperty = typeof(T).GetProperty("Id");
        var entityId = idProperty?.GetValue(entity);
        var existingEntity = await dbSet.FindAsync(entityId);
        dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

}
