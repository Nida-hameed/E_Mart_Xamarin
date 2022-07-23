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
    public partial class Manage_Categories : ContentPage
    {
        public Manage_Categories()
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
            DataList.ItemsSource = (await App.firebaseDatabase.Child("PRO_CATEGORY_tbl").OnceAsync<PRO_CATEGORY_tbl>()).Select(x => new PRO_CATEGORY_tbl
            {
                PRO_CATEGORY_ID = x.Object.PRO_CATEGORY_ID,
                PRO_CATEGORY_NAME = x.Object.PRO_CATEGORY_NAME,
                PRO_CATEGORY_STATUS = x.Object.PRO_CATEGORY_STATUS,
                PRO_CATEGORY_ICON = x.Object.PRO_CATEGORY_ICON,
                SHOP_FID = x.Object.SHOP_FID,
                

            }).ToList();
        }


        private async void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            var Selected = e.Item as PRO_CATEGORY_tbl;
            var item = (await App.firebaseDatabase.Child("PRO_CATEGORY_tbl").OnceAsync<PRO_CATEGORY_tbl>()).FirstOrDefault(a => a.Object.PRO_CATEGORY_ID == Selected.PRO_CATEGORY_ID);


            var Choice = await DisplayActionSheet("Options", "Cancel", "OK", "Delete", "View", "Edit");

            if (Choice == "View")
            {

                await DisplayAlert("Detail", "" +
                    "\nPRO_CATEGORY_ID     : " + item.Object.PRO_CATEGORY_ID +
                    "\nPRO_CATEGORY_NAME    : " + item.Object.PRO_CATEGORY_NAME +
                    "\nPRO_CATEGORY_STATUS : " + item.Object.PRO_CATEGORY_STATUS +
                    "\nPRO_CATEGORY_ICON   : " + item.Object.PRO_CATEGORY_ICON +
                    "\nSHOP_FID : " + item.Object.SHOP_FID, "", "OK");
            }
            if (Choice== "Edit")
            {

            }
            if (Choice == "Delete")
            {

                var Confirm = await DisplayAlert("Confirmation", "Are you sure you want to delete " +item.Object.PRO_CATEGORY_NAME, "Yes", "No");
                if (Confirm)
                {
                    await App.firebaseDatabase.Child("PRO_CATEGORY_tbl").Child(item.Key).DeleteAsync();
                    LoadData();
                    await DisplayAlert("Message", item.Object.PRO_CATEGORY_NAME + "Deleted Permanently", "OK");

                }


            }
        }

    }


}
