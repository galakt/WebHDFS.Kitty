using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebHDFS.Kitty.DataModels.RequestParams;

namespace WebHDFS.Kitty.IntegrationTests
{
    [TestClass]
    public class Open
    {
        private IWebHdfsClient client;

        [TestInitialize]
        public void Init()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
        }

        [Ignore]
        [TestMethod]
        public async Task ReadFileStream()
        {
            var result = await client.OpenStream($"{DataTestUtility.HdfsRootDir}/sample", new OpenParams());
            using (StreamReader r = new StreamReader(result))
            {
                var c = r.ReadToEnd();
            }
            Assert.IsNotNull(result);
        }

        [Ignore]
        [TestMethod]
        public async Task ReadBigFile()
        {
            var result = await client.OpenStream($"{DataTestUtility.HdfsRootDir}/part.0", new OpenParams());

            string firstLine = null;
            using (StreamReader r = new StreamReader(result))
            {
                firstLine = r.ReadLine();
            }

            Assert.IsNotNull(result);
            Assert.IsFalse(string.IsNullOrWhiteSpace(firstLine));
        }
    }
}
