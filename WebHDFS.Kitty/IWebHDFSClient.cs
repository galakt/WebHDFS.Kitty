using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WebHDFS.Kitty.DataModels;

namespace WebHDFS.Kitty
{
    public interface IWebHdfsClient
    {
        Task<bool> MakeDirectory(string path);

        Task UploadFile(string path, Stream fileStream);

        Task<IReadOnlyCollection<FileStatus>> ListDirectory(string path);

        Task<Stream> ReadFileStream(string path);
    }
}
