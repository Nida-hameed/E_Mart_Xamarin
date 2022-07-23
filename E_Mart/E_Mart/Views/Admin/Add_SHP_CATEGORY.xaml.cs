﻿using E_Mart.Models;
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

namespace E_Mart.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Add_SHP_CATEGORY : ContentPage
    {
        public static string PicPath = "ImagePicker.png";
        public Add_SHP_CATEGORY()
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

        private async void btnAddCategory_Clicked(object sender, EventArgs e)
        {

            try
            {

                if (string.IsNullOrEmpty(txtShpCatName.Text) || string.IsNullOrEmpty(txtShpCatStatus.Text) )
                {

                    await DisplayAlert("Error", "Please fillout all requried fields and Try Again!", "OK");
                    return;
                }




                var Check = (await App.firebaseDatabase.Child("SHP_CATEGORY_tbl").OnceAsync<SHP_CATEGORY_tbl>()).FirstOrDefault(x => x.Object.SHP_CATEGORY_NAME == txtShpCatName.Text);

                if (Check != null)
                {
                    await DisplayAlert("Error", "This category is already added.", "OK");
                    return;
                }

                ProgressInd.IsRunning = true;

                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("SHP_CATEGORY_tbl").OnceAsync<SHP_CATEGORY_tbl>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("SHP_CATEGORY_tbl").OnceAsync<SHP_CATEGORY_tbl>()).Max(a => a.Object.SHP_CATEGORY_ID);
                    NewID = ++LastID;
                }



                SHP_CATEGORY_tbl cat = new SHP_CATEGORY_tbl()
                {
                    SHP_CATEGORY_ID = NewID,
                    SHP_CATEGORY_NAME = txtShpCatName.Text,
                    SHP_CATEGORY_STATUS = txtShpCatStatus.Text,
                    SHP_CATEGORY_ICON = PicPath,
                    

                };


                await App.firebaseDatabase.Child("SHP_CATEGORY_tbl").PostAsync(cat);

                await DisplayAlert("Success", "Category Added.", "OK");
                await Navigation.PushAsync(new Add_SHP_CATEGORY());


            }
            catch (Exception ex)
            {
                ProgressInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong, Please Try Again later.\n Erroe: " + ex.Message, "OK");

            }
        }
    }
}