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
    public partial class Add_PRO_CATEGORY : ContentPage
        
    {
        private MediaFile _mediaFile;
        public static string PicPath = "ImagePicker.png";
        public Add_PRO_CATEGORY()
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
                    _mediaFile = SelectedImg;

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
                    _mediaFile = SelectedImg;

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

        private async void btnAddCategory_Clicked(object sender, EventArgs e)
        {

            try
            {

                if (string.IsNullOrEmpty(txtProCatName.Text) || string.IsNullOrEmpty(txtProCatStatus.Text) )
                {

                    await DisplayAlert("Error", "Please fillout all requried fields and Try Again!", "OK");
                    return;
                }




                var Check = (await App.firebaseDatabase.Child("PRO_CATEGORY_tbl").OnceAsync<PRO_CATEGORY_tbl>()).FirstOrDefault(x => x.Object.PRO_CATEGORY_NAME == txtProCatName.Text);

                if (Check != null)
                {
                    await DisplayAlert("Error", "This category is already added.", "OK");
                    return;
                }

                ProgressInd.IsRunning = true;

                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("PRO_CATEGORY_tbl").OnceAsync<PRO_CATEGORY_tbl>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("PRO_CATEGORY_tbl").OnceAsync<PRO_CATEGORY_tbl>()).Max(a => a.Object.PRO_CATEGORY_ID);
                    NewID = ++LastID;
                }
                      
                     var StoredImageURL = await App.FirebaseStorage
                    .Child("catImages")
                    .Child(NewID+"_"+txtProCatName.Text + ".jpg")
                    .PutAsync(_mediaFile.GetStream());




        PRO_CATEGORY_tbl cat = new PRO_CATEGORY_tbl()
                {
                    PRO_CATEGORY_ID = NewID,
                    PRO_CATEGORY_NAME = txtProCatName.Text,
                    PRO_CATEGORY_STATUS = txtProCatStatus.Text,
                    PRO_CATEGORY_ICON = StoredImageURL,
                    SHOP_FID = 1,

                };


                await App.firebaseDatabase.Child("PRO_CATEGORY_tbl").PostAsync(cat);

                await DisplayAlert("Success", "Category Added.", "OK");
                await Navigation.PushAsync(new Manage_Categories());


            }
            catch (Exception ex)
            {
                ProgressInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong, Please Try Again later.\n Erroe: " + ex.Message, "OK");

            }
        }
    }
}