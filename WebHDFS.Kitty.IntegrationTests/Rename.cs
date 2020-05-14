﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebHDFS.Kitty.IntegrationTests
{
    public class Rename
    {
        private IWebHdfsClient client;

        [CheckConnStrSetupFact]
        public async Task RenameTest()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var dirPath = $"{DataTestUtility.HdfsRootDir}/{nameof(RenameTest)}";
            await client.Delete(dirPath, Recursive:true);
            await client.UploadFile($"{dirPath}/sample", File.OpenRead("Samples/SampleTextFile.txt"));

            var fileStat = await client.GetFileStatus($"{dirPath}/sample");
            if (fileStat.Type == "FILE")
            {
                var result = await client.Rename($"{dirPath}/sample", $"{dirPath}/notsample");
                fileStat = await client.GetFileStatus($"{dirPath}/notsample");
                Assert.True(fileStat.Type == "FILE");
                Assert.True(result);
            }
            else { Assert.True(false); }
        }
    }
}
