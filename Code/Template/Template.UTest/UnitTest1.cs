using FsCheck;
using FsCheck.Xunit;
using System;
using Xunit;

namespace Template.UTest
{
    public class UnitTest1
    {
        [Property(Arbitrary = new[] { typeof(Arbitraries) }, Verbose = true)]
        public void AddProperty(
            (decimal limit, decimal total) accountSetup, 
            (decimal value, decimal discount) accountAdd)
        {
            // ARRANGE
            var account = new Account(accountSetup.total, accountSetup.limit);

            // ACT
            account.Add(accountAdd.value, accountAdd.discount);
        }

        public static class Arbitraries
        {
            public static Arbitrary<(decimal limit, decimal total)> AccountGenerator()
            {
                return
                    (
                        from limit in Arb.Default.Decimal()
                                        .Convert(
                                            limit => limit.Round(), 
                                            limit => limit)
                                        .Filter(limit => limit > 0)
                                        .Generator
                        from total in Arb.Default.Decimal()
                                        .Convert(
                                            total => total.Round(), 
                                            total => total)
                                        .Filter(total => total > 0)
                                        .Generator
                        where total < limit
                        select (limit, total))
                    .ToArbitrary();
            }

            public static Arbitrary<(decimal value, decimal discount)> AccountAddGenerator()
            {
                return (
                        from value in Arb.Default.Decimal()
                                        .Convert(
                                            value => value.Round(),
                                            value => value)
                                        .Filter(value => value >= 0)
                                        .Generator
                        from discount in Arb.Default.Decimal()
                                        .Convert(
                                            discount => discount.Round(), 
                                            discount => discount)
                                        .Filter(discount => 
                                            discount >= 0 && discount <= 50)
                                        .Generator
                        select (value, discount))
                    .ToArbitrary();
            }
        }
    }
}
