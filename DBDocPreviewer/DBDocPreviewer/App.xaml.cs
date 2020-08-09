using System;
using DBDocPreviewer.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DBDocPreviewer
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainView();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
