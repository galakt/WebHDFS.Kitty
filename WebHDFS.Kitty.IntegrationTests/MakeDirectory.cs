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
        public async Task CreateNewDir2()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var result = await client.MakeDirectory($"{DataTestUtility.HdfsRootDir}/MakeDirTest", "770");
            Assert.True(result);
        }
    }
}
