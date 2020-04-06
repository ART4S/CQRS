using System;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataContext;
using WebFeatures.Application.Interfaces.Jobs;
using WebFeatures.Domian.Common;

namespace WebFeatures.Application.Jobs.Common
{
    internal class SyncEntityBetweenDatabasesJobArg<TEntity> where TEntity : BaseEntity
    {
        public Guid Id { get; }
        public SyncEntityBetweenDatabasesJobArg(Guid id) => Id = id;
    }

    internal abstract class SyncEntityBetweenDatabasesJob<TEntity> : IBackgroundJob<SyncEntityBetweenDatabasesJobArg<TEntity>>
        where TEntity : BaseEntity
    {
        protected readonly IWriteContext WriteDb;
        protected readonly IReadContext ReadDb;

        protected SyncEntityBetweenDatabasesJob(IWriteContext writeDb, IReadContext readDb)
        {
            WriteDb = writeDb;
            ReadDb = readDb;
        }

        public abstract Task ExecuteAsync(SyncEntityBetweenDatabasesJobArg<TEntity> argument);
    }
}
