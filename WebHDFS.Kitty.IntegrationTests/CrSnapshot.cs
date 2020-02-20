using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebHDFS.Kitty.IntegrationTests
{
    public class CrSnapshot
    {
        private IWebHdfsClient client;

        [Fact(Skip = "Admin required")]
        public async Task CreateSnapshot()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var dirPath = $"{DataTestUtility.HdfsRootDir}/{nameof(CreateSnapshot)}/";
            await client.MakeDirectory(dirPath, "770");


            var result = await client.CreateSnapshot(dirPath);
            var fileStat = await client.GetFileStatus($"{dirPath}SNAPSHOT");
            Assert.True(false);
        }
    }
}