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
            await client.UploadFile($"{DataTestUtility.HdfsRootDir}/sample", File.OpenRead("Samples/SampleTextFile.txt"));

            var fileStat = await client.GetFileStatus($"{DataTestUtility.HdfsRootDir}/sample");
            Assert.True(fileStat.Length == 11);
            Assert.True(fileStat.Type == "FILE");
            Assert.True(fileStat.Permission == "770");
        }

        [CheckConnStrSetupFact]
        public async Task GetStatusOfDirectory()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);

            var fileStat = await client.GetFileStatus($"{DataTestUtility.HdfsRootDir}");
            Assert.True(fileStat.Length == 0);
            Assert.True(fileStat.Type == "DIRECTORY");
            Assert.True(fileStat.Permission == "755");
        }
    }
}
