using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using WebHDFS.Kitty.DataModels.RequestParams;

namespace WebHDFS.Kitty.IntegrationTests
{
    public class Open
    {
        private IWebHdfsClient client;

        [CheckConnStrSetupFact]
        public async Task ReadFileStream()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var filePath = $"{DataTestUtility.HdfsRootDir}/{nameof(ReadFileStream)}/sample";
            await client.UploadFile(filePath, File.OpenRead("Samples/SampleTextFile.txt"), Overwrite: true);

            var openResponse = await client.OpenStream(filePath, new OpenParams());
            using (StreamReader reader = new StreamReader(openResponse))
            {
                var openResult = reader.ReadToEnd();
                Assert.True("Some text" == openResult);
            }
        }

        [CheckConnStrSetupFact]
        public async Task ReadBigFile()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var openResponse = await client.OpenStream($"{DataTestUtility.HdfsRootDir}/part.0", new OpenParams());
            string firstLine = null;
            using (StreamReader reader = new StreamReader(openResponse))
            {
                firstLine = reader.ReadLine();
            }
            Assert.NotNull(openResponse);
            Assert.False(string.IsNullOrWhiteSpace(firstLine));
        }

        [CheckConnStrSetupFact]
        public async Task ReadWithLenght()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var filePath = $"{DataTestUtility.HdfsRootDir}/{nameof(ReadWithLenght)}/sample";
            await client.Delete($"{DataTestUtility.HdfsRootDir}/sample");
            await client.UploadFile(filePath, File.OpenRead("Samples/SampleTextFile.txt"), Overwrite: true);

            var openResponse = await client.OpenStream(filePath, new OpenParams() { Length = 2 });
            using (StreamReader reader = new StreamReader(openResponse))
            {
                var openResult = reader.ReadToEnd();
                Assert.True("So" == openResult);
            }
        }

        [CheckConnStrSetupFact]
        public async Task ReadWithOffset()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var filePath = $"{DataTestUtility.HdfsRootDir}/{nameof(ReadWithOffset)}/sample";
            await client.UploadFile(filePath, File.OpenRead("Samples/SampleTextFile.txt"), Overwrite: true);

            var openResponse = await client.OpenStream(filePath, new OpenParams() { Offset = 2 });
            using (StreamReader reader = new StreamReader(openResponse))
            {
                var openResult = reader.ReadToEnd();
                Assert.Equal("me text", openResult);
            }
        }

        [CheckConnStrSetupFact]
        public async Task ReadWithOffsetWithLenght()
        {
            client = new WebHdfsClient(DataTestUtility.HdfsConnStr);
            var filePath = $"{DataTestUtility.HdfsRootDir}/{nameof(ReadWithOffsetWithLenght)}/sample";
            await client.UploadFile(filePath, File.OpenRead("Samples/SampleTextFile.txt"), Overwrite: true);

            var openResponse = await client.OpenStream(filePath, new OpenParams() { Offset = 2, Length = 2 });
            using (StreamReader reader = new StreamReader(openResponse))
            {
                var openResult = reader.ReadToEnd();
                Assert.Equal("me", openResult);
            }
        }
    }
}
