using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.Listados;

namespace Lestoma.Tests.Common.Helpers
{
    public class ReutilizablesTests
    {
        [Fact]
        public void Desencrypt_ShouldBe_Equals()
        {
            string valueEncrypt = "BVD45PKGn6zFuRa/2nc9N49sYf5mxBRFCVbphfB8v8Y=";
            string decript = Reutilizables.Decrypt(valueEncrypt);
            string encript = Reutilizables.Encrypt(decript);
            Assert.Equal(valueEncrypt, encript);
        }
        [Fact]
        public void Encrypt_ShouldBe_Equals()
        {
            string valuedesencrypt = "diego12345";
            string encript = Reutilizables.Encrypt(valuedesencrypt);
            string decript = Reutilizables.Decrypt(encript);
            Assert.Equal(valuedesencrypt, decript);
        }
        [Theory]
        [InlineData("498BF01A40C00000", "97E7")]
        [InlineData("4961F08540C00000", "4538")]
        [InlineData("498BF0D940C00000", "86A3")]
        [InlineData("49C8F05C420C0000", "1BDD")]
        [InlineData("6F03F0C4420C0000", "3904")]
        public void CRC_Returns_Equals(string trama, string crc)
        {
            var result = new CRCHelper().CalculateCrc16Modbus(trama);
            Assert.NotNull(result);
            string resultHexa = Reutilizables.ByteArrayToHexString(new byte[] { result[1], result[0] });
            Assert.Equal(crc, resultHexa);
        }

        [Theory]
        [InlineData(20, "41A00000")]
        [InlineData(48, "42400000")]
        [InlineData(35, "420C0000")]
        [InlineData(27, "41D80000")]
        [InlineData(18, "41900000")]
        public void IEEEFloatingPointToByte_Return_CorrectFormat(int entrada, string salida)
        {
            var resultIEEE = Reutilizables.IEEEFloatingPointToByte(entrada);
            Assert.NotNull(resultIEEE);
            string resultHexa = Reutilizables.ByteArrayToHexString(resultIEEE);
            Assert.Equal(salida, resultHexa);
        }

        [Fact]
        public void ReadJson_return_List_Estados_Componentes()
        {
            ListadoEstadoComponente data = new();
            Assert.Equal(4, data.Listado.Count);
        }
        [Fact]
        public void ParseJson_Return_List()
        {
            var parse = new Reutilizables().ReadJSON<List<ItemColor>>("JsonTest.json");
        }
    }
    public class ItemColor
    {
        public string color { get; set; }
        public string value { get; set; }
    }
}
