using E_Mart.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace E_Mart.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}