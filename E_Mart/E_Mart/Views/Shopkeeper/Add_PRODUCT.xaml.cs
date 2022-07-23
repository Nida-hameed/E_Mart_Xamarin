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
    public partial class Add_PRODUCT : ContentPage
    {
        public static string PicPath = "ImagePicker.png";
        public Add_PRODUCT()
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

        private async void btnAddProduct_Clicked(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(txtPROName.Text) || string.IsNullOrEmpty(txtPROAvailibility.Text)|| string.IsNullOrEmpty(txtPROAvailibility.Text)|| string.IsNullOrEmpty(txtPROAvailibility.Text)|| string.IsNullOrEmpty(txtPRODescription.Text)|| string.IsNullOrEmpty(txtPROPurchasePrice.Text)|| string.IsNullOrEmpty(txtPROSalePrice.Text))
                {

                    await DisplayAlert("Error", "Please fillout all requried fields and Try Again!", "OK");
                    return;
                }




                var Check = (await App.firebaseDatabase.Child("PRODUCT_tbl").OnceAsync<PRODUCT_tbl>()).FirstOrDefault(x => x.Object.PRODUCT_NAME == txtPROName.Text);

                if (Check != null)
                {
                    await DisplayAlert("Error", "This Product is already added.", "OK");
                    return;
                }

                ProgressInd.IsRunning = true;

                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("PRODUCT_tbl").OnceAsync<PRODUCT_tbl>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("PRODUCT_tbl").OnceAsync<PRODUCT_tbl>()).Max(a => a.Object.PRODUCT_ID);
                    NewID = ++LastID;
                }



                PRODUCT_tbl pro = new PRODUCT_tbl()
                {
                    PRODUCT_ID = NewID,
                    PRODUCT_NAME = txtPROName.Text,
                    PRODUCT_DESCRIPTION = txtPRODescription.Text,
                    PRODUCT_IMAGE = PicPath,
                    //PRODUCT_SALEPRICE = txtPROSalePrice.decimal,
                    //PRODUCT_PURCHASEPRICE= txtPROPurchasePrice.decimal,
                    //PRODUCT_AVAILABILITY = txtPROAvailibility.Text,
                    //PRO_CATEGORY_FID = txtPROCategory.Text,


                };


                await App.firebaseDatabase.Child("PRODUCT_tbl").PostAsync(pro);

                await DisplayAlert("Success", "PRODUCT Added.", "OK");
                await Navigation.PushAsync(new Add_PRODUCT());


            }
            catch (Exception ex)
            {
                ProgressInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong, Please Try Again later.\n Erroe: " + ex.Message, "OK");

            }
        }
    }
}

       
  
