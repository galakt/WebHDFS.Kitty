using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WebHDFS.Kitty.DataModels;
using WebHDFS.Kitty.DataModels.Responses;

namespace WebHDFS.Kitty
{
    public interface IWebHdfsClient
    {
        Task<Stream> OpenStream(string path);

        Task<FileStatus> GetFileStatus(string path);

        Task<IReadOnlyCollection<FileStatus>> ListStatus(string path);

        Task<ContentSummaryResponse> GetContentSummary(string path);

        Task<FileChecksum> GetFileChecksum(string path);

        Task<string> GetHomeDirectory();
        
        Task<bool> MakeDirectory(string path);

        Task UploadFile(string path, Stream fileStream);
    }
}
