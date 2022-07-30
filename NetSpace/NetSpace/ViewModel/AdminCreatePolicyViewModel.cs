using System;
using System.Threading.Tasks;
using NetSpace.Model;
using NetSpace.Service;
using NetSpace.Util;
using Xamarin.Forms;

namespace NetSpace.ViewModel
{
    public class AdminCreatePolicyViewModel
    {
        public Policy policy { get; set; }
        public Command createPolicy;
        public Command updatePolicyCommand;
        public bool isCreate { get; set; }
        private UserSession ses = UserSession.getSession();
        private PolicyService policyService = new PolicyService();
        private SnackBarAlert alert = new SnackBarAlert();

        public AdminCreatePolicyViewModel(Policy p)
        {
            policy = p != null ? p : new Policy();
            createPolicy = new Command(async () => await addPolicy());
            updatePolicyCommand = new Command(async () => await updatePolicy());
            isCreate = p == null;
        }

        private async Task addPolicy()
        {
            policy.business_id = ses.getUser().provider;
            if (policyService.insert(policy))
            {
                Application.Current.MainPage.Navigation.PopAsync();
                await alert.displaySnackBarAlertAsync("Politica creado.", 5, SnackBarAlert.INFORMATION);
            }
            else
            {
                await alert.displaySnackBarAlertAsync("Ha ocurrido un error.", 5, SnackBarAlert.ERROR);
            }
        }

        private async Task updatePolicy()
        {
            policy.business_id = ses.getUser().provider;
            if (policyService.update(policy))
            {
                Application.Current.MainPage.Navigation.PopAsync();
                await alert.displaySnackBarAlertAsync("Politica actualizado.", 5, SnackBarAlert.INFORMATION);
            }
            else
            {
                await alert.displaySnackBarAlertAsync("Ha ocurrido un error.", 5, SnackBarAlert.ERROR);
            }
        }

        public Command CreatePolicy
        {
            get => createPolicy;
        }

        public Command UpdatePolicyCommand
        {
            get => updatePolicyCommand;
        }
    }
}

