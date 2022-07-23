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
    public partial class Manage_Cities : ContentPage
    {
        public Manage_Cities()
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
            DataList.ItemsSource = (await App.firebaseDatabase.Child("CITY_tbl").OnceAsync<CITY_tbl>()).Select(x => new CITY_tbl
            {
                CITY_ID = x.Object.CITY_ID,
                CITY_NAME = x.Object.CITY_NAME,
                POSTAL_CODE = x.Object.POSTAL_CODE,

            }).ToList();
        }


        private async void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            var Selected = e.Item as CITY_tbl;
            var item = (await App.firebaseDatabase.Child("CITY_tbl").OnceAsync<CITY_tbl>()).FirstOrDefault(a => a.Object.CITY_ID == Selected.CITY_ID);


            var Choice = await DisplayActionSheet("Options", "Cancel", "OK", "Delete", "View", "Edit");

            if (Choice == "View")
            {

                await DisplayAlert("Detail", "" +
                    "\nCITY_ID          : " + item.Object.CITY_ID +
                    "\nCITY_NAME        : " + item.Object.CITY_NAME +
                    "\nPOSTAL_CODE         : " + item.Object.POSTAL_CODE, "", "OK");
            }
            if (Choice== "Edit")
            {

            }
            if (Choice == "Delete")
            {

                var Confirm = await DisplayAlert("Confirmation", "Are you sure you want to delete " +item.Object.CITY_NAME, "Yes", "No");
                if (Confirm)
                {
                    await App.firebaseDatabase.Child("CITY_tbl").Child(item.Key).DeleteAsync();
                    LoadData();
                    await DisplayAlert("Message", item.Object.CITY_NAME + "Deleted Permanently", "OK");

                }


            }
        }

    }


}
