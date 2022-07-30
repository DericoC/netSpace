using NetSpace.Model;
using NetSpace.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NetSpace.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminCreatePolicyView : ContentPage
    {
        public bool imageLoaded = false;

        public AdminCreatePolicyView(Policy p)
        {
            InitializeComponent();
            BindingContext = new AdminCreatePolicyViewModel(p);
        }

        void YesChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
            combo.IsVisible = true;
        }

        void NoChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
            combo.IsVisible = false;
        }
    }
}