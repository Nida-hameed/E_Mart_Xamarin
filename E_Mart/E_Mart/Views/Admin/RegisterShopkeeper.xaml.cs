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
    public partial class RegisterShopkeeper : ContentPage
    {
        public RegisterShopkeeper()
        {
            InitializeComponent();
        }
        private async void btnRegister_Clicked(object sender, EventArgs e)
        {

            try
            {

                if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtContact.Text) || string.IsNullOrEmpty(txtAddress.Text) || string.IsNullOrEmpty(txtPassword.Text))
                {

                    await DisplayAlert("Error", "Please fillout all requried fields and Try Again!", "OK");
                    return;
                }

                if (txtPassword.Text != txtCPassword.Text)
                {
                    await DisplayAlert("Error", "Passwords do not match.", "OK");
                    return;
                }


                var Check = (await App.firebaseDatabase.Child("SHOPKEEPER_tbl").OnceAsync<SHOPKEEPER_tbl>()).FirstOrDefault(x => x.Object.SHOPKEEPER_EMAIL == txtEmail.Text);

                if (Check != null)
                {
                    await DisplayAlert("Error", "This email is already registered.", "OK");
                    return;
                }

                ProgressInd.IsRunning = true;

                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("SHOPKEEPER_tbl").OnceAsync<SHOPKEEPER_tbl>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("SHOPKEEPER_tbl").OnceAsync<SHOPKEEPER_tbl>()).Max(a => a.Object.SHOPKEEPER_ID);
                    NewID = ++LastID;
                }



                SHOPKEEPER_tbl s = new SHOPKEEPER_tbl()
                {
                    SHOPKEEPER_ID = NewID,
                    SHOPKEEPER_NAME = txtName.Text,
                    SHOPKEEPER_CNIC = txtCNIC.Text,
                    SHOPKEEPER_CONTACT = txtContact.Text,
                    SHOPKEEPER_EMAIL = txtEmail.Text,
                    SHOPKEEPER_PASSWORD = txtPassword.Text,
                    SHOPKEEPER_ADDRESS = txtAddress.Text,

                };
                
                
                await App.firebaseDatabase.Child("SHOPKEEPER_tbl").PostAsync(s);

                await DisplayAlert("Success", "Account Registered", "OK");
                await Navigation.PushAsync(new SHOPKEEPER_List());


            }
            catch (Exception ex)
            {
                ProgressInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong, Please Try Again later.\n Erroe: " + ex.Message, "OK");

            }
        }
    }
}