namespace SalesMobile.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class MainViewModel
    {
        #region Properties
        public ProductViewModel  Product { get; set; }
        #endregion


        #region Constructor
        public MainViewModel()
        {
            this.Product = new ProductViewModel();
        }
        #endregion
    }
}
