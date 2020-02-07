using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using WebHDFS.Kitty.DataModels;
using WebHDFS.Kitty.DataModels.RequestParams;
using WebHDFS.Kitty.DataModels.Responses;

namespace WebHDFS.Kitty
{
    public class WebHdfsClient : IWebHdfsClient
    {
        private readonly HttpClient _httpClient;

        public WebHdfsClient(string baseAddress = null, HttpClient httpClient = null)
        {
            _httpClient = httpClient ?? new HttpClient (new HttpClientHandler { UseDefaultCredentials = true, AllowAutoRedirect = false })
            {
                BaseAddress = new Uri(baseAddress ?? "")
            };
        }

        public async Task<Stream> OpenStream(string path, OpenParams requestParams)
        {
            var requestUri = $"/webhdfs/v1/{path.TrimStart('/')}?op=OPEN";
            if (requestParams.Offset !=null)
            {
                requestUri = requestUri + "&offset=" + requestParams.Offset;
            }
            if (requestParams.Length != null)
            {
                requestUri = requestUri + "&length=" + requestParams.Length;
            }
            if (requestParams.BufferSize != null)
            {
                requestUri = requestUri + "&buffersize=" + requestParams.BufferSize;
            }

            var initRequest = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var initResponse = await _httpClient.SendAsync(initRequest, HttpCompletionOption.ResponseHeadersRead);

            if (initResponse.StatusCode == HttpStatusCode.TemporaryRedirect)
            {
                var downloadRequest = new HttpRequestMessage(HttpMethod.Get, initResponse.Headers.Location);
                var downloadResponse = await _httpClient.SendAsync(downloadRequest, HttpCompletionOption.ResponseHeadersRead);
                if (!downloadResponse.IsSuccessStatusCode)
                {
                    var notSuccessDownloadResponseContent = await downloadResponse.Content.ReadAsStringAsync();
                    throw new InvalidOperationException($"Not success status code. Code={downloadResponse.StatusCode}. Content={notSuccessDownloadResponseContent}");
                }

                return await downloadResponse.Content.ReadAsStreamAsync();
            }
            else if (!initResponse.IsSuccessStatusCode)
            {
                var notSuccessInitResponseContent = await initResponse.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"Not success status code. Code={initResponse.StatusCode}. Content={notSuccessInitResponseContent}");
            }
            else
            {
                return await initResponse.Content.ReadAsStreamAsync();
            }
        }

        public async Task<FileStatus> GetFileStatus(string path)
        {
            return (await GetRequest<FileStatusResponse>($"/webhdfs/v1/{path.TrimStart('/')}?op=GETFILESTATUS")).FileStatus;
        }

        public async Task<IReadOnlyCollection<FileStatus>> ListStatus(string path)
        {
            return (await GetRequest<ListStatusResponse>($"/webhdfs/v1/{path.TrimStart('/')}?op=LISTSTATUS")).FileStatusCollection.FileStatuses;
        }

        public async Task<ContentSummaryResponse> GetContentSummary(string path)
        {
            return await GetRequest<ContentSummaryResponse>($"/webhdfs/v1/{path.TrimStart('/')}?op=GETCONTENTSUMMARY");
        }

        public async Task<FileChecksum> GetFileChecksum(string path)
        {
            var initRequest = new HttpRequestMessage(HttpMethod.Get, $"/webhdfs/v1/{path.TrimStart('/')}?op=GETFILECHECKSUM");
            var initResponse = await _httpClient.SendAsync(initRequest, HttpCompletionOption.ResponseHeadersRead);

            if (initResponse.StatusCode == HttpStatusCode.TemporaryRedirect)
            {
                var downloadRequest = new HttpRequestMessage(HttpMethod.Get, initResponse.Headers.Location);
                var downloadResponse = await _httpClient.SendAsync(downloadRequest, HttpCompletionOption.ResponseHeadersRead);
                if (!downloadResponse.IsSuccessStatusCode)
                {
                    var notSuccessDownloadResponseContent = await downloadResponse.Content.ReadAsStringAsync();
                    throw new InvalidOperationException($"Not success status code. Code={downloadResponse.StatusCode}. Content={notSuccessDownloadResponseContent}");
                }

                var content = await downloadResponse.Content.ReadAsStringAsync();
                var deserializedContent = JsonConvert.DeserializeObject<FileChecksumResponse>(content);
                return deserializedContent.Checksum;
            }
            else if (!initResponse.IsSuccessStatusCode)
            {
                var notSuccessInitResponseContent = await initResponse.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"Not success status code. Code={initResponse.StatusCode}. Content={notSuccessInitResponseContent}");
            }
            else
            {
                var content = await initResponse.Content.ReadAsStringAsync();
                var deserializedContent = JsonConvert.DeserializeObject<FileChecksumResponse>(content);
                return deserializedContent.Checksum;
            }
        }

        public async Task<string> GetHomeDirectory()
        {
            return (await GetRequest<HomeDirectoryResponse>($"/webhdfs/v1/?op=GETHOMEDIRECTORY")).Path;
        }
        
        public async Task<bool> MakeDirectory(string path, string permission = null)
        {
            var requestUri = $"/webhdfs/v1/{path.TrimStart('/')}?op=MKDIRS";
            if (!string.IsNullOrWhiteSpace(permission))
            {
                requestUri += "&permission=" + permission;
            }

            var request = new HttpRequestMessage(HttpMethod.Put, requestUri);
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var notSuccessContent = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"Not success status code. Code={response.StatusCode}. Content={notSuccessContent}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var deserializedContent = JsonConvert.DeserializeObject<MakeDirectoryResponse>(content);
            return deserializedContent.Boolean;
        }

        public async Task Delete(string path, bool Recursive = false)
        {
            var requestUri = $"/webhdfs/v1/{path.TrimStart('/')}?op=DELETE";
            if (Recursive != false)
            {
                requestUri = requestUri + "&recursive=true";
            }
            else
            {
                requestUri = requestUri + "&recursive=false";
            }
            var initRequest = new HttpRequestMessage(HttpMethod.Delete, requestUri);

            initRequest.Content = new StreamContent(Stream.Null);
            initRequest.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            var initResponse = await _httpClient.SendAsync(initRequest);
            if (!initResponse.IsSuccessStatusCode)
            {
                var notSuccessResponseContent = await initResponse.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"Not success status code. Code={initResponse.StatusCode}. Content={notSuccessResponseContent}");
            }
        }

        public async Task Append(string path, Stream fileStream, int? buffersize = null)
        {
            var requestUri = $"/webhdfs/v1/{path.TrimStart('/')}?op=APPEND";
            if (buffersize != null)
            {
                requestUri = requestUri + "&buffersize=" + buffersize;
            }
            var initRequest = new HttpRequestMessage(HttpMethod.Post, requestUri);
            initRequest.Content = new StreamContent(Stream.Null);
            initRequest.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            var initResponse = await _httpClient.SendAsync(initRequest);
            if (initResponse.StatusCode == HttpStatusCode.TemporaryRedirect)
            {
                var uploadRequest = new HttpRequestMessage(HttpMethod.Post, initResponse.Headers.Location);
                uploadRequest.Content = new StreamContent(fileStream);
                uploadRequest.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                var uploadResponse = await _httpClient.SendAsync(uploadRequest);
                uploadResponse.EnsureSuccessStatusCode();
            }
        }

        public async Task UploadFile(string path,
            Stream fileStream,
            bool Overwrite = false,
            int Permission = 755,
            short? Replication = null,
            long? BufferSize = null,
            long? BlockSize = null)
        {
            var requestUri = $"/webhdfs/v1/{path.TrimStart('/')}?op=CREATE&noredirect=false";
            if (Overwrite != false)
            {
                requestUri = requestUri + "&overwrite=true";
            }
            else
            {
                requestUri = requestUri + "&overwrite=false";
            }
            if (BlockSize != null)
            {
                requestUri = requestUri + "&blocksize=" + BlockSize;
            }
            if (Replication != null)
            {
                requestUri = requestUri + "&replication=" + Replication;
            }
            if (Permission != 755)
            {
                requestUri = requestUri + "&permission=" + Permission;
            }
            else
            {
                requestUri = requestUri + "&permission=" + Permission;
            }
            if (BufferSize != null)
            {
                requestUri = requestUri + "&buffersize=" + BufferSize;
            }

            var initRequest = new HttpRequestMessage(HttpMethod.Put, requestUri);

            initRequest.Content = new StreamContent(Stream.Null);
            initRequest.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            var initResponse = await _httpClient.SendAsync(initRequest);
            //var headers = initResponse.Headers;
            //var content = initResponse.Content.ReadAsStringAsync().Result;
            if (initResponse.StatusCode == HttpStatusCode.TemporaryRedirect)
            {
                var uploadRequest = new HttpRequestMessage(HttpMethod.Put, initResponse.Headers.Location);
                uploadRequest.Content = new StreamContent(fileStream);
                uploadRequest.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                var uploadResponse = await _httpClient.SendAsync(uploadRequest);
                uploadResponse.EnsureSuccessStatusCode();
            }
        }

        private async Task<TResult> GetRequest<TResult>(string requestUri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var notSuccessResponseContent = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"Not success status code. Code={response.StatusCode}. Content={notSuccessResponseContent}");
            }
            var content = await response.Content.ReadAsStringAsync();
            var deserializedContent = JsonConvert.DeserializeObject<TResult>(content);
            return deserializedContent;
        }
    }
}
