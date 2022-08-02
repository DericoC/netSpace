using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NetSpace.Model;
using NetSpace.Service;
using NetSpace.Util;
using Xamarin.Forms;

namespace NetSpace.ViewModel
{
    public class AdminCreatePlaceViewModel : BaseViewModel
    {
        public Place place { get; set; }
        public Command createPlace;
        public Command savePlace;
        public Command pickImage;
        public ObservableCollection<User> managers { get; set; }
        public ObservableCollection<Policy> policies { get; set; }
        public ObservableCollection<Tags> tags { get; set; }
        public List<Tags> selectedTags { get; set; }
        private readonly SnackBarAlert alert = new SnackBarAlert();
        private UserSession ses = UserSession.getSession();
        private UserService userService = new UserService();
        private PlaceService placeService = new PlaceService();
        private PolicyService policyService = new PolicyService();
        private TagsService tagsService = new TagsService();
        private TagsPlacesService tpService = new TagsPlacesService();
        public bool isCreate { get; set; }
        public bool isBusy { get; set; }

        public AdminCreatePlaceViewModel(Place p)
        {
            isBusy = false;
            selectedTags = new List<Tags>();
            managers = new ObservableCollection<User>();
            foreach (var item in userService.businessManagers(ses.getUser().provider))
            {
                managers.Add(item);
            }
            policies = new ObservableCollection<Policy>();
            foreach (var item in policyService.businessPolicies(ses.getUser().provider))
            {
                policies.Add(item);
            }
            tags = new ObservableCollection<Tags>();
            foreach (var item in tagsService.read())
            {
                tags.Add(item);
            }
            place = p != null ? p : new Place();
            createPlace = new Command(async () => await addPlace());
            savePlace = new Command(async () => await updatePlace());
            isCreate = p == null;
        }
        private async Task addPlace()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                isBusy = true;
            });
            await Task.Delay(100);
            TagsPlaces tagPlaces = new TagsPlaces();
            tagPlaces.place = new Place();
            tagPlaces.tag = new Tags();

            if (place.manager != null)
            {
                if (place.policy != null)
                {
                    place.business = new Business();
                    place.business.business_id = ses.getUser().provider;
                    if (placeService.insert(place))
                    {
                        tagPlaces.place.place_id = placeService.findByNameAndDescription(place.place_name, place.description);
                        foreach (var item in selectedTags)
                        {
                            tagPlaces.tag.tag_id = item.tag_id;
                            await tpService.insert(tagPlaces, true);
                        }

                        Application.Current.MainPage.Navigation.PopAsync();
                        await alert.displaySnackBarAlertAsync("Lugar creado.", 5, SnackBarAlert.INFORMATION);
                    }
                    else
                    {
                        await alert.displaySnackBarAlertAsync("Ha ocurrido un error.", 5, SnackBarAlert.ERROR);
                    }
                } else
                {
                    await alert.displaySnackBarAlertAsync("Debe seleccionar una politica.", 5, SnackBarAlert.ERROR);
                }
            } else
            {
                await alert.displaySnackBarAlertAsync("Debe seleccionar un administrador.", 5, SnackBarAlert.ERROR);
            }
            Device.BeginInvokeOnMainThread(() =>
            {
                isBusy = false;
            });
        }
        private async Task updatePlace()
        {
            TagsPlaces tagPlaces = new TagsPlaces();
            tagPlaces.place = new Place();
            tagPlaces.tag = new Tags();

            if (placeService.update(place))
            {
                tagPlaces.tag.tag_id = tpService.findByPlaceId(place.place_id);
                foreach (var item in selectedTags)
                {
                    tagPlaces.place.place_id = place.place_id;
                    if (tpService.update(tagPlaces, item.tag_id))
                    {
                        Application.Current.MainPage.Navigation.PopAsync();
                        await alert.displaySnackBarAlertAsync("Lugar actualizado.", 5, SnackBarAlert.INFORMATION);
                    }
                    else
                    {
                        await alert.displaySnackBarAlertAsync("Ha ocurrido un error.", 5, SnackBarAlert.ERROR);
                    }
                }
            }
            else
            {
                await alert.displaySnackBarAlertAsync("Ha ocurrido un error.", 5, SnackBarAlert.ERROR);
            }
        }

        public void comboBoxChange(Tags tag)
        {
            selectedTags.Add(tag);
        }

        public Command CreatePlace
        {
            get => createPlace;
        }

        public Command SavePlace
        {
            get => savePlace;
        }
    }
}