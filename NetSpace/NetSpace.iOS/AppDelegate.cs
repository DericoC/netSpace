﻿using Foundation;
using Syncfusion.ListView.XForms.iOS;
using Syncfusion.SfBusyIndicator.XForms.iOS;
using Syncfusion.SfCalendar.XForms.iOS;
using Syncfusion.XForms.iOS.EffectsView;
using UIKit;

namespace NetSpace.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            global::ZXing.Net.Mobile.Forms.iOS.Platform.Init();
            SfCalendarRenderer.Init();
            LoadApplication(new App());
            new SfBusyIndicatorRenderer();
            Syncfusion.XForms.iOS.ComboBox.SfComboBoxRenderer.Init();
            Syncfusion.XForms.iOS.Border.SfBorderRenderer.Init();
            Syncfusion.XForms.iOS.Buttons.SfButtonRenderer.Init();
            Syncfusion.XForms.iOS.PopupLayout.SfPopupLayoutRenderer.Init();
            Syncfusion.XForms.iOS.Core.SfAvatarViewRenderer.Init();
            SfListViewRenderer.Init();
            SfEffectsViewRenderer.Init();
            return base.FinishedLaunching(app, options);
        }
    }
}

