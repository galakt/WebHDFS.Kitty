﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebHDFS.Kitty.IntegrationTests
{
    public class GetAllXAttrs
    {
        private IWebHdfsClient client;

        [CheckConnStrSetupFact]
        public async Task GetAllxAttr()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var filePath = $"{DataTestUtility.HdfsRootDir}/{nameof(GetAllxAttr)}/sample";
            await client.Delete(filePath, Recursive: true);
            await client.UploadFile(filePath, File.OpenRead("Samples/SampleTextFile.txt"));
            await client.SetXAttr(filePath, "user.attr", "get", "CREATE");

            var result = await client.GetAllXAttrs(filePath, "TEXT");
            Assert.True(result[0].Value == "\"get\"");
        }
    }
}
