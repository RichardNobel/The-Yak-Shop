using YakShop.Server.Models;

namespace YakShop.Server.Helpers
{
    public static class YakProduceCalculator
    {
        public static decimal SingleYakLitersOfMilkByAge(decimal ageInDays)
        {
            if (ageInDays > 999)
            {
                return 0;
            }

            return 50 - (ageInDays * (decimal)0.03);
        }

        public static decimal NextShaveDay(decimal ageInDays, decimal ageLastShaved)
        {
            decimal nextShaveDay = (8 + (ageInDays * (decimal)0.01)) + ageLastShaved;
            return Math.Floor(nextShaveDay);
        }

        public static bool IsEligibleToBeShaven(decimal ageInDays, decimal ageLastShaved)
        {
            if (ageInDays < 100 || ageInDays > 999)
            {
                return false;
            }

            return ageInDays >= NextShaveDay(ageInDays, ageLastShaved);
        }

        public static decimal TotalHerdLitersOfMilkToday(IEnumerable<IHerdMember> herdMembers)
        {
            decimal milkAmount = 0;
            foreach (var yak in herdMembers.Where(
                hm => hm.Sex.Equals("FEMALE", StringComparison.InvariantCultureIgnoreCase)
                && hm.Age < 10
            ))
            {
                milkAmount += SingleYakLitersOfMilkByAge(yak.Age * 100);
            }

            return milkAmount;
        }
    }
}
