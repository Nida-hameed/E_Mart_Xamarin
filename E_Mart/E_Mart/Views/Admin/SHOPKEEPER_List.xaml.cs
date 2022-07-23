using E_Mart.Models;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace E_Mart.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SHOPKEEPER_List : ContentPage
    {
        public SHOPKEEPER_List()
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
            DataList.ItemsSource = (await App.firebaseDatabase.Child("SHOPKEEPER_tbl").OnceAsync<SHOPKEEPER_tbl>()).Select(x => new SHOPKEEPER_tbl
            {
                SHOPKEEPER_ID = x.Object.SHOPKEEPER_ID,
                SHOPKEEPER_NAME = x.Object.SHOPKEEPER_NAME,
                SHOPKEEPER_CONTACT = x.Object.SHOPKEEPER_CONTACT,
                SHOPKEEPER_CNIC = x.Object.SHOPKEEPER_CNIC,
                SHOPKEEPER_EMAIL = x.Object.SHOPKEEPER_EMAIL,
                SHOPKEEPER_PASSWORD = x.Object.SHOPKEEPER_PASSWORD,

            }).ToList();
        }


        private async void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            var Selected = e.Item as SHOPKEEPER_tbl;
            var item = (await App.firebaseDatabase.Child("SHOPKEEPER_tbl").OnceAsync<SHOPKEEPER_tbl>()).FirstOrDefault(a => a.Object.SHOPKEEPER_ID == Selected.SHOPKEEPER_ID);


            var Choice = await DisplayActionSheet("Options", "Cancel", "OK", "Delete", "View", "Edit");

            if (Choice == "View")
            {

                await DisplayAlert("Detail", "" +
                    "\nSHOPKEEPER_ID      : " + item.Object.SHOPKEEPER_ID +
                    "\nSHOPKEEPER_NAME    : " + item.Object.SHOPKEEPER_NAME +
                    "\nSHOPKEEPER_CONTACT : " + item.Object.SHOPKEEPER_CONTACT +
                    "\nSHOPKEEPER_PASSWORD:   ******" +
                    "\nSHOPKEEPER_CNIC    : " + item.Object.SHOPKEEPER_CNIC +
                    "\nSHOPKEEPER_ADDRESS : " + item.Object.SHOPKEEPER_ADDRESS +
                    "\nSHOPKEEPER_EMAIL   : " + item.Object.SHOPKEEPER_EMAIL, "", "OK");
            }
            if (Choice== "Edit")
            {

            }
            if (Choice == "Delete")
            {

                var Confirm = await DisplayAlert("Confirmation", "Are you sure you want to delete " +item.Object.SHOPKEEPER_NAME, "Yes", "No");
                if (Confirm)
                {
                    await App.firebaseDatabase.Child("SHOPKEEPER_tbl").Child(item.Key).DeleteAsync();
                    LoadData();
                    await DisplayAlert("Message", item.Object.SHOPKEEPER_NAME + "Deleted Permanently", "OK");

                }


            }
        }

    }


}
