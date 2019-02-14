namespace SalesMobile.Helpers
{
    using SalesMobile.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Xamarin.Forms;
    using Resources;

    public class Languages
    {
        public Languages()
        {
            var cultureInfo = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = cultureInfo;
            DependencyService.Get<ILocalize>().SetLocale(cultureInfo);
        }


        public static string Accept { get => Resource.Accept; }
        public static string Error { get => Resource.Error; }
        public static string NoInternet { get => Resource.NoInternet; }
        public static string Products { get => Resource.Products; }
        public static string TurnOnInternet { get => Resource.TurnOnInternet; }
        public static string google_com { get => Resource.google_com; }
        public static string Ok { get => Resource.OK; }
        public  static string AddProduct { get => Resource.AddProduct; }
    }
}
