namespace SalesMobile.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using SalesMobile.Views;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class MainViewModel
    {
        #region Properties
        public ProductViewModel  Product { get; set; }
        public AddProductViewModel AddProduct { get; set; }
        #endregion


        #region Constructor
        public MainViewModel()
        {
            this.Product = new ProductViewModel();
            this.AddProduct = new AddProductViewModel();
        }
        #endregion

        #region Commands

        public ICommand AddProductCommand { get => new RelayCommand(GoToAddProduct); }

        #endregion

        #region Methods

        private async void GoToAddProduct()
        {
             await Application.Current.MainPage.Navigation.PushAsync(new AddProductPage());
        }

        #endregion
    }
}
