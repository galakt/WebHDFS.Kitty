using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebHDFS.Kitty.IntegrationTests
{
    public class ChangeTimes
    {
        private IWebHdfsClient client;

        [CheckConnStrSetupFact]
        public async Task SetTimes()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var filePath = $"{DataTestUtility.HdfsRootDir}/{nameof(SetTimes)}/sample";
            await client.Delete(filePath, Recursive: true);
            await client.UploadFile(filePath, File.OpenRead("Samples/SampleTextFile.txt"));

            await client.SetTimes(filePath, 12345678, 87654321);
            var fileStat = await client.GetFileStatus(filePath);
            Assert.True(fileStat.ModificationTime == 12345678 && fileStat.AccessTime == 87654321);
        }
    }
}

