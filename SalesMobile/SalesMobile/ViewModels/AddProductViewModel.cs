namespace SalesMobile.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using SalesCommon;
    using SalesMobile.Helpers;
    using SalesMobile.Services;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class AddProductViewModel : BaseViewModel
    {
        #region Services

        private APIService aPIService;

        #endregion

        #region Attributes
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Properties
        public string Description { get; set; }
        public string Price { get; set; }
        public string Remarks { get; set; }
        public bool IsRunning
        {
            get => isRunning;

            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                if (isEnabled != value)
                {
                    isEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Contructors
        public AddProductViewModel()
        {
            //Services
            aPIService = new APIService();

            IsEnabled = true;
        }
        #endregion

        #region Commands
        public ICommand SaveCommand { get => new RelayCommand(Save); }

        #endregion

        #region Methods
        private async void Save()
        {
            if (string.IsNullOrEmpty(this.Description))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.DescriptionError, Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.Price))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.PriceError, Languages.Accept);

                return;
            }

            decimal price = decimal.Parse(Price);

            if (price < 0)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.PriceError, Languages.Accept);
                return;
            }


            IsRunning = true;
            IsEnabled = false;

            //check intternet:
            var connection = await aPIService.CheckConnection();
            if (!connection.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;

                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);

                return;
            }

            //aqui capturo loas datos que el usurio ingreso por el formulario:
            var product = new Product()
            {
              Description = Description,
              Price = price,
              Remarks = Remarks,
              
            };

            //aqyui consumo el API del post:
            string urlProducts = Application.Current.Resources["UrlApiProducts"].ToString();
            string prefix = Application.Current.Resources["Prefix"].ToString();
            string ProductsController = Application.Current.Resources["ProductsController"].ToString();
            var response = await this.aPIService.Post(urlProducts, prefix, ProductsController, product);

            if (!response.IsSuccess)
            {

                IsRunning = false;
                IsEnabled = true;

                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);   
                return;

            }

            

            IsRunning = false;
            IsEnabled = true;
            
            //Aqui regreso a la pagina anterior
            await Application.Current.MainPage.Navigation.PopAsync();


        }

        #endregion
    }
}
