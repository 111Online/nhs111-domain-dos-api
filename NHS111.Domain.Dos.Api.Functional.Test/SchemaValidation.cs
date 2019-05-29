using System.ComponentModel;
using NUnit.Framework;

namespace NHS111.Domain.Dos.Api.Functional.Test
{
    public class SchemaValidation
    {
        public enum ResponseSchemaType
        {
            CheckCapacitySummary,
            CheckServiceDetailsById
        }

        public static void AssertValidResponseSchema(string result, ResponseSchemaType schemaType)
        {
            switch (schemaType)
            {
                case ResponseSchemaType.CheckCapacitySummary:
                    AssertValidCheckCapacitySummaryResponseSchema(result);
                    break;
                case ResponseSchemaType.CheckServiceDetailsById:
                    AssertValidCheckServiceDetailsByIdResponseSchema(result);
                    break;
                default:
                    throw new InvalidEnumArgumentException(string.Format("{0}{1}{2}", "ResponseSchemaType of ", schemaType.ToString(), "is unsupported"));
            }
        }

        private static void AssertValidCheckCapacitySummaryResponseSchema(string result)
        {
            Assert.IsTrue(result.Contains("\"Question"));
            Assert.IsTrue(result.Contains("\"order"));
            Assert.IsTrue(result.Contains("\"topic"));
            Assert.IsTrue(result.Contains("\"id"));
            Assert.IsTrue(result.Contains("\"questionNo"));
            Assert.IsTrue(result.Contains("\"title"));
            Assert.IsTrue(result.Contains("\"jtbs"));
            Assert.IsTrue(result.Contains("\"jtbsText"));
            Assert.IsTrue(result.Contains("\"Answers"));
            Assert.IsTrue(result.Contains("\"symptomDiscriminator"));
            Assert.IsTrue(result.Contains("\"Labels"));
            Assert.IsTrue(result.Contains("\"State"));
        }

        private static void AssertValidCheckServiceDetailsByIdResponseSchema(string result)
        {
            Assert.IsTrue(result.Contains("\"id"));
            Assert.IsTrue(result.Contains("\"odsCode"));
            Assert.IsTrue(result.Contains("\"contactDetails"));
        }

    }
}
