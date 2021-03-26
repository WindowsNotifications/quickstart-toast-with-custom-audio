using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.System.Profile;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace QuickstartToastWithCustomAudio
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ButtonSendToast_Click(object sender, RoutedEventArgs e)
        {
            // In a real app, these would be initialized with actual data
            string title = "Andrew Bares";
            string content = "Cannot wait to try your UWP app!";
            var logoOverrideUri = new Uri("https://picsum.photos/48?image=883");

            // Construct the content21
            var contentBuilder = new ToastContentBuilder()
                .AddToastActivationInfo("app-defined-string", ToastActivationType.Foreground)
                .AddText(title)
                .AddText(content)
                // Profile (app logo override) image
                .AddAppLogoOverride(logoOverrideUri, ToastGenericAppLogoCrop.Circle);

            // If we're running on Desktop before Version 1511, do NOT include custom audio
            // since it was not supported until Version 1511, and would result in a silent toast.

            var supportsCustomAudio = !(AnalyticsInfo.VersionInfo.DeviceFamily.Equals("Windows.Desktop")
                                        && !ApiInformation.IsApiContractPresent(
                                            "Windows.Foundation.UniversalApiContract", 2));

            if (supportsCustomAudio)
            {
                contentBuilder.AddAudio(new Uri("ms-appx:///Assets/Audio/CustomToastAudio.m4a"));
            }

            // Send the toast
            contentBuilder.Show();
        }
    }
}
