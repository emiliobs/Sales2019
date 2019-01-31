namespace SalesMobile.ViewModels
{
    using SalesCommon;
    using SalesMobile.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using Xamarin.Forms;

    public class ProductViewModel : BaseViewModel
    {
        #region Services
        private APIService apiService;
        #endregion

        #region Attributes

        private ObservableCollection<Product> products;

        #endregion

        #region Properties
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

        #region Methods
        private async void LoadProducts()
        {
            var response = await this.apiService.GetList<Product>("https://salesapiservice.azurewebsites.net", "/api", "/Products");

            //Aqui pregunto si llego algo de la api
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error,",response.Message,"Accept.");

                return;
            }

            //aqui ya llego una lista de objecto: tambien caste lo que llega de T  una lista de product:
            var list = (List<Product>)response.Result;

            //aqui convierto la lista a una lista de obsevableCollection:
            this.Products = new ObservableCollection<Product>(list);


        }
        #endregion
    }
}
