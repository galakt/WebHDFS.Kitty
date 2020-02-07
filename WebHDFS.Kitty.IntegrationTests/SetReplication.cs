using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebHDFS.Kitty.IntegrationTests
{
    public class SetReplication
    {
        private IWebHdfsClient client;

        [CheckConnStrSetupFact]
        public async Task SetReplic()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var filePath = $"{DataTestUtility.HdfsRootDir}/{nameof(SetReplic)}/sample";
            await client.Delete(filePath, Recursive: true);
            await client.UploadFile(filePath, File.OpenRead("Samples/SampleTextFile.txt"), Replication: 5);

            var result = await client.SetReplicationFactor(filePath, 7);
            var fileStat = await client.GetFileStatus(filePath);
            Assert.True(fileStat.Replication == 7);
            Assert.True(result);
        }
    }
}
