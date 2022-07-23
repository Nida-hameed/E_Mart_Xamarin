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
    public partial class Manage_Shop : ContentPage
    {
        public Manage_Shop()
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
            DataList.ItemsSource = (await App.firebaseDatabase.Child("SHOP_tbl").OnceAsync<SHOP_tbl>()).Select(x => new SHOP_tbl
            {
                SHOP_ID = x.Object.SHOP_ID,
                SHOP_NAME = x.Object.SHOP_NAME,
                SHOP_ADDRESS = x.Object.SHOP_ADDRESS,
                SHOP_IMAGE = x.Object.SHOP_IMAGE,
                SHP_CATEGORY_FID = x.Object.SHP_CATEGORY_FID,
                SHOP_RENT = x.Object.SHOP_RENT,
                SHOPKEEPER_FID = x.Object.SHOPKEEPER_FID,
                CITY_FID = x.Object.CITY_FID,

            }).ToList();
        }


        private async void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            var Selected = e.Item as SHOP_tbl;
            var item = (await App.firebaseDatabase.Child("SHOP_tbl").OnceAsync<SHOP_tbl>()).FirstOrDefault(a => a.Object.SHOP_ID == Selected.SHOP_ID);


            var Choice = await DisplayActionSheet("Options", "Cancel", "OK", "Delete", "View", "Edit");

            if (Choice == "View")
            {

                await DisplayAlert("Detail", "" +
                    "\nSHOP_ID          : " + item.Object.SHOP_ID +
                    "\nSHOP_NAME        : " + item.Object.SHOP_NAME +
                    "\nSHOP_ADDRESS     : " + item.Object.SHOP_ADDRESS +
                    "\nSHOP_IMAGE       : " + item.Object.SHOP_IMAGE +
                    "\nSHP_CATEGORY_FID : " + item.Object.SHP_CATEGORY_FID +
                    "\nSHOP_RENT        : " + item.Object.SHOP_RENT +
                    "\nSHOPKEEPER_FID   : " + item.Object.SHOPKEEPER_FID +
                    "\nCITY_FID         : " + item.Object.CITY_FID, "", "OK");
            }
            if (Choice== "Edit")
            {

            }
            if (Choice == "Delete")
            {

                var Confirm = await DisplayAlert("Confirmation", "Are you sure you want to delete " +item.Object.SHOP_NAME, "Yes", "No");
                if (Confirm)
                {
                    await App.firebaseDatabase.Child("SHOP_tbl").Child(item.Key).DeleteAsync();
                    LoadData();
                    await DisplayAlert("Message", item.Object.SHOP_NAME + "Deleted Permanently", "OK");

                }


            }
        }

    }


}
