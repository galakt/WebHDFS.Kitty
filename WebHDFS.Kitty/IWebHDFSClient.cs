﻿using System.Collections.Generic;
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

        Task<bool> Rename(string path, string destination);

        Task<bool> SetReplicationFactor(string path, short replication);

        Task SetPermission(string path, int permission);

        Task<string> CreateSnapshot(string path, string name = null);

        Task SetOwner(string path, string owner, string group = null);

        Task SetTimes(string path, int modificationtime = -1, int acesstime = -1);

        Task<string> GetDelegationToken(string path, string user, string service, string kind);

        Task<string> GetDelegationTokens(string path, string user);

        Task CheckAccess(string path, string fsaction);

        Task<long> RenewDelegstionToken(string path, string token);

        Task CreatesSymLink(string path, string destination, bool createParent = false);
    }
}
