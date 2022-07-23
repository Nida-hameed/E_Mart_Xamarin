using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace E_Mart.Views.Customer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerSideBarFlyout : ContentPage
    {
        public ListView ListView;

        public CustomerSideBarFlyout()
        {
            InitializeComponent();

            BindingContext = new CustomerSideBarFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class CustomerSideBarFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<CustomerSideBarFlyoutMenuItem> MenuItems { get; set; }

            public CustomerSideBarFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<CustomerSideBarFlyoutMenuItem>(new[]
                {
                    new CustomerSideBarFlyoutMenuItem { Id = 0, Title = "Home",   },
                    new CustomerSideBarFlyoutMenuItem { Id = 1, Title = "Shops",   },
                    new CustomerSideBarFlyoutMenuItem { Id = 2, Title = "Cart",  },
                    new CustomerSideBarFlyoutMenuItem { Id = 3, Title = "Checkout" },
                    new CustomerSideBarFlyoutMenuItem { Id = 4, Title = "Contact Us" },
                    new CustomerSideBarFlyoutMenuItem { Id = 4, Title = "Wishlist" },
                    new CustomerSideBarFlyoutMenuItem { Id = 5, Title = "My Account" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}