using YakShop.Server.Models;

namespace YakShop.Server.Helpers
{
    public static class YakProduceCalculator
    {
        public static decimal SingleYakLitersOfMilkByAge(decimal ageInDays)
        {
            return 50 - (ageInDays * (decimal)0.03);
        }

        public static decimal NextShaveDay(decimal ageInDays)
        {
            return 8 + (ageInDays * (decimal)0.01);
        }

        public static bool IsEligibleToBeShaven(decimal ageInDays)
        {
            return NextShaveDay(ageInDays) == ageInDays;
        }

        internal static decimal TotalHerdLitersOfMilkToday(IEnumerable<IHerdMember> herdMembers)
        {
            decimal milkAmount = 0;
            foreach (var yak in herdMembers.Where(hm => hm.Sex.Equals("FEMALE", StringComparison.InvariantCultureIgnoreCase)))
            {
                milkAmount += SingleYakLitersOfMilkByAge(yak.Age);
            }

            return milkAmount;
        }
    }
}
