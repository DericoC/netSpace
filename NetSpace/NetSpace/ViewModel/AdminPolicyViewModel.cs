using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NetSpace.Model;
using NetSpace.Service;
using NetSpace.Util;
using NetSpace.View;
using Xamarin.Forms;

namespace NetSpace.ViewModel
{
    public class AdminPolicyViewModel : BaseViewModel
    {
        public Command createPolicy;
        public ObservableCollection<Policy> policies { get; set; }
        private UserSession ses = UserSession.getSession();
        private PolicyService policyService = new PolicyService();
        private readonly SnackBarAlert alert = new SnackBarAlert();

        public AdminPolicyViewModel()
        {
            createPolicy = new Command(async () => await goToCreatePolicy());
            policies = new ObservableCollection<Policy>();
            foreach (var item in policyService.businessPolicies(ses.getUser().provider))
            {
                policies.Add(item);
            }
        }

        private async Task goToCreatePolicy()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AdminCreatePolicyView(null));
        }

        public async Task<bool> deletePolicyAsync(Policy p)
        {
            bool valido = false;

            if (policyService.delete(p))
            {
                await alert.displaySnackBarAlertAsync("Politica borrado.", 3, SnackBarAlert.INFORMATION);
                valido = true;
            }
            else
            {
                await alert.displaySnackBarAlertAsync("Ha ocurrido un error.", 3, SnackBarAlert.ERROR);
            }

            return valido;
        }

        public Command GoToCreatePolicy
        {
            get => createPolicy;
        }
    }
}

