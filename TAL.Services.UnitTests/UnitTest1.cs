using ConfigurationProvider;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using TAL.Domain.Interfaces;
using TAL.Domain.Models;
using TAL.Service.Interfaces;
using TAL.Services;

namespace TAL.Services.UnitTests
{
    public class Tests
    {


        [Test]
        public void NonExistingOccupationTest()
        {
            var mockDataProvider = new Mock<IProvideTALData>();
            mockDataProvider.Setup(x => x.GetOccupation("abc")).Returns(Result.Failed<Occupation>(Error.CreateFrom("Error", ErrorType.InternalServerError)));
            mockDataProvider.Setup(x => x.GetOccupationRating("abc")).Returns(Result.Failed<OccupationRating>(Error.CreateFrom("Error", ErrorType.InternalServerError)));


            ICalculatePremium permiumCalculator = new CalculatePremium(mockDataProvider.Object);

            var premium = permiumCalculator.CalculateDeathPremium(100, 20, "abc");

            Assert.AreEqual(false, premium.IsOk);
        }

        [Test]
        public void ExistingOccupationTest()
        {
            var mockDataProvider = new Mock<IProvideTALData>();
            mockDataProvider.Setup(x => x.GetOccupation("Cleaner")).Returns(Result.Ok(new Occupation() { OccupationName = "Cleaner", Rating = "Light Manual" }));
            mockDataProvider.Setup(x => x.GetOccupationRating("Light Manual")).Returns(Result.Ok(new OccupationRating() { Factor = 1.50, Rating = "Light Manual" }));


            ICalculatePremium permiumCalculator = new CalculatePremium(mockDataProvider.Object);

            var premium = permiumCalculator.CalculateDeathPremium(100, 20, "Cleaner");

            Assert.AreEqual(true, premium.IsOk);

            Assert.AreEqual(36, premium.Value);
        }

        [Test]
        public void NonExistingRatingTest()
        {
            var mockDataProvider = new Mock<IProvideTALData>();
            mockDataProvider.Setup(x => x.GetOccupation("Cleaner")).Returns(Result.Failed<Occupation>(Error.CreateFrom("Error", ErrorType.InternalServerError)));
            mockDataProvider.Setup(x => x.GetOccupationRating("Light Manual")).Returns(Result.Failed<OccupationRating>(Error.CreateFrom("Error", ErrorType.InternalServerError)));

            ICalculatePremium permiumCalculator = new CalculatePremium(mockDataProvider.Object);

            var premium = permiumCalculator.CalculateDeathPremium(100, 20, "Cleaner");

            Assert.AreEqual(false, premium.IsOk);
            
        }
    }
}