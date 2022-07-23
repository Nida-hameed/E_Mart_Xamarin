using E_Mart.Models;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace E_Mart.Views.Shopkeeper
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Manage_Products : ContentPage
    {
        public Manage_Products()
        {
            InitializeComponent();

           
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {

                ProgressInd.IsRunning = true;  
                LoadData();
                ProgressInd.IsRunning = false;

            }
            catch (Exception ex)
            {
                ProgressInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong, Please Try Again later.\n Erroe: " + ex.Message, "OK");
                throw;

            }
            

        }

        async void LoadData()
        {
            DataList.ItemsSource = (await App.firebaseDatabase.Child("PRODUCT_tbl").OnceAsync<PRODUCT_tbl>()).Select(x => new PRODUCT_tbl
            {
                PRODUCT_ID = x.Object.PRODUCT_ID,
                PRODUCT_NAME = x.Object.PRODUCT_NAME,
                PRODUCT_IMAGE= x.Object.PRODUCT_IMAGE,
                PRODUCT_DESCRIPTION = x.Object.PRODUCT_DESCRIPTION,
                PRODUCT_PURCHASEPRICE = x.Object.PRODUCT_PURCHASEPRICE,
                PRODUCT_SALEPRICE = x.Object.PRODUCT_SALEPRICE,
                PRODUCT_AVAILABILITY = x.Object.PRODUCT_AVAILABILITY,
                PRO_CATEGORY_FID = x.Object.PRO_CATEGORY_FID,

            }).ToList();
        }


        private async void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            var Selected = e.Item as PRODUCT_tbl;
            var item = (await App.firebaseDatabase.Child("PRODUCT_tbl").OnceAsync<PRODUCT_tbl>()).FirstOrDefault(a => a.Object.PRODUCT_ID == Selected.PRODUCT_ID);


            var Choice = await DisplayActionSheet("Options", "Cancel", "OK", "Delete", "View", "Edit");

            if (Choice == "View")
            {

                await DisplayAlert("Detail", "" +
                    "\nPRODUCT_ID     : " + item.Object.PRODUCT_ID +
                    "\nPRODUCT_NAME    : " + item.Object.PRODUCT_NAME +
                    "\nPRODUCT_IMAGE : " + item.Object.PRODUCT_IMAGE +
                    "\nPRODUCT_DESCRIPTION    : " + item.Object.PRODUCT_DESCRIPTION +
                    "\nPRODUCT_SALEPRICE : " + item.Object.PRODUCT_SALEPRICE +
                    "\nPRODUCT_PURCHASEPRICE : " + item.Object.PRODUCT_PURCHASEPRICE +
                    "\nPRO_CATEGORY_FID : " + item.Object.PRO_CATEGORY_FID +
                    "\nPRODUCT_AVAILABILITY : " + item.Object.PRODUCT_AVAILABILITY, "", "OK");
            }
            if (Choice== "Edit")
            {

            }
            if (Choice == "Delete")
            {

                var Confirm = await DisplayAlert("Confirmation", "Are you sure you want to delete " +item.Object.PRODUCT_NAME, "Yes", "No");
                if (Confirm)
                {
                    await App.firebaseDatabase.Child("PRODUCT_tbl").Child(item.Key).DeleteAsync();
                    LoadData();
                    await DisplayAlert("Message", item.Object.PRODUCT_NAME + "Deleted Permanently", "OK");

                }


            }
        }

    }


}
