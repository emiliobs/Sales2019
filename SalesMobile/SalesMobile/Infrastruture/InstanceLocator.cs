namespace SalesMobile.Infrastruture
{
    using SalesMobile.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
    }
}
