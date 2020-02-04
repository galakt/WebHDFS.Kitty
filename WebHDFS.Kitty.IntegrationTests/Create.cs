using System;
using Xunit;
using System.IO;
using System.Threading.Tasks;

namespace WebHDFS.Kitty.IntegrationTests
{
    public class Create
    {
        private IWebHdfsClient client;

        [CheckConnStrSetupFact]
        public async Task UploadFile()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            await client.UploadFile($"{DataTestUtility.HdfsRootDir}/sample", File.OpenRead("Samples/SampleTextFile.txt"));
        }
    }
}
