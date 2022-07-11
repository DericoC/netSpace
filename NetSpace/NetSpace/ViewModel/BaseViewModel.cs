using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NetSpace.ViewModel
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

