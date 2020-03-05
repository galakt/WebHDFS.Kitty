using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace WebHDFS.Kitty.IntegrationTests
{
    public class Append
    {
        private IWebHdfsClient client;

        [CheckConnStrSetupFact]
        public async Task AppendTest()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var dirPath = $"{DataTestUtility.HdfsRootDir}/{nameof(AppendTest)}";
            await client.Delete(dirPath, Recursive: true);
            await client.UploadFile($"{dirPath}/sample", File.OpenRead("Samples/SampleTextFile.txt"), Overwrite: true);

            await client.Append($"{dirPath}/sample", File.OpenRead("Samples/SampleTextFile2.txt"));
            var fileStat = await client.GetFileStatus($"{dirPath}/sample");
            Assert.True(fileStat.Length == 12);
        }
    }
}
