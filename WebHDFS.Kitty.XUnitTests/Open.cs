using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using WebHDFS.Kitty.DataModels.RequestParams;

namespace WebHDFS.Kitty.XUnitTests
{
   public class Open
   {
        private IWebHdfsClient client;

        [DataTestUtilityControl]
        public async Task ReadFileStream()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var result = await client.OpenStream($"{DataTestUtility.HdfsRootDir}/sample", new OpenParams());
            using (StreamReader r = new StreamReader(result))
            {
                var c = r.ReadToEnd();
            }
            Assert.NotNull(result);
        }

        [DataTestUtilityControl]
        public async Task ReadBigFile()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var result = await client.OpenStream($"{DataTestUtility.HdfsRootDir}/part.0", new OpenParams());
            string firstLine = null;
            using (StreamReader r = new StreamReader(result))
            {
                firstLine = r.ReadLine();
            }
            Assert.NotNull(result);
            Assert.False(string.IsNullOrWhiteSpace(firstLine));
        }

   }
}
