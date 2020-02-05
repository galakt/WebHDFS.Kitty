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
            var FileStat = await client.GetFileStatus($"{DataTestUtility.HdfsRootDir}/sample");
            Assert.True(FileStat.Length == 11);
            Assert.True(FileStat.Type == "FILE");
            Assert.True(FileStat.Permission == "770");
        }

        [CheckConnStrSetupFact]
        public async Task GetStatusOfDirectory()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var FileStat = await client.GetFileStatus($"{DataTestUtility.HdfsRootDir}");
            Assert.True(FileStat.Length == 0);
            Assert.True(FileStat.Type == "DIRECTORY");
            Assert.True(FileStat.Permission == "755");
        }
    }
}
