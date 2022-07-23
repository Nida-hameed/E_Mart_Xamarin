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
    public partial class RegisterAdmin : ContentPage
    {
        public RegisterAdmin()
        {
            InitializeComponent();
        }
        private async void btnRegister_Clicked(object sender, EventArgs e)
        {

            try
            {

                if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtCNIC.Text) || string.IsNullOrEmpty(txtContact.Text) || string.IsNullOrEmpty(txtAddress.Text) || string.IsNullOrEmpty(txtPassword.Text))
                {

                    await DisplayAlert("Error", "Please fillout all requried fields and Try Again!", "OK");
                    return;
                }

                if (txtPassword.Text != txtCPassword.Text)
                {
                    await DisplayAlert("Error", "Passwords do not match.", "OK");
                    return;
                }


                var Check = (await App.firebaseDatabase.Child("ADMIN_tbl").OnceAsync<ADMIN_tbl>()).FirstOrDefault(x => x.Object.ADMIN_EMAIL == txtEmail.Text);

                if (Check != null)
                {
                    await DisplayAlert("Error", "This email is already registered.", "OK");
                    return;
                }

                ProgressInd.IsRunning = true;

                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("ADMIN_tbl").OnceAsync<ADMIN_tbl>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("ADMIN_tbl").OnceAsync<ADMIN_tbl>()).Max(a => a.Object.ADMIN_ID);
                    NewID = ++LastID;
                }



                ADMIN_tbl admin = new ADMIN_tbl()
                {
                    ADMIN_ID = NewID,
                    ADMIN_NAME = txtName.Text,
                    ADMIN_CNIC = txtCNIC.Text,
                    ADMIN_CONTACT = txtContact.Text,
                    ADMIN_EMAIL = txtEmail.Text,
                    ADMIN_PASSWORD = txtPassword.Text,
                    ADMIN_ADDRESS = txtAddress.Text,

                };
                
                
                await App.firebaseDatabase.Child("ADMIN_tbl").PostAsync(admin);

                await DisplayAlert("Success", "Account Registered", "OK");
                await Navigation.PushAsync(new LoginAdmin());


            }
            catch (Exception ex)
            {
                ProgressInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong, Please Try Again later.\n Erroe: " + ex.Message, "OK");

            }
        }
    }
}