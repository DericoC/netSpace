using System;
using NetSpace.ViewModel;

namespace NetSpace.Model
{
	public class Tags : BaseViewModel
	{
		public int tag_id { get; set; }
		public string value { get; set; }

		public Tags()
		{
		}

        public Tags(int id, string value)
        {
            this.tag_id = id;
            this.value = value;
        }
    }
}

