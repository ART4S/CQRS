using System;
using System.Data;
using System.Threading.Tasks;
using WebFeatures.Application.Features.Files.DownloadFile;
using WebFeatures.Application.Interfaces.DataAccess.Repositories.Reading;
using WebFeatures.Infrastructure.DataAccess.Executors;

namespace WebFeatures.Infrastructure.DataAccess.Repositories.Reading
{
    internal class FileReadRepository : ReadRepository, IFileReadRepository
    {
        public FileReadRepository(IDbConnection connection, IDbExecutor executor) : base(connection, executor)
        {
        }

        public async Task<FileDownloadDto> GetAsync(Guid id)
        {
            FileDownloadDto file = await Executor.QuerySingleOrDefaultAsync<FileDownloadDto>(
                connection: Connection,
                sql: "get_file",
                param: new { file_id = id },
                commandType: CommandType.StoredProcedure);

            return file;
        }
    }
}
