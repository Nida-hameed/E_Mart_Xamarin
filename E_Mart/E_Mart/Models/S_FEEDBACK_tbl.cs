using System;

namespace E_Mart.Models
{

    public partial class S_FEEDBACK_tbl
    {
       
        public int S_FEEDBACK_ID { get; set; }

        public int SHOPKEEPER_FID { get; set; }

        public string S_FEEDBACK_DESCRIPTION { get; set; }

       
        public DateTime S_FEEDBACK_DATE { get; set; }

    }
}
