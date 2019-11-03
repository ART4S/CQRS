using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WebFeatures.Domian.Entities.Abstractions;

namespace WebFeatures.Application.Interfaces.Data
{
    public interface IAppContext
    {
        ChangeTracker ChangeTracker { get; }

        DatabaseFacade Database { get; }

        DbSet<T> Set<T>() where T : class;

        T GetById<T, TId>(TId id) where T : class, IEntity<TId>, new() where TId : struct, IEquatable<TId>;

        void Add<T, TId>(T entity) where T : class, IEntity<TId>, new() where TId : struct, IEquatable<TId>;

        void Remove<T, TId>(TId id) where T : class, IEntity<TId>, new() where TId : struct, IEquatable<TId>;

        bool Exists<T, TId>(TId id) where T : class, IEntity<TId>, new() where TId : struct, IEquatable<TId>;

        int SaveChanges();
    }
}
