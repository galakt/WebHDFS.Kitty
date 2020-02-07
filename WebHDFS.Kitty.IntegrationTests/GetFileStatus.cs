using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using WebHDFS.Kitty.DataModels.RequestParams;
using Xunit;

namespace WebHDFS.Kitty.IntegrationTests
{
    public class GetFileStatus
    {
        private IWebHdfsClient client;

        [CheckConnStrSetupFact]
        public async Task GetStatusOfFile()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var filePath = $"{DataTestUtility.HdfsRootDir}/{nameof(GetStatusOfFile)}/sample";
            await client.UploadFile(filePath, File.OpenRead("Samples/SampleTextFile.txt"), Permission: 770, Overwrite: true);

            var fileStat = await client.GetFileStatus(filePath);
            Assert.True(fileStat.Length == 9);
            Assert.True(fileStat.Type == "FILE");
            Assert.True(fileStat.Permission == "770");
        }

        [CheckConnStrSetupFact]
        public async Task GetStatusOfDirectory()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var dirPath = $"{DataTestUtility.HdfsRootDir}/{nameof(GetStatusOfDirectory)}";
            await client.MakeDirectory(dirPath, "755");

            var fileStat = await client.GetFileStatus(dirPath);
            Assert.True(fileStat.Length == 0);
            Assert.True(fileStat.Type == "DIRECTORY");
            Assert.True(fileStat.Permission == "755");
        }
    }
}
