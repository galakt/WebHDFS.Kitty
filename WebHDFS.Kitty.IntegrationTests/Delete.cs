using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.IO;


namespace WebHDFS.Kitty.IntegrationTests
{

    public class Delete
    {
        private IWebHdfsClient client;

        [CheckConnStrSetupFact]
        public async Task Remove()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var filePath = $"{DataTestUtility.HdfsRootDir}/{nameof(Remove)}/sample2";
            await client.UploadFile(filePath, File.OpenRead("Samples/SampleTextFile.txt"), Overwrite: true);

            await client.Delete(filePath);
            await Assert.ThrowsAsync<System.InvalidOperationException>(() => client.GetFileStatus(filePath));
        }

        [CheckConnStrSetupFact]
        public async Task RemoveNonexistentFile()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var filePath = $"{DataTestUtility.HdfsRootDir}/{nameof(RemoveNonexistentFile)}/sample2";
            await client.Delete(filePath);

            await client.Delete(filePath);
            await Assert.ThrowsAsync<System.InvalidOperationException>(() => client.GetFileStatus(filePath));

        }

        [CheckConnStrSetupFact]
        public async Task RemoveDirectory()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var filePath = $"{DataTestUtility.HdfsRootDir}/{nameof(RemoveDirectory)}/";
            await client.MakeDirectory(filePath, "770");

            await client.Delete(filePath,Recursive: true);
            await Assert.ThrowsAsync<System.InvalidOperationException>(() => client.GetFileStatus(filePath));
        }
    }
}
