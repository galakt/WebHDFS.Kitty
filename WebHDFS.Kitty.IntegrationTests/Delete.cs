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
            await client.UploadFile($"{DataTestUtility.HdfsRootDir}/sample2", File.OpenRead("Samples/SampleTextFile.txt"));

            await client.Delete($"{DataTestUtility.HdfsRootDir}/sample2");
            var listStatus = await client.ListStatus($"{DataTestUtility.HdfsRootDir}");
            foreach (var status in listStatus)
            {
                Assert.False(status.PathSuffix == "sample2" && status.Type == "FILE");
            }
        }

        [CheckConnStrSetupFact]
        public async Task RemoveNonexistentFile()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            await client.Delete($"{DataTestUtility.HdfsRootDir}/sample2");

            await client.Delete($"{DataTestUtility.HdfsRootDir}/sample2");
            var listStatus = await client.ListStatus($"{DataTestUtility.HdfsRootDir}");
            foreach (var status in listStatus)
            {
                Assert.False(status.PathSuffix == "sample2" && status.Type == "FILE");
            }
        }

    }
}
