using System;
using NetSpace.ViewModel;

namespace NetSpace.Model
{
	public class GeneralParameters : BaseViewModel
	{
		public int general_parameter_id { get; set; }
		public string general_param_name { get; set; }
		public string description { get; set; }
		public string value { get; set; }
		public string hour_value { get; set; }

		public GeneralParameters()
		{
		}

        public GeneralParameters(int general_parameter_id, string general_param_name, string description, string value)
        {
            this.general_parameter_id = general_parameter_id;
            this.general_param_name = general_param_name;
            this.description = description;
            this.value = value;
        }
    }
}

