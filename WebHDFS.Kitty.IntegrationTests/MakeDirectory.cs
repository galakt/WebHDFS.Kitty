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
            var result = await client.MakeDirectory($"{DataTestUtility.HdfsRootDir}/MakeDirTest", "770");
            var listStatus = await client.ListStatus($"{DataTestUtility.HdfsRootDir}");
            foreach (var status in listStatus)
            {
                Assert.False(status.PathSuffix == "MakeDirTest" && status.Type == "Directory" && status.Permission == "770");
            }
        }
    }
}
