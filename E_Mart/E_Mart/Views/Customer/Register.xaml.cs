using E_Mart.Models;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace E_Mart.Views.Customer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        public Register()
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


                var Check = (await App.firebaseDatabase.Child("CUSTOMER_tbl").OnceAsync<CUSTOMER_tbl>()).FirstOrDefault(x => x.Object.CUSTOMER_EMAIL == txtEmail.Text);

                if (Check != null)
                {
                    await DisplayAlert("Error", "This email is already registered.", "OK");
                    return;
                }

                ProgressInd.IsRunning = true;

                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("CUSTOMER_tbl").OnceAsync<CUSTOMER_tbl>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("CUSTOMER_tbl").OnceAsync<CUSTOMER_tbl>()).Max(a => a.Object.CUSTOMER_ID);
                    NewID = ++LastID;
                }



                CUSTOMER_tbl cus = new CUSTOMER_tbl()
                {
                    CUSTOMER_ID = NewID,
                    CUSTOMER_NAME = txtName.Text,
                    CUSTOMER_AGE = txtAge.Text,
                    CUSTOMER_CNIC = txtCNIC.Text,
                    CUSTOMER_CONTACT = txtContact.Text,
                    CUSTOMER_EMAIL = txtEmail.Text,
                    CUSTOMER_PASSWORD = txtPassword.Text,
                    CUSTOMER_ADDRESS = txtAddress.Text,

                };
                
                
                await App.firebaseDatabase.Child("CUSTOMER_tbl").PostAsync(cus);

                await DisplayAlert("Success", "Account Registered", "OK");
                await Navigation.PushAsync(new Login());


            }
            catch (Exception ex)
            {
                ProgressInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong, Please Try Again later.\n Erroe: " + ex.Message, "OK");

            }
        }
    }
}