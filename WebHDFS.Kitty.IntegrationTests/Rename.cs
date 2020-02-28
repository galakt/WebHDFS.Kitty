using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebHDFS.Kitty.IntegrationTests
{
    public class Rename
    {
        private IWebHdfsClient client;

        [CheckConnStrSetupFact]
        public async Task RenameTest()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var dirPath = $"{DataTestUtility.HdfsRootDir}/{nameof(RenameTest)}";
            await client.Delete(dirPath, Recursive:true);
            await client.UploadFile($"{dirPath}/sample", File.OpenRead("Samples/SampleTextFile.txt"));

            var result = await client.Rename($"{dirPath}/sample", $"{dirPath}/notsample");
            Assert.True(result);
        }
    }
}
