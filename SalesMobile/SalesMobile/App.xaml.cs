using SalesMobile.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SalesMobile
{
    public partial class App : Application
    {
        #region Constructor
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ProductsPage());
        }
        #endregion

        #region Properties
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        } 
        #endregion
    }
}
