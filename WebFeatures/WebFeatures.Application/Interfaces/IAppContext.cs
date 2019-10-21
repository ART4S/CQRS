using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WebFeatures.Domian.Entities.Abstractions;

namespace WebFeatures.Application.Interfaces
{
    public interface IAppContext
    {
        ChangeTracker ChangeTracker { get; }

        DatabaseFacade Database { get; }

        DbSet<T> Set<T>() where T : class;

        T GetById<T>(int id) where T : class, IEntity, new();

        void Add<T>(T entity) where T : class, IEntity, new();

        void Remove<T>(int id) where T : class, IEntity, new();

        bool Exists<T>(int id) where T : class, IEntity, new();

        int SaveChanges();
    }
}
