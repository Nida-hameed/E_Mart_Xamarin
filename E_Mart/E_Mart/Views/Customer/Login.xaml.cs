using E_Mart.Models;
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
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }
        private async void btnLogin_Clicked(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                await DisplayAlert("Error", "Please fillout all requried fields!", "OK");
                return;
            }
            try
            {

                ProgressInd.IsRunning = true;
                var Check = (await App.firebaseDatabase.Child("CUSTOMER_tbl").OnceAsync<CUSTOMER_tbl>()).FirstOrDefault(x => x.Object.CUSTOMER_EMAIL == txtEmail.Text && x.Object.CUSTOMER_PASSWORD == txtPassword.Text);

                if (Check == null)
                {
                    ProgressInd.IsRunning = false;
                    await DisplayAlert("Error", "Wrong Credentials.", "OK");
                    return;
                }
                else
                {
                    App.Current.MainPage = new CustomerSideBar();
                }
            }



            catch (Exception ex)
            {
                ProgressInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong, Please Try Again later.\n Error: " + ex.Message, "OK");

            }


        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            
            await Navigation.PushAsync(new Register());
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new StartUpPage();
        }
    }
}