using static CodeContract.Contract;

namespace Template
{
    public class Contract
    {
    }

    public class Account
    {
        private int _limit;
        public int Limit
        {
            get => _limit;
            set
            {
                Require(value > 0, "Limit must not be 0.");
                _limit = value;
                ClassInvariant();
            }
        }
        public int Total { get; private set; }
        public int Add(int value, int discount)
        {
            Require(value >= 0, "Value must be positive.")
                .Require(
                    discount >= 0 && discount <= 50, 
                    "Discount must be positive and not more than 50%");
            var old = new { Total };

            // method logic

            Ensure(Total > old.Total, "The total will not decrease.");
            ClassInvariant();
            return discountedValue;
        }
        private void ClassInvariant()
        {
            Invariant(Total >= 0, "The total will not be negative.")
                .Invariant(Limit > 0, "Limit will not be 0.");
        }
    }
}