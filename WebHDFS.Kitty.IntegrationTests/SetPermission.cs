using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebHDFS.Kitty.IntegrationTests
{
    public class SetPermission
    {
        private IWebHdfsClient client;

        [CheckConnStrSetupFact]
        public async Task SetPermissOfFile()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var filePath = $"{DataTestUtility.HdfsRootDir}/{nameof(SetPermissOfFile)}/sample";
            await client.Delete(filePath, Recursive: true);
            await client.UploadFile(filePath, File.OpenRead("Samples/SampleTextFile.txt"), Permission: 750);

            await client.SetPermission(filePath, 700);
            var fileStat = await client.GetFileStatus(filePath);
            Assert.True(fileStat.Permission == "700");
        }

        [CheckConnStrSetupFact]
        public async Task SetPermissOfDirectory()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var dirPath = $"{DataTestUtility.HdfsRootDir}/{nameof(SetPermissOfDirectory)}";
            await client.Delete(dirPath, Recursive: true);
            await client.MakeDirectory(dirPath, "750");

            await client.SetPermission(dirPath, 700);
            var fileStat = await client.GetFileStatus(dirPath);
            Assert.True(fileStat.Permission == "700");
        }
    }
}
