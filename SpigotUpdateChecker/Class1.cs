using System;
using System.IO;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.UI.Xaml;

namespace SpigotUpdateChecker
{
    class Notifications
    {
        private const String APP_ID = "Microsoft.Samples.DesktopToastsSample";

        // Create and show the toast.
        // See the "Toasts" sample for more detail on what can be done with toasts
        public static void ShowToast(string text)
        {

            // Get a toast XML template
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText04);

            // Fill in the text elements
            XmlNodeList stringElements = toastXml.GetElementsByTagName("text");
            for (int i = 0; i < stringElements.Length; i++)
            {
                stringElements[i].AppendChild(toastXml.CreateTextNode(text));
            }

            // Specify the absolute path to an image
            String imagePath = "file:///" + Properties.Resources.ALRT; // "file:///" + Path.GetFullPath("ALRT.png");
            XmlNodeList imageElements = toastXml.GetElementsByTagName("image");
            imageElements[0].Attributes.GetNamedItem("src").NodeValue = imagePath;

            // Create the toast and attach event listeners
            ToastNotification toast = new ToastNotification(toastXml);
            toast.Activated += ToastActivated;
            toast.Dismissed += ToastDismissed;
            toast.Failed += ToastFailed;

            // Show the toast. Be sure to specify the AppUserModelId on your application's shortcut!
            ToastNotificationManager.CreateToastNotifier("SPIGOT ALERT SPIGOT ALERT").Show(toast);
        }

        private static void ToastActivated(ToastNotification sender, object e)
        {
            //Activate();
            //Output.Text = "The user activated the toast.";
        }

        private static void ToastDismissed(ToastNotification sender, ToastDismissedEventArgs e)
        {
            String outputText = "";
            switch (e.Reason)
            {
                case ToastDismissalReason.ApplicationHidden:
                    outputText = "The app hid the toast using ToastNotifier.Hide";
                    break;
                case ToastDismissalReason.UserCanceled:
                    outputText = "The user dismissed the toast";
                    break;
                case ToastDismissalReason.TimedOut:
                    outputText = "The toast has timed out";
                    break;
            }

            //Dispatcher.Invoke(() =>
            //{
            //    Output.Text = outputText;
            //});
        }

        private static void ToastFailed(ToastNotification sender, ToastFailedEventArgs e)
        {
            //Dispatcher.Invoke(() =>
            //{
            //    Output.Text = "The toast encountered an error.";
            //});
        }
    }
}
