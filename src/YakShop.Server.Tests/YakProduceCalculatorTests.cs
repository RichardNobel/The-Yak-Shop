using YakShop.Server.Helpers;
using YakShop.Server.Models;

namespace YakShop.Tests
{
    public class YakProduceCalculatorTests
    {
        [Theory]
        [InlineData(100, 47)]
        [InlineData(200, 44)]
        [InlineData(300, 41)]
        [InlineData(1000, 0)]
        public void SingleYakLitersOfMilkByAge_ShouldReturnCorrectMilkAmount(decimal ageInDays, decimal expectedMilk)
        {
            var result = YakProduceCalculator.SingleYakLitersOfMilkByAge(ageInDays);
            Assert.Equal(expectedMilk, result);
        }

        [Theory]
        [InlineData(412, 400, 412)]
        [InlineData(513, 500, 513)]
        [InlineData(715, 700, 715)]
        public void NextShaveDay_ShouldReturnCorrectShaveDay(decimal ageInDays, decimal ageLastShaved, decimal expectedShaveDay)
        {
            var result = YakProduceCalculator.NextShaveDay(ageInDays, ageLastShaved);
            Assert.Equal(expectedShaveDay, result);
        }

        [Theory]
        [InlineData(50, 0, false)]
        [InlineData(412, 400, true)]
        [InlineData(513, 500, true)]
        [InlineData(916, 900, false)]
        [InlineData(1000, 980, false)]
        public void IsEligibleToBeShaven_ShouldReturnCorrectEligibility(decimal ageInDays, decimal ageLastShaved, bool expectedEligibility)
        {
            var result = YakProduceCalculator.IsEligibleToBeShaven(ageInDays, ageLastShaved);
            Assert.Equal(expectedEligibility, result);
        }

        [Fact]
        public void TotalHerdLitersOfMilkToday_ShouldReturnCorrectTotalMilk()
        {
            var herdMembers = new List<IHerdMember>
            {
                new HerdMember("Yak-1", 3, "FEMALE"),
                new HerdMember("Yak-2", 5, "FEMALE"),
                new HerdMember("Yak-3", 8, "MALE")
            };

            var result = YakProduceCalculator.TotalHerdLitersOfMilkToday(herdMembers);
            Assert.Equal(76, result); // 41 + 35 + 0 (male yak doesn't produce milk)
        }
    }
}
