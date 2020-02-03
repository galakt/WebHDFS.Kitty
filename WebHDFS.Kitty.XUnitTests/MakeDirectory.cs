using System.Threading.Tasks;
using Xunit;

namespace WebHDFS.Kitty.XUnitTests
{
    public class MakeDirectory
    {
        private IWebHdfsClient client;

        [DataTestUtilityControl]
        public async Task CreateNewDir()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var result = await client.MakeDirectory($"{DataTestUtility.HdfsRootDir}/MakeDirTest", "770");
            Assert.True(result);
        }

        [DataTestUtilityControl]
        public async Task CreateNewDir2()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var result = await client.MakeDirectory($"{DataTestUtility.HdfsRootDir}/MakeDirTest", "770");
            Assert.True(result);
        }
    }
}
