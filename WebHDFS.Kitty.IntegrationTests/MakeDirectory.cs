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
            var result = await client.MakeDirectory($"{DataTestUtility.HdfsRootDir}/MakeDirTest", "770");
            Assert.True(result);
        }

        [CheckConnStrSetupFact]
        public async Task CreateNewDirWithControl()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            await client.Delete($"{DataTestUtility.HdfsRootDir}/MakeDirTest", Recursive: true);
            var result = await client.MakeDirectory($"{DataTestUtility.HdfsRootDir}/MakeDirTest", "770");
            var dirStatus = await client.GetFileStatus($"{DataTestUtility.HdfsRootDir}/MakeDirTest");
            Assert.True(dirStatus.Type == "DIRECTORY" && dirStatus.Permission == "770");
            Assert.True(result);
        }
    }
}
