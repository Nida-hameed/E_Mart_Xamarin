namespace E_Mart.Models
{

    public partial class SHOP_tbl
    {

        public int SHOP_ID { get; set; }

        public string SHOP_NAME { get; set; }


        public string SHOP_ADDRESS { get; set; }

        public string SHOP_IMAGE { get; set; }

        public int SHP_CATEGORY_FID { get; set; }

        public string SHOP_RENT { get; set; }

        public int SHOPKEEPER_FID { get; set; }

        public int CITY_FID { get; set; }

       
    }
}
