using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.Listados;
using lestoma.CommonUtils.Requests.Filters;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace Lestoma.Tests.Common.Helpers
{
    public class ReutilizablesTests
    {
        [Fact]
        public void Desencrypt_ShouldBe_Equals()
        {
            string valueEncrypt = "Kzl4WuN0TDHsaft4nGlCwOMNOBs/nKXln6lC0YkeOgGw3HoZNKNgc8FFJiPPrBLwliJLfDzSfHpPWYiWQa5kpSK8XzO8i/WDV6WAFkb9+bBH1OkrBsZz2fX6v/S/FYZb6cN4piDndaYsNZPsp9hpm49O8hXwQrBpRztw4vjFqv3CN0RpVUyKFqXQ0zaVf8b6MqYrBAZO2KPLTDbJldN3yb1R6hqFb+J24KQ/fiUj3XwZuTn5spdDRfGysV7wD1ZLtaljHhqcZqAD6rBxyuMvAjPcf+zwhSj91gPcn3jpfIU9rloHo0h9eRrISN0jrd6fUYawyGzcmPXCFi4W8dOsVg==";
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
        [InlineData("43CC8000434800007082", 200)]
        [InlineData("43CC8000424800008C83", 50)]
        [InlineData("43CC800041F00000ED03", 30)]
        [InlineData("43CC800043CC800099A3", 409)]
        [InlineData("43CC8000417000000502", 15)]
        public void SetPoint_Return_CorrectFormat(string trama, float setPoint)
        {
            float result = Reutilizables.ConvertReceivedTramaToResult(trama);
            Assert.Equal(setPoint, result);
        }

        [Theory]
        [InlineData(200)]
        [InlineData(52.1)]
        [InlineData(26.4)]
        [InlineData(409)]
        [InlineData(15.3)]
        [InlineData(17.23)]
        public void ByteToIEEEFloatingPoint_AndReverse_Return_CorrectFormat(float setPoint)
        {
            var resultByte = Reutilizables.IEEEFloatingPointToByte(setPoint);
            Assert.NotNull(resultByte);
            float result = Reutilizables.ByteToIEEEFloatingPoint(resultByte);
            Assert.Equal(setPoint, result);
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

        [Theory]

        [InlineData("420C0000")]
        [InlineData("41D80000")]
        [InlineData("41900000")]
        public void ByteArrayToHexString_AndReverse_Return_CorrectFormat(string hexa)
        {
            var resultByte = Reutilizables.StringToByteArray(hexa);
            Assert.NotNull(resultByte);
            string resultHexa = Reutilizables.ByteArrayToHexString(resultByte);
            Assert.Equal(hexa, resultHexa);
        }

        [Theory]
        [InlineData("4901F0493F8000005350")]
        [InlineData("4901F04900000000AF5D")]
        [InlineData("4904F04900000000AF08")]
        [InlineData("6F97F01642480000F729")]

        public void CRCReceived_Return_CorrectFormat(string crcRecibido)
        {
            var response = Reutilizables.VerifyCRCOfReceivedTrama(crcRecibido);
            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
        }


        [Theory]
        [InlineData("?UpaId=7d491ea0-bcb0-47ce-aeb5-c0cba7f045cb&ModuloId=2773e708-f03f-43df-8692-e47d0631c975&ActividadId=197c0024-0889-45ca-92a5-da800357bee2")]
        public void GenerateQueryString_Return_CorrectFormat(string resultado)
        {
            var FilterRequest = new UpaModuleActivityFilterRequest
            {
                UpaId = System.Guid.Parse("7D491EA0-BCB0-47CE-AEB5-C0CBA7F045CB"),
                ModuloId = System.Guid.Parse("2773E708-F03F-43DF-8692-E47D0631C975"),
                ActividadId = System.Guid.Parse("197C0024-0889-45CA-92A5-DA800357BEE2")
            };

            string queryString = Reutilizables.GenerateQueryString(FilterRequest);
            Assert.Equal(resultado, queryString);
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
