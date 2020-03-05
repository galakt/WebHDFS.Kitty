using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebHDFS.Kitty.IntegrationTests
{
    public class CreatesSymLink
    {
        //private IWebHdfsClient client;

        //[CheckConnStrSetupFact]
        //public async Task MakeSymLink()
        //{
        //    client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
        //    var dirPath = $"{DataTestUtility.HdfsRootDir}/{nameof(MakeSymLink)}";
        //    await client.Delete(dirPath, Recursive: true);
        //    await client.UploadFile($"{dirPath}/sample", File.OpenRead("Samples/SampleTextFile.txt"));
        //    await client.MakeDirectory($"{dirPath}/result", "770");

        //    await client.CreatesSymLink($"{dirPath}", $"{dirPath}/result/");
        //    //var fileStat = await client.GetFileStatus($"{dirPath}/result/link");
        //    Assert.True(true);
        //}
    }
}
