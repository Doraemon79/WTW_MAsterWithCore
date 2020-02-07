using System.Collections.Generic;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using Wtw.Models;
using Wtw.Services;

namespace WtwTest.Services
{
    class TriangleServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AccumulateTest()
        {
            var applicationSettings = new ApplicationSettings()
            {
                IgnoreBadRows = true
            };
            var triangleService = new TriangleService(Options.Create(applicationSettings));
            var inputs = new List<Input>
            {
                new Input() { Product = "Comp", OriginYear = 1992, DevelopmentYear = 1992, IncrementalValue = 110.0m },
                new Input() { Product = "Comp", OriginYear = 1992, DevelopmentYear = 1993, IncrementalValue = 170.0m },
                new Input() { Product = "Comp", OriginYear = 1993, DevelopmentYear = 1993, IncrementalValue = 200.0m },
            };
            var actualReport =  triangleService.Accumulate(inputs);

            var expectedReport = new Report()
            {
                MinYear = 1992,
                YearCount = 2,
                Products = new[]
                {
                    new Product()
                    {
                        Name = "Comp",
                        Values = new[] {110m, 280m, 200m}
                    }
                }
            };

            Assert.AreEqual(expectedReport.MinYear, actualReport.MinYear);
            Assert.AreEqual(expectedReport.YearCount, actualReport.YearCount);
            Assert.AreEqual(expectedReport.Products.Length, actualReport.Products.Length);
            for (int i = 0; i < expectedReport.Products.Length; i++)
            {
                var expectedProduct = expectedReport.Products[i];
                var actualProduct = actualReport.Products[i];

                Assert.AreEqual(expectedProduct.Name, actualProduct.Name);
                Assert.AreEqual(expectedProduct.Values.Length, actualProduct.Values.Length);
                for (int j = 0; j < expectedProduct.Values.Length; j++)
                {
                    Assert.AreEqual(expectedProduct.Values[j], actualProduct.Values[j]);
                }
            }
        }
    }
}
