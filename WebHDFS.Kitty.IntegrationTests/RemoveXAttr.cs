using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebHDFS.Kitty.IntegrationTests
{
    public class RemoveXAttr
    {
        private IWebHdfsClient client;

        [CheckConnStrSetupFact]
        public async Task RmxAttr()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var filePath = $"{DataTestUtility.HdfsRootDir}/{nameof(RmxAttr)}/sample";
            await client.Delete(filePath, Recursive: true);
            await client.UploadFile(filePath, File.OpenRead("Samples/SampleTextFile.txt"));
            await client.SetXAttr(filePath, "user.attr", "value", "CREATE");

            await client.RemoveXAttr(filePath, "user.attr");
            var result = await client.GetAllXAttrs(filePath, "TEXT");
            Assert.True(result.Length == 0);
        }
    }
}
