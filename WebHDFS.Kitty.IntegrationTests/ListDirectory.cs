using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebHDFS.Kitty.IntegrationTests
{
    [TestClass]
    public class ListDirectory
    {
        private IWebHdfsClient client;

        [TestInitialize]
        public void Init()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
        }

        [Ignore]
        [TestMethod]
        public async Task ListDir()
        {
            var result = await client.ListStatus(DataTestUtility.HdfsRootDir);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
        }
    }
}
