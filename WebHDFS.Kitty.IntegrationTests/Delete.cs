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
            await client.UploadFile($"{DataTestUtility.HdfsRootDir}/sample2", File.OpenRead("Samples/SampleTextFile.txt"), Overwrite: true);

            await client.Delete($"{DataTestUtility.HdfsRootDir}/sample2");
            await Assert.ThrowsAsync<System.InvalidOperationException>(() => client.GetFileStatus($"{DataTestUtility.HdfsRootDir}/sample2"));
        }

        [CheckConnStrSetupFact]
        public async Task RemoveNonexistentFile()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            await client.Delete($"{DataTestUtility.HdfsRootDir}/sample2");

            await client.Delete($"{DataTestUtility.HdfsRootDir}/sample2");
            await Assert.ThrowsAsync<System.InvalidOperationException>(() => client.GetFileStatus($"{DataTestUtility.HdfsRootDir}/sample2"));

        }

        [CheckConnStrSetupFact]
        public async Task RemoveDirectory()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            await client.MakeDirectory($"{DataTestUtility.HdfsRootDir}/DirForDelete/SampleFile", "770");

            await client.Delete($"{DataTestUtility.HdfsRootDir}/DirForDelete",Recursive: true);
            await Assert.ThrowsAsync<System.InvalidOperationException>(() => client.GetFileStatus($"{DataTestUtility.HdfsRootDir}/DirForDelete"));
        }
    }
}
