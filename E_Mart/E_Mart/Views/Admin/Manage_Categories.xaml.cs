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
            DataList.ItemsSource = (await App.firebaseDatabase.Child("SHP_CATEGORY_tbl").OnceAsync<SHP_CATEGORY_tbl>()).Select(x => new SHP_CATEGORY_tbl
            {
                SHP_CATEGORY_ID = x.Object.SHP_CATEGORY_ID,
                SHP_CATEGORY_NAME = x.Object.SHP_CATEGORY_NAME,
                SHP_CATEGORY_STATUS = x.Object.SHP_CATEGORY_STATUS,
                SHP_CATEGORY_ICON = x.Object.SHP_CATEGORY_ICON,
                
                

            }).ToList();
        }


        private async void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            var Selected = e.Item as SHP_CATEGORY_tbl;
            var item = (await App.firebaseDatabase.Child("SHP_CATEGORY_tbl").OnceAsync<SHP_CATEGORY_tbl>()).FirstOrDefault(a => a.Object.SHP_CATEGORY_ID == Selected.SHP_CATEGORY_ID);


            var Choice = await DisplayActionSheet("Options", "Cancel", "OK", "Delete", "View", "Edit");

            if (Choice == "View")
            {

                await DisplayAlert("Detail", "" +
                    "\nSHP_CATEGORY_ID     : " + item.Object.SHP_CATEGORY_ID +
                    "\nSHP_CATEGORY_NAME    : " + item.Object.SHP_CATEGORY_NAME +
                    "\nSHP_CATEGORY_STATUS : " + item.Object.SHP_CATEGORY_STATUS +
                    "\nSHP_CATEGORY_ICON   : " + item.Object.SHP_CATEGORY_ICON, "", "OK");

            }
            if (Choice== "Edit")
            {

            }
            if (Choice == "Delete")
            {

                var Confirm = await DisplayAlert("Confirmation", "Are you sure you want to delete " +item.Object.SHP_CATEGORY_NAME, "Yes", "No");
                if (Confirm)
                {
                    await App.firebaseDatabase.Child("SHP_CATEGORY_tbl").Child(item.Key).DeleteAsync();
                    LoadData();
                    await DisplayAlert("Message", item.Object.SHP_CATEGORY_NAME + "Deleted Permanently", "OK");

                }


            }
        }

    }


}
