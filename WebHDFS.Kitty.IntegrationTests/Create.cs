using System;
using Xunit;
using System.IO;
using System.Threading.Tasks;

namespace WebHDFS.Kitty.IntegrationTests
{
    public class Create
    {
        private IWebHdfsClient client;

        [CheckConnStrSetupFact]
        public async Task UploadFileWithoutParams()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var filePath = $"{DataTestUtility.HdfsRootDir}/{nameof(UploadFileWithoutParams)}/sample2";
            await client.Delete(filePath);

            await client.UploadFile(filePath, File.OpenRead("Samples/SampleTextFile.txt"));
            var fileStat = await client.GetFileStatus(filePath);
            Assert.True(fileStat.Type == "FILE");
            Assert.True(fileStat.Permission == "755");
        }

        [CheckConnStrSetupFact]
        public async Task UploadFileWithOverWrite()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var filePath = $"{DataTestUtility.HdfsRootDir}/{nameof(UploadFileWithOverWrite)}/sample2";
            await client.UploadFile(filePath, File.OpenRead("Samples/SampleTextFile.txt"), Overwrite: true);
            var fileStat = await client.GetFileStatus(filePath);
            var oldLenght = fileStat.Length;

            await client.UploadFile(filePath, File.OpenRead("Samples/SampleTextFile2.txt"), Overwrite: true);
            fileStat = await client.GetFileStatus(filePath);
            Assert.True(fileStat.Length != oldLenght);
        }

        [CheckConnStrSetupFact]
        public async Task UploadFileWithNoOverWrite()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var filePath = $"{DataTestUtility.HdfsRootDir}/{nameof(UploadFileWithNoOverWrite)}/sample2";

            await client.UploadFile(filePath, File.OpenRead("Samples/SampleTextFile2.txt"), Overwrite: true);
            await Assert.ThrowsAsync<System.Net.Http.HttpRequestException>(() => client.UploadFile(filePath, File.OpenRead("Samples/SampleTextFile.txt"), Overwrite: false));
        }

        [CheckConnStrSetupFact]
        public async Task UploadFileWithPermission()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var filePath = $"{DataTestUtility.HdfsRootDir}/{nameof(UploadFileWithPermission)}/sample2";
            await client.UploadFile(filePath, File.OpenRead("Samples/SampleTextFile.txt"), Permission: 700, Overwrite: true);

            var fileStat = await client.GetFileStatus(filePath);
            Assert.True(fileStat.Type == "FILE");
            Assert.True(fileStat.Permission == "700");
        }

        [CheckConnStrSetupFact]
        public async Task UploadFileWithReplication()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var filePath = $"{DataTestUtility.HdfsRootDir}/{nameof(UploadFileWithReplication)}/sample2";

            await client.UploadFile(filePath, File.OpenRead("Samples/SampleTextFile.txt"), Overwrite: true, Replication: 3);
            var fileStat = await client.GetFileStatus(filePath);
            Assert.True(fileStat.Replication == 3);
        }
    }
}
