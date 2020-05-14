using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebHDFS.Kitty.IntegrationTests
{
    public class CheckAccess
    {
        //private IWebHdfsClient client;

        //[CheckConnStrSetupFact]
        //public async Task Access()
        //{
        //    client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
        //    var filePath = $"{DataTestUtility.HdfsRootDir}/{nameof(Access)}/sample";
        //    await client.Delete(filePath, Recursive: true);
        //    await client.UploadFile(filePath, File.OpenRead("Samples/SampleTextFile.txt"));

        //    var result = await client.CheckAccess(filePath, "rwx");
        //    Assert.True(result);
        //}
    }
}
