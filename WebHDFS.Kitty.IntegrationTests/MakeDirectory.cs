using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebHDFS.Kitty.IntegrationTests
{
    [TestClass]
    public class MakeDirectory
    {
        private IWebHdfsClient client;

        [TestInitialize]
        public void Init()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
        }

        [Ignore]
        [TestMethod]
        public async Task CreateNewDir()
        {
            var result = await client.MakeDirectory($"{DataTestUtility.HdfsRootDir}/MakeDirTest", "770");
            Assert.IsTrue(result);
        }

        [Ignore]
        [TestMethod]
        public async Task CreateNewDir2()
        {
            var result = await client.MakeDirectory($"{DataTestUtility.HdfsRootDir}/MakeDirTest", "770");
            Assert.IsTrue(result);
        }
    }
}
