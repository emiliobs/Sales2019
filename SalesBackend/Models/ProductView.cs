namespace SalesBackend.Models
{
    using SalesCommon;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    public class ProductView : Product
    {
        public HttpPostedFileBase ImageFile { get; set; }
    }
}