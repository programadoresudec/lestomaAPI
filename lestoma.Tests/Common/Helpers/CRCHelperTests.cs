using lestoma.CommonUtils.Helpers;
using lestoma.Tests.Global;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Xunit;

namespace lestoma.Tests.Common.Helpers
{
    public class CRCHelperTests : IClassFixture<MyTestFixture>
    {
        private readonly MyTestFixture _fixture;
        public CRCHelperTests(MyTestFixture fixture)
        {
            _fixture = fixture;
        }
        [Theory]
        [InlineData(73, 1, 240, 1, 63, 128, 0, 0)]
        [InlineData(111, 0, 240, 1, 63, 128, 0, 0)]
        [InlineData(73, 1, 240, 1, 0, 0, 0, 0)]
        [InlineData(111, 0, 240, 1, 0, 0, 0, 0)]
        [InlineData(73, 1, 60, 1, 0, 0, 0, 0)]
        [InlineData(73, 1, 240, 2, 63, 128, 0, 0)]
        [InlineData(111, 0, 240, 2, 63, 128, 0, 0)]
        [InlineData(73, 9, 240, 2, 0, 0, 0, 0)]
        [InlineData(111, 0, 240, 2, 0, 0, 0, 0)]
        [InlineData(73, 6, 60, 2, 0, 0, 0, 0)]
        [InlineData(73, 6, 240, 4, 63, 128, 0, 0)]
        [InlineData(111, 0, 240, 4, 63, 128, 0, 0)]
        [InlineData(73, 6, 240, 40, 0, 0, 0, 0)]
        [InlineData(111, 0, 240, 4, 0, 0, 0, 0)]
        [InlineData(73, 3, 60, 4, 0, 0, 0, 0)]
        [InlineData(73, 3, 240, 0, 63, 128, 0, 0)]
        [InlineData(111, 0, 240, 0, 63, 128, 0, 0)]
        [InlineData(73, 3, 240, 0, 0, 0, 0, 0)]
        [InlineData(111, 0, 240, 0, 0, 0, 0, 0)]
        [InlineData(73, 3, 60, 0, 0, 0, 0, 0)]
        [InlineData(73, 7, 240, 4, 63, 128, 0, 0)]
        [InlineData(73, 7, 240, 4, 0, 0, 0, 0)]
        [InlineData(73, 7, 60, 4, 0, 0, 0, 0)]
        [InlineData(73, 8, 240, 3, 63, 128, 0, 0)]
        [InlineData(111, 0, 240, 3, 63, 128, 0, 0)]
        [InlineData(73, 8, 240, 3, 0, 0, 0, 0)]
        [InlineData(111, 0, 240, 3, 0, 0, 0, 0)]
        [InlineData(73, 8, 60, 3, 0, 0, 0, 0)]
        [InlineData(73, 5, 15, 2, 0, 0, 0, 0)]
        [InlineData(73, 5, 15, 1, 0, 0, 0, 0)]
        [InlineData(73, 5, 15, 3, 0, 0, 0, 0)]
        [InlineData(73, 5, 15, 4, 0, 0, 0, 0)]
        [InlineData(73, 5, 15, 5, 0, 0, 0, 0)]
        [InlineData(73, 5, 240, 2, 67, 131, 192, 0)]
        [InlineData(111, 0, 240, 2, 66, 55, 51, 51)]
        [InlineData(73, 5, 240, 1, 67, 233, 76, 205)]
        [InlineData(111, 0, 240, 1, 67, 166, 192, 0)]
        [InlineData(73, 5, 240, 3, 67, 172, 198, 102)]
        [InlineData(111, 0, 240, 3, 67, 250, 0, 0)]
        public void Add_CRC_To_trama_EightBytes(byte one, byte two, byte three, byte four, byte five, byte six, byte seven, byte eight)
        {
            var tramaCompleta = _fixture.CrcHelper.TramaConCRC16Modbus(new List<byte> { one, two, three, four, five, six, seven, eight });
            Assert.NotNull(tramaCompleta);
            Assert.Equal(10, tramaCompleta.Count);
            string tramaString = string.Join("-", tramaCompleta.Select(b => b.ToString())) + Environment.NewLine;
            File.AppendAllText(_fixture.RutaArchivo, tramaString, Encoding.Unicode);
        }
        [Theory]
        [InlineData("4901F0493F8000005350")]
        [InlineData("4901F04900000000AF5D")]
        [InlineData("4904F04900000000AF08")]
        [InlineData("6F97F01642480000F729")]
        public void CRCReceived_Return_CorrectFormat(string crcRecibido)
        {
            var response = _fixture.CrcHelper.VerifyCRCOfReceivedTrama(crcRecibido);
            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("498BF01A40C00000", "97E7")]
        [InlineData("4961F08540C00000", "4538")]
        [InlineData("498BF0D940C00000", "86A3")]
        [InlineData("49C8F05C420C0000", "1BDD")]
        [InlineData("6F03F0C4420C0000", "3904")]
        [InlineData("49050F010000A242", "3F15")]
        [InlineData("43CC800000004842", "D3A1")]
        
        public void CRC_Returns_Equals(string trama, string crc)
        {
            var result = _fixture.CrcHelper.CalculateCrc16Modbus(trama);
            Assert.NotNull(result);
            string resultHexa = Reutilizables.ByteArrayToHexString(new byte[] { result[1], result[0] });
            Assert.Equal(crc, resultHexa);
        }
    }
}
