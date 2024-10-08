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
        [InlineData(400, 412)]
        [InlineData(500, 513)]
        [InlineData(700, 715)]
        public void NextShaveDay_ShouldReturnCorrectShaveDay(decimal ageInDays, decimal expectedShaveDay)
        {
            var result = YakProduceCalculator.NextShaveDay(ageInDays);
            Assert.Equal(expectedShaveDay, result * 100);
        }

        [Theory]
        [InlineData(50, 85, false)]
        [InlineData(513, 513, true)]
        [InlineData(1000, 1018, false)]
        public void IsEligibleToBeShaven_ShouldReturnCorrectEligibility(decimal ageInDays, decimal ageNextShaveInDays, bool expectedEligibility)
        {
            var result = YakProduceCalculator.IsEligibleToBeShaved(ageInDays, ageNextShaveInDays);
            Assert.Equal(expectedEligibility, result);
        }

        [Fact]
        public void TotalHerdLitersOfMilkToday_ShouldReturnCorrectTotalMilk()
        {
            var herdMembers = new List<IHerdMember>
            {
                new HerdMember(Guid.Empty, "Yak-1", 3, "FEMALE", 3, (decimal) 3.11),
                new HerdMember(Guid.Empty, "Yak-2", 5, "FEMALE", 5, (decimal) 5.13),
                new HerdMember(Guid.Empty, "Yak-3", 8, "MALE", 8, (decimal) 8.16)
            };

            var result = YakProduceCalculator.TotalHerdLitersOfMilkToday(herdMembers);
            Assert.Equal(76, result); // 41 + 35 + 0 (male yak doesn't produce milk)
        }
    }
}