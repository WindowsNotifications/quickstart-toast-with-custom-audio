using NotificationsExtensions;
using NotificationsExtensions.Toasts;
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

            // Construct the toast content
            ToastContent toastContent = new ToastContent()
            {
                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText()
                            {
                                Text = title
                            },

                            new AdaptiveText()
                            {
                                Text = content
                            }
                        }
                    }
                }
            };

            bool supportsCustomAudio = true;

            // If we're running on Desktop before Version 1511, do NOT include custom audio
            // since it was not supported until Version 1511, and would result in a silent toast.
            if (AnalyticsInfo.VersionInfo.DeviceFamily.Equals("Windows.Desktop")
                && !ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 2))
            {
                supportsCustomAudio = false;
            }

            if (supportsCustomAudio)
            {
                toastContent.Audio = new ToastAudio()
                {
                    Src = new Uri("ms-appx:///Assets/Audio/CustomToastAudio.m4a")

                    // Supported audio file types include
                    // .aac
                    // .flac
                    // .m4a
                    // .mp3
                    // .wav
                    // .wma
                };
            }

            // And create the toast notification
            ToastNotification notification = new ToastNotification(toastContent.GetXml());
            
            // And then send the toast
            ToastNotificationManager.CreateToastNotifier().Show(notification);
        }
    }
}
