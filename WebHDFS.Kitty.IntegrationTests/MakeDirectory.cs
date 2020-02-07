using System.Threading.Tasks;
using Xunit;

namespace WebHDFS.Kitty.IntegrationTests
{
    public class MakeDirectory
    {
        private IWebHdfsClient client;

        [CheckConnStrSetupFact]
        public async Task CreateNewDir()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var dirPath = $"{DataTestUtility.HdfsRootDir}/{nameof(CreateNewDir)}/MakeDirTest";

            var result = await client.MakeDirectory(dirPath, "770");
            Assert.True(result);
        }

        [CheckConnStrSetupFact]
        public async Task CreateNewDirWithControl()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var dirPath = $"{DataTestUtility.HdfsRootDir}/{nameof(CreateNewDirWithControl)}/MakeDirTest";
            await client.Delete(dirPath, Recursive: true);

            var result = await client.MakeDirectory(dirPath, "770");
            var dirStatus = await client.GetFileStatus(dirPath);
            Assert.True(dirStatus.Type == "DIRECTORY" && dirStatus.Permission == "770");
            Assert.True(result);
        }
    }
}
