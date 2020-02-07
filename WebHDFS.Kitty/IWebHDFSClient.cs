using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WebHDFS.Kitty.DataModels;
using WebHDFS.Kitty.DataModels.RequestParams;
using WebHDFS.Kitty.DataModels.Responses;

namespace WebHDFS.Kitty
{
    public interface IWebHdfsClient
    {
        Task<Stream> OpenStream(string path, OpenParams requestParams);

        Task<FileStatus> GetFileStatus(string path);

        Task<IReadOnlyCollection<FileStatus>> ListStatus(string path);

        Task<ContentSummaryResponse> GetContentSummary(string path);

        Task<FileChecksum> GetFileChecksum(string path);

        Task<string> GetHomeDirectory();
        
        Task<bool> MakeDirectory(string path, string permission);

        Task UploadFile(string path, Stream fileStream, bool Overwrite = false, int Permission = 755, short? Replication = null, long? BufferSize = null, long? BlockSize = null);

        Task Delete(string path, bool Recursive = false);

        Task Append(string path, Stream fileStream, int? buffersize = null);

    }
}
