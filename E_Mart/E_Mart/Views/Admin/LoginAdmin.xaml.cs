using E_Mart.Models;
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
    public partial class LoginAdmin : ContentPage
    {
        public LoginAdmin()
        {
            InitializeComponent();
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {

            try
            {

                if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Text) )
                {

                    await DisplayAlert("Error", "Please fillout all requried fields!", "OK");
                    return;
                }



                ProgressInd.IsRunning = true;
                var Check = (await App.firebaseDatabase.Child("ADMIN_tbl").OnceAsync<ADMIN_tbl>()).FirstOrDefault(x => x.Object.ADMIN_EMAIL == txtEmail.Text && x.Object.ADMIN_PASSWORD == txtPassword.Text);


                if (Check == null)
                {
                    ProgressInd.IsRunning = false;
                    await DisplayAlert("Error", "Wrong Credentials.", "OK");
                    return;
                }
                else
                {
                    App.Current.MainPage = new AdminSideBar();
                }


              

            }
            catch (Exception ex)
            {
                ProgressInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong, Please Try Again later.\n Error: " + ex.Message, "OK");

            }

        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new StartUpPage();

        }

       
    }
}
   