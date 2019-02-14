namespace SalesMobile.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using SalesCommon;
    using SalesMobile.Helpers;
    using SalesMobile.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class ProductViewModel : BaseViewModel
    {
        #region Services
        private readonly APIService apiService;
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

            Response connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            }

            // var response = await this.apiService.GetList<Product>("https://salesapiservice.azurewebsites.net", "/api", "/Products");
            string urlProducts = Application.Current.Resources["UrlApiProducts"].ToString();
            string prefix = Application.Current.Resources["Prefix"].ToString();
            string ProductsController = Application.Current.Resources["ProductsController"].ToString();
            Response response = await this.apiService.GetList<Product>(urlProducts, prefix, ProductsController);

            //Aqui pregunto si llego algo de la api
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Error);

                this.IsRefreshing = false;

                return;
            }

            //aqui ya llego una lista de objecto: tambien caste lo que llega de T  una lista de product:
            List<Product> list = (List<Product>)response.Result;

            //aqui convierto la lista a una lista de obsevableCollection:
            this.Products = new ObservableCollection<Product>(list);

            this.IsRefreshing = false;


        }
        #endregion
    }
}
