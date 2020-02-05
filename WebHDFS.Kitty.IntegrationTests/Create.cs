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
            await client.UploadFile($"{DataTestUtility.HdfsRootDir}/sample2", File.OpenRead("Samples/SampleTextFile.txt"));
            var FileStat = await client.GetFileStatus($"{DataTestUtility.HdfsRootDir}/sample2");
            Assert.True(FileStat.Type == "FILE");
            Assert.True(FileStat.Permission == "755");
        }

        [CheckConnStrSetupFact]
        public async Task UploadFileWithOverWrite()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var FileStat = await client.GetFileStatus($"{DataTestUtility.HdfsRootDir}/sample2");
            var oldLenght = FileStat.Length;

            await client.UploadFile($"{DataTestUtility.HdfsRootDir}/sample2", File.OpenRead("Samples/SampleTextFile2.txt"), Overwrite: true);
            FileStat = await client.GetFileStatus($"{DataTestUtility.HdfsRootDir}/sample2");
            Assert.True(FileStat.Length != oldLenght);
        }

        [CheckConnStrSetupFact]
        public async Task UploadFileWith_No_OverWrite()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            await client.Delete($"{DataTestUtility.HdfsRootDir}/sample2");

            await client.UploadFile($"{DataTestUtility.HdfsRootDir}/sample2", File.OpenRead("Samples/SampleTextFile.txt"), Overwrite: false);
            var FileStat = await client.GetFileStatus($"{DataTestUtility.HdfsRootDir}/sample2");
            Assert.True(FileStat.Length != 0);
        }

        [CheckConnStrSetupFact]
        public async Task UploadFileWithPermission()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            await client.UploadFile($"{DataTestUtility.HdfsRootDir}/sample2", File.OpenRead("Samples/SampleTextFile.txt"), Permission: 750);
            var FileStat = await client.GetFileStatus($"{DataTestUtility.HdfsRootDir}/sample2");
            Assert.True(FileStat.Type == "FILE");
            Assert.True(FileStat.Permission == "750");
        }

        [CheckConnStrSetupFact]
        public async Task UploadFileWithReplication()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            await client.UploadFile($"{DataTestUtility.HdfsRootDir}/sample2", File.OpenRead("Samples/SampleTextFile.txt"), Replication: 3);
            var FileStat = await client.GetFileStatus($"{DataTestUtility.HdfsRootDir}/sample2");
            Assert.True(FileStat.Replication == 3);
        }
    }
}
