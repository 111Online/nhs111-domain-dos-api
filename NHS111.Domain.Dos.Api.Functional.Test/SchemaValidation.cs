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
                    throw new InvalidEnumArgumentException($"ResponseSchemaType of {schemaType.ToString()} is unsupported");
            }
        }

        private static void AssertValidCheckCapacitySummaryResponseSchema(string result)
        {
            Assert.IsTrue(result.Contains("\"serviceType\"{\"id"));
            Assert.IsTrue(result.Contains("\"serviceType\"{\"name"));
            Assert.IsTrue(result.Contains("\"id"));
            Assert.IsTrue(result.Contains("\"capacity"));
            Assert.IsTrue(result.Contains("\"name"));
            Assert.IsTrue(result.Contains("\"contactDetails"));
            Assert.IsTrue(result.Contains("\"address"));
            Assert.IsTrue(result.Contains("\"postCode"));
            Assert.IsTrue(result.Contains("\"northings"));
            Assert.IsTrue(result.Contains("\"eastings"));
            Assert.IsTrue(result.Contains("\"url"));
            Assert.IsTrue(result.Contains("\"notes"));
            Assert.IsTrue(result.Contains("\"openAllHours"));
            Assert.IsTrue(result.Contains("\"rotaSessions"));
            Assert.IsTrue(result.Contains("\"serviceType"));
            Assert.IsTrue(result.Contains("\"odsCode"));
        }

        private static void AssertValidCheckServiceDetailsByIdResponseSchema(string result)
        {
            Assert.IsTrue(result.Contains("\"id"));
            Assert.IsTrue(result.Contains("\"odsCode"));
            Assert.IsTrue(result.Contains("\"contactDetails"));
        }

    }
}
