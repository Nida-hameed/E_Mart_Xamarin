using System;
namespace E_Mart.Models { 

   public partial class C_FEEDBACK_tbl
   {
      
        public int C_FEEDBACK_ID { get; set; }

        public int CUSTOMER_FID { get; set; }

        public string C_FEEDBACK_DESCRIPTION { get; set; }

        
        public DateTime C_FEEDBACK_DATE { get; set; }

        
   }
}
