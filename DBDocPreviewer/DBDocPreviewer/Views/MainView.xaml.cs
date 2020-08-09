using System;
using System.Collections.Generic;
using DBDocPreviewer.Interfaces;
using Xamarin.Forms;

namespace DBDocPreviewer.Views
{
    public partial class MainView : ContentPage
    {
        IDocumentInteractionController documentInteraction = DependencyService.Get<IDocumentInteractionController>();

        public MainView()
        {
            InitializeComponent();
        }

        void ImageButton_Clicked(System.Object sender, System.EventArgs e)
        {
            var file = @"https://www.tutorialspoint.com/css/css_tutorial.pdf";
            documentInteraction.DownloadAndOpenFile(file, "Attachment");
        }
    }
}
