
using E_Mart.Services;
using E_Mart.Views;
using Firebase.Database;
using Firebase.Storage;
using SQLite;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace E_Mart
{
    public partial class App : Application
    {
       // public static string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "dbE_Mart.db3");
        //public static SQLiteConnection db = new SQLiteConnection(dbPath);

        //Firebase Connections ======================================
        public static FirebaseStorage FirebaseStorage = new FirebaseStorage("gs://e-mart1.appspot.com");

        public static FirebaseClient firebaseDatabase = new FirebaseClient("https://e-mart1-default-rtdb.firebaseio.com/");


        public App()
        {


            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new StartUpPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
