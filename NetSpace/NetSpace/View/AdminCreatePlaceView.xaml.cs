using NetSpace.Model;
using NetSpace.ViewModel;
using Xamarin.Forms;

namespace NetSpace.View
{
    public partial class AdminCreatePlaceView : ContentPage
    {
        public AdminCreatePlaceView(Place p)
        {
            InitializeComponent();
            BindingContext = new AdminCreatePlaceViewModel(p);
        }
    }
}

