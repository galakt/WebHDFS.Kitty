using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebHDFS.Kitty.IntegrationTests
{
    public class ListXAttrs
    {
        private IWebHdfsClient client;

        [CheckConnStrSetupFact]
        public async Task ListxAttr()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var filePath = $"{DataTestUtility.HdfsRootDir}/{nameof(ListxAttr)}/sample";
            await client.Delete(filePath, Recursive: true);
            await client.UploadFile(filePath, File.OpenRead("Samples/SampleTextFile.txt"));
            await client.SetXAttr(filePath, "user.attr", "get", "CREATE");

            var result = await client.ListXAttrs(filePath);
            Assert.True(result.Names == "[\"user.attr\"]");
        }
    }
}
