using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using Android.Content;
using Android.Support.V4.Content;
using Android.Webkit;
using DBDocPreviewer.Droid.Helpers;
using DBDocPreviewer.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(DocumentInteractionController))]
namespace DBDocPreviewer.Droid.Helpers
{
    public class DocumentInteractionController : IDocumentInteractionController
    {
        public string filename;
        public string filePath;

        public void DownloadAndOpenFile(string url, string folder)
        {
            string pathToNewFolder;
            //string pathToNewFolder = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, folder);

            if (Android.OS.Environment.IsExternalStorageEmulated)
            {
                pathToNewFolder = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, folder);
            }
            else
                pathToNewFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), folder);

            if (!Directory.Exists(pathToNewFolder))
                Directory.CreateDirectory(pathToNewFolder);

            try
            {
                filename = url;
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                string pathToNewFile = Path.Combine(pathToNewFolder, Path.GetFileName(url));
                filePath = pathToNewFile;
                webClient.DownloadFileAsync(new Uri(url), pathToNewFile);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            Java.IO.File file = new Java.IO.File(filePath);

            if (e.Error != null)
            { 
            }
            else
            {
                Android.Net.Uri pdfPath = FileProvider.GetUriForFile(Android.App.Application.Context,
                                Android.App.Application.Context.PackageName + ".provider", file);

                string extension = MimeTypeMap.GetFileExtensionFromUrl(Android.Net.Uri.FromFile(file).ToString());
                string mimeType = MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension);

                Intent intent = new Intent(Intent.ActionView);
                intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);
                intent.SetDataAndType(pdfPath, mimeType); //application/pdf
                intent.AddFlags(ActivityFlags.GrantReadUriPermission);
                Forms.Context.StartActivity(intent);
            }
        }
    }
}
