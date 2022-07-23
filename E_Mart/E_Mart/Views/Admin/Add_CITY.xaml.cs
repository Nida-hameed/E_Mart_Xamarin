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
    public partial class Add_CITY : ContentPage
    {
     
        public Add_CITY()
        {
            InitializeComponent();
        }

        

        private async void btnAddCity_Clicked(object sender, EventArgs e)
        {

            try
            {

                if (string.IsNullOrEmpty(txtCityName.Text) || string.IsNullOrEmpty(txtPostalCode.Text) )
                {

                    await DisplayAlert("Error", "Please fillout all requried fields and Try Again!", "OK");
                    return;
                }




                var Check = (await App.firebaseDatabase.Child("CITY_tbl").OnceAsync<CITY_tbl>()).FirstOrDefault(x => x.Object.CITY_NAME == txtCityName.Text);

                if (Check != null)
                {
                    await DisplayAlert("Error", "This City is already added.", "OK");
                    return;
                }

                ProgressInd.IsRunning = true;

                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("CITY_tbl").OnceAsync<CITY_tbl>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("CITY_tbl").OnceAsync<CITY_tbl>()).Max(a => a.Object.CITY_ID);
                    NewID = ++LastID;
                }



                CITY_tbl city = new CITY_tbl()
                {
                    CITY_ID = NewID,
                    CITY_NAME = txtCityName.Text,
                    POSTAL_CODE= txtPostalCode.Text,         

                };


                await App.firebaseDatabase.Child("CITY_tbl").PostAsync(city);

                await DisplayAlert("Success", "City Added.", "OK");
                await Navigation.PushAsync(new Manage_Cities());


            }
            catch (Exception ex)
            {
                ProgressInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong, Please Try Again later.\n Erroe: " + ex.Message, "OK");

            }
        }
    }
}