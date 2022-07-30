using System;
using NetSpace.ViewModel;

namespace NetSpace.Model
{
	public class Tags : BaseViewModel
	{
		public int tag_id { get; set; }
		public string name { get; set; }
		public string value { get; set; }

		public Tags()
		{
		}

        public Tags(int tag_id, string name, string value)
        {
            this.tag_id = tag_id;
            this.name = name;
            this.value = value;
        }
    }
}

