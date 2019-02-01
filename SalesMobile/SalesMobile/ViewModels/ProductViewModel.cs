namespace SalesMobile.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using SalesCommon;
    using SalesMobile.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class ProductViewModel : BaseViewModel
    {
        #region Services
        private APIService apiService;
        #endregion

        #region Attributes

        private ObservableCollection<Product> products;
        private bool isRefreshing;
        

        #endregion

        #region Properties

        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                if (isRefreshing != value)
                {
                    isRefreshing = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Product> Products
        {
            get => products;
            set
            {
                if (products != value)
                {
                    products = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Constructor
        public ProductViewModel()
        {
            this.apiService = new APIService();
            this.LoadProducts();
        }


        #endregion

        #region Comands


        public ICommand RefreshCommand => new RelayCommand(LoadProducts);
        
        #endregion

        #region Methods

        private async void LoadProducts()
        {
            this.IsRefreshing = true;

            var response = await this.apiService.GetList<Product>("https://salesapiservice.azurewebsites.net", "/api", "/Products");
            //var urlProducts = Application.Current.Resources["UrlApiProducts"].ToString();
            //var prefix = Application.Current.Resources["Prefix"].ToString();
            //var ProductsController = Application.Current.Resources["ProductsController"].ToString();
            //var response = await this.apiService.GetList<Product>(urlProducts,prefix,ProductsController);

            //Aqui pregunto si llego algo de la api
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error,",response.Message,"Accept.");

                this.IsRefreshing = false;

                return;
            }

            //aqui ya llego una lista de objecto: tambien caste lo que llega de T  una lista de product:
            var list = (List<Product>)response.Result;

            //aqui convierto la lista a una lista de obsevableCollection:
            this.Products = new ObservableCollection<Product>(list);

            this.IsRefreshing = false;


        }
        #endregion
    }
}
