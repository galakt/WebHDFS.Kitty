using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebHDFS.Kitty.IntegrationTests
{
    [TestClass]
    public class Create
    {
        private IWebHdfsClient client;

        [TestInitialize]
        public void Init()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
        }

        [Ignore]
        [TestMethod]
        public async Task UploadFile()
        {
            await client.UploadFile($"{DataTestUtility.HdfsRootDir}/sample", File.OpenRead("Samples/SampleTextFile.txt"));
        }
    }
}
