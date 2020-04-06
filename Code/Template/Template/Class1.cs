using static CodeContract.Contract;

namespace Template
{
    public class Account
    {
        public Account(decimal value, decimal limit)
        {
            Limit = limit;
            Total = value;
            ClassInvariant();
        }

        private decimal _limit;
        public decimal Limit
        {
            get => _limit.Round();
            set
            {
                Require(value > 0, "Limit must not be 0.");
                _limit = value;
                ClassInvariant();
            }
        }

        public decimal Total { get; private set; }

        public decimal? Add(decimal value, decimal discount)
        {
            Require(value >= 0, "Value must be positive.")
            .Require(
                discount >= 0 && discount <= 50, 
                "Discount must be positive and not more than 50%");
            var old = new { Total };

            var discountedValue = (decimal?)(value*(1-(discount/100))).Round();

            if ((Total + (decimal)discountedValue).Round() > Limit)
                discountedValue = null;
            else
                Total = (Total + (decimal)discountedValue).Round();

            Ensure(Total >= old.Total, "The total will not decrease.");
            Ensure(
                discountedValue == null || old.Total == (Total - value * (1 - (discount / 100))).Round(), 
                "Reversing the transaction results in the previous total.");

            ClassInvariant();
            return discountedValue;
        }

        private void ClassInvariant()
        {
            Invariant(Total >= 0, "The total will not be negative.")
            .Invariant(Limit > 0, "Limit will not be 0.")
            .Invariant(Total <= Limit, "Total may not exceed Limit.");
        }
    }

    public static class Extend
    {
        public static decimal Round(this decimal value)
            => decimal.Round(value, 5);
    }
        
}