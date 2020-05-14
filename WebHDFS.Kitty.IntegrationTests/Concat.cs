using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebHDFS.Kitty.IntegrationTests
{
    public class Concat
    {
        private IWebHdfsClient client;

        [Fact(Skip = "Not full Blocks")]
        public async Task ConcatTest()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var dirPath = $"{DataTestUtility.HdfsRootDir}/{nameof(ConcatTest)}";
            await client.Delete(dirPath, Recursive: true);
            await client.UploadFile($"{dirPath}/sample", File.OpenRead("Samples/SampleTextFile.txt"));
            await client.UploadFile($"{dirPath}/sample2", File.OpenRead("Samples/SampleTextFile.txt"));

            await client.Concat($"{dirPath}/sample", $"{dirPath}/sample2");
            var fileStat = await client.GetFileStatus($"{dirPath}/sample");
            Assert.True(fileStat.Length == 12);
        }
    }
}
