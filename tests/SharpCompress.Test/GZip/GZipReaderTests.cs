﻿using System.IO;
using System.Threading.Tasks;
using SharpCompress.Common;
using SharpCompress.IO;
using Xunit;

namespace SharpCompress.Test.GZip
{
    public class GZipReaderTests : ReaderTests
    {
        public GZipReaderTests()
        {
            UseExtensionInsteadOfNameToVerify = true;
        }

        [Fact]
        public async ValueTask GZip_Reader_Generic()
        {
            await ReadAsync("Tar.tar.gz", CompressionType.GZip);
        }
        
        
        [Fact]
        public async ValueTask GZip_Reader_Generic2()
        {
            //read only as GZip itme
            using (Stream stream = File.OpenRead(Path.Combine(TEST_ARCHIVES_PATH, "Tar.tar.gz")))
            await using (var reader = SharpCompress.Readers.GZip.GZipReader.Open(new RewindableStream(stream)))
            {
                while (await reader.MoveToNextEntry()) // Crash here
                {
                    Assert.NotEqual(0, reader.Entry.Size);
                    Assert.NotEqual(0, reader.Entry.Crc);
                }
            }
        }
    }
}