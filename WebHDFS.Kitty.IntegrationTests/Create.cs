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
            await client.Delete($"{DataTestUtility.HdfsRootDir}/sample2");

            await client.UploadFile($"{DataTestUtility.HdfsRootDir}/sample2", File.OpenRead("Samples/SampleTextFile.txt"));
            var fileStat = await client.GetFileStatus($"{DataTestUtility.HdfsRootDir}/sample2");
            Assert.True(fileStat.Type == "FILE");
            Assert.True(fileStat.Permission == "755");
        }

        [CheckConnStrSetupFact]
        public async Task UploadFileWithOverWrite()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            await client.UploadFile($"{DataTestUtility.HdfsRootDir}/sample2", File.OpenRead("Samples/SampleTextFile.txt"));
            var fileStat = await client.GetFileStatus($"{DataTestUtility.HdfsRootDir}/sample2");
            var oldLenght = fileStat.Length;

            await client.UploadFile($"{DataTestUtility.HdfsRootDir}/sample2", File.OpenRead("Samples/SampleTextFile2.txt"), Overwrite: true);
            fileStat = await client.GetFileStatus($"{DataTestUtility.HdfsRootDir}/sample2");
            Assert.True(fileStat.Length != oldLenght);
        }

        [CheckConnStrSetupFact]
        public async Task UploadFileWithNoOverWrite()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            await client.UploadFile($"{DataTestUtility.HdfsRootDir}/UploadFileWithNoOverWrite/sample2", File.OpenRead("Samples/SampleTextFile2.txt"), Overwrite: true);

            await Assert.ThrowsAsync<System.Net.Http.HttpRequestException>(() => client.UploadFile($"{DataTestUtility.HdfsRootDir}/UploadFileWithNoOverWrite/sample2", File.OpenRead("Samples/SampleTextFile.txt"), Overwrite: false));
        }

        [CheckConnStrSetupFact]
        public async Task UploadFileWithPermission()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            await client.UploadFile($"{DataTestUtility.HdfsRootDir}/sample2", File.OpenRead("Samples/SampleTextFile.txt"), Permission: 700, Overwrite: true);
            var fileStat = await client.GetFileStatus($"{DataTestUtility.HdfsRootDir}/sample2");
            Assert.True(fileStat.Type == "FILE");
            Assert.True(fileStat.Permission == "700");
        }

        [CheckConnStrSetupFact]
        public async Task UploadFileWithReplication()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            await client.UploadFile($"{DataTestUtility.HdfsRootDir}/sample2", File.OpenRead("Samples/SampleTextFile.txt"), Replication: 3);
            var fileStat = await client.GetFileStatus($"{DataTestUtility.HdfsRootDir}/sample2");
            Assert.True(fileStat.Replication == 3);
        }
    }
}
