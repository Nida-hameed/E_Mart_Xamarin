using E_Mart.Models;
using Firebase.Database.Query;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
    public partial class Add_SHOP : ContentPage
    {
        public static string PicPath = "ImagePicker.png";
        public Add_SHOP()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                var response = await DisplayActionSheet("Select Image Source", "Close", "", "From Gallery", "From Camera");
                if (response == "From Camera")
                {
                    await CrossMedia.Current.Initialize();
                    if (!CrossMedia.Current.IsTakePhotoSupported)
                    {
                        await DisplayAlert("Error", "Phone is not Take Photo Supported", "OK");
                        return;
                    }

                    var mediaOptions = new StoreCameraMediaOptions()
                    {
                        PhotoSize = PhotoSize.Medium
                    };

                    var SelectedImg = await CrossMedia.Current.TakePhotoAsync(mediaOptions);

                    if (SelectedImg == null)
                    {
                        await DisplayAlert("Error", "Error Picking Image...", "OK");
                        return;
                    }

                    PicPath = SelectedImg.Path;
                    PreviewPic.Source = SelectedImg.Path;


                }
                if (response == "From Gallery")
                {
                    await CrossMedia.Current.Initialize();
                    if (!CrossMedia.Current.IsPickPhotoSupported)
                    {
                        await DisplayAlert("Error", "Phone is not Pick Photo Supported", "OK");
                        return;
                    }

                    var mediaOptions = new PickMediaOptions()
                    {
                        PhotoSize = PhotoSize.Medium
                    };

                    var SelectedImg = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

                    if (SelectedImg == null)
                    {
                        await DisplayAlert("Error", "Error Picking Image...", "OK");
                        return;
                    }

                    PicPath = SelectedImg.Path;
                    PreviewPic.Source = SelectedImg.Path;


                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Message", "Something Went wrong \n" + ex.Message, "OK");

            }

        }

        private async void btnAddShop_Clicked(object sender, EventArgs e)
        {

            try
            {

                if (string.IsNullOrEmpty(txtShopName.Text) || string.IsNullOrEmpty(txtShopAddress.Text)|| string.IsNullOrEmpty(txtShopAddress.Text)|| string.IsNullOrEmpty(txtCityFID.Text)|| string.IsNullOrEmpty(txtShopCatFID.Text)|| string.IsNullOrEmpty(txtShopkeeperFID.Text)|| string.IsNullOrEmpty(txtShopRent.Text) )
                {

                    await DisplayAlert("Error", "Please fillout all requried fields and Try Again!", "OK");
                    return;
                }




                var Check = (await App.firebaseDatabase.Child("SHOP_tbl").OnceAsync<SHOP_tbl>()).FirstOrDefault(x => x.Object.SHOP_NAME == txtShopName.Text);

                if (Check != null)
                {
                    await DisplayAlert("Error", "This Shop is already added.", "OK");
                    return;
                }

                ProgressInd.IsRunning = true;

                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("SHOP_tbl").OnceAsync<SHOP_tbl>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("SHOP_tbl").OnceAsync<SHOP_tbl>()).Max(a => a.Object.SHOP_ID);
                    NewID = ++LastID;
                }



                SHOP_tbl shp = new SHOP_tbl()
                {
                    SHOP_ID = NewID,
                    SHOP_NAME = txtShopName.Text,
                    SHOP_ADDRESS = txtShopAddress.Text,
                    SHOP_IMAGE = PicPath,
                    //SHP_CATEGORY_FID = txtShopCatFID.Text,
                    //CITY_FID = txtCityFID.Text,
                    //SHOPKEEPER_FID = txtShopkeeperFID.Text,
                    SHOP_RENT = txtShopRent.Text,

                };


                await App.firebaseDatabase.Child("SHOP_tbl").PostAsync(shp);

                await DisplayAlert("Success", "Shop Added.", "OK");
                await Navigation.PushAsync(new Add_SHOP());


            }
            catch (Exception ex)
            {
                ProgressInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong, Please Try Again later.\n Erroe: " + ex.Message, "OK");

            }
        }

    }
}