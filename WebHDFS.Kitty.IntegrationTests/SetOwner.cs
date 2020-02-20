using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebHDFS.Kitty.IntegrationTests
{
    public class SetOwner
    {
        private IWebHdfsClient client;

        [Fact(Skip ="Manual test")]
        public async Task SetOwnerOfFile()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var filePath = $"{DataTestUtility.HdfsRootDir}/{nameof(SetOwnerOfFile)}/sample";
            await client.Delete(filePath);
            await client.UploadFile(filePath, File.OpenRead("Samples/SampleTextFile.txt"));

            await client.SetOwner(filePath, "USER");
            var fileStat = await client.GetFileStatus(filePath);
            Assert.True(fileStat.Owner == "USER");
        }
    }
}
