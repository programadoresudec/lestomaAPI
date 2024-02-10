using lestoma.CommonUtils.Constants;
using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.ListadosJson;
using lestoma.CommonUtils.Requests.Filters;
using System;
using System.Collections.Generic;
using Xunit;

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
        [InlineData(25.200000762939453, 25.20, 2)]
        [InlineData(66.69999694824219, 66.69, 2)]
        [InlineData(58.71000076293945, 58.71, 2)]
        [InlineData(25.2532, 25.25, 2)]
        [InlineData(1.0, 1, 2)]
        [InlineData(0.000, 0, 2)]
        public void TruncateDouble_To_Two_Decimals_Returns_Equals(double value, double truncateValue, int numeroDecimales)
        {
            double valueOut = Reutilizables.TruncateDouble(value, numeroDecimales);
            Assert.Equal(valueOut, truncateValue);
        }
        [Theory]
        [InlineData("43CC800000004842D3A1", 50)]
        [InlineData(Constants.TRAMA_SUCESS, -411206.25)]
        public void SetPoint_Return_CorrectFormat(string trama, float setPoint)
        {
            float result = Reutilizables.ConvertReceivedTramaToResult(trama);
            Assert.Equal(setPoint, result);
        }

        [Theory]
        [InlineData(80)]
        [InlineData(1)]
        public void ByteToIEEEFloatingPoint_AndReverse_Return_CorrectFormat(float setPoint)
        {
            var resultByte = Reutilizables.IEEEFloatingPointToByte(setPoint);
            Assert.NotNull(resultByte);
            float result = Reutilizables.ByteToIEEEFloatingPoint(resultByte);
            Assert.Equal(setPoint, result);
        }

        [Theory]
        [InlineData(20, "0000A041")]
        [InlineData(200, "00004843")]
        [InlineData(48, "00004042")]
        [InlineData(35, "00000C42")]
        [InlineData(27, "0000D841")]
        [InlineData(18, "00009041")]
        [InlineData(0, "00000000")]
        [InlineData(1, "0000803F")]
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
        [InlineData("?UpaId=7d491ea0-bcb0-47ce-aeb5-c0cba7f045cb&ModuloId=2773e708-f03f-43df-8692-e47d0631c975&ActividadId=197c0024-0889-45ca-92a5-da800357bee2")]
        public void GenerateQueryString_Return_CorrectFormat(string resultado)
        {
            var FilterRequest = new UpaModuleActivityFilterRequest
            {
                UpaId = Guid.Parse("7D491EA0-BCB0-47CE-AEB5-C0CBA7F045CB"),
                ModuloId = Guid.Parse("2773E708-F03F-43DF-8692-E47D0631C975"),
                ActividadId = Guid.Parse("197C0024-0889-45CA-92A5-DA800357BEE2")
            };

            string queryString = Reutilizables.GenerateQueryString(FilterRequest);
            Assert.Equal(resultado, queryString);
        }

        [Fact]
        public void ReadJson_return_List_Estados_Componentes()
        {
            ListadoEstadoComponente data = new();
            Assert.Equal(3, data.Listado.Count);
        }
        [Fact]
        public void ParseJson_Return_List()
        {
            var parse = new Reutilizables().ReadJSON<List<ItemColor>>("JsonTest.json");
            Assert.NotNull(parse);
        }
    }
    public class ItemColor
    {
        public string color { get; set; }
        public string value { get; set; }
    }
}
