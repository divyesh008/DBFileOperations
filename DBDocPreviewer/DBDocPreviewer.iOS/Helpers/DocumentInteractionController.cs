using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using DBDocPreviewer.Interfaces;
using DBDocPreviewer.iOS.Helpers;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(DocumentInteractionController))]
namespace DBDocPreviewer.iOS.Helpers
{
    public class DocumentInteractionController : IDocumentInteractionController
    {
        public string pathToNewFile;

        public void DownloadAndOpenFile(string url, string folder)
        {
            try
            {
                string pathToNewFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), folder);
                if (!Directory.Exists(pathToNewFolder))
                    Directory.CreateDirectory(pathToNewFolder);

                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                pathToNewFile = Path.Combine(pathToNewFolder, Path.GetFileName(url));
                Debug.WriteLine("PDF File Path ====== " + pathToNewFile);
                webClient.DownloadFileAsync(new Uri(url), pathToNewFile);
            }
            catch(Exception ex) 
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                 
            }
            else
            {
                var previewController = UIDocumentInteractionController.FromUrl(NSUrl.FromFilename(pathToNewFile));
                previewController.Delegate = new MyInteractionDelegate(UIApplication.SharedApplication.KeyWindow.RootViewController);
                previewController.PresentPreview(true);
            }
        }
    }

    internal class MyInteractionDelegate : UIDocumentInteractionControllerDelegate
    {
        private UIViewController rootViewController;

        public MyInteractionDelegate(UIViewController rootViewController)
        {
            this.rootViewController = rootViewController;
        }

        public override UIViewController ViewControllerForPreview(UIDocumentInteractionController controller)
        {
            return rootViewController;
        }
    }
}
