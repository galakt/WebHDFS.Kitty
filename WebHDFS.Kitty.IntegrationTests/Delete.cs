using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace WebHDFS.Kitty.IntegrationTests
{

    public class Delete
    {
        private IWebHdfsClient client;

        [CheckConnStrSetupFact]
        public async Task Remove()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            await client.Delete($"{DataTestUtility.HdfsRootDir}/sample2");

        }

    }
}
