using System;

namespace E_Mart.Models
{
   

    public partial class ORDER_tbl
    {
     
      
        public int ORDER_ID { get; set; }

        public DateTime ORDER_DATE { get; set; }

        public int SHOP_FID { get; set; }

        public int? CUSTOMER_FID { get; set; } 
        public int? SHOPKEEPER_FID { get; set; } 

        public string ORDER_STATUS { get; set; }

        public string PAYMENT_MODE { get; set; }
      
        public string NAME { get; set; }
       
        public string ADDRESS { get; set; }
       
        public string MOBILE_NO { get; set; }
       
        public string EMAIL { get; set; }

       
    }  
}

