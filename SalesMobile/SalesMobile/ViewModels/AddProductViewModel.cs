﻿namespace SalesMobile.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using SalesMobile.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class AddProductViewModel :BaseViewModel
    {
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

            var price = decimal.Parse(Price);

            if (price < 0)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.PriceError, Languages.Accept);
                return;
            }

            await Application.Current.MainPage.DisplayAlert(Languages.Error, "Todo va bien......", Languages.Accept);


        }

        #endregion
    }
}
