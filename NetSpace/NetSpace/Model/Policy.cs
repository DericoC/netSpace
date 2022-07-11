using System;
using NetSpace.ViewModel;

namespace NetSpace.Model
{
	public class Policy : BaseViewModel
	{
		public int policy_id { get; set; }
		public string policy_name { get; set; }
		public bool age_restriction { get; set; }
		public double deposit { get; set; }
		public double price { get; set; }

		public Policy()
		{
		}

        public Policy(int policy_id, string policy_name, bool age_restriction, double deposit, double price)
        {
            this.policy_id = policy_id;
            this.policy_name = policy_name;
            this.age_restriction = age_restriction;
            this.deposit = deposit;
            this.price = price;
        }
    }
}

