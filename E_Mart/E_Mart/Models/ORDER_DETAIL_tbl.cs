namespace E_Mart.Models
{


    
    public partial class ORDER_DETAIL_tbl
    {
       
        public int ORDER_DETAIL_ID { get; set; }

        public int PRODUCT_FID { get; set; }

        
        public int PRO_ORDER_QUANTITY { get; set; }

        public int ORDER_FID { get; set; }

       
        public decimal PURCHASE_PRICE { get; set; }

       
        public decimal SALE_PRICE { get; set; }

       
    }
}
