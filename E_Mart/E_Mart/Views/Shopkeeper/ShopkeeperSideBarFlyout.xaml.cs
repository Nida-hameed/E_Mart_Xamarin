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

namespace E_Mart.Views.Shopkeeper
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShopkeeperSideBarFlyout : ContentPage
    {
        public ListView ListView;

        public ShopkeeperSideBarFlyout()
        {
            InitializeComponent();

            BindingContext = new ShopkeeperSideBarFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class ShopkeeperSideBarFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<ShopkeeperSideBarFlyoutMenuItem> MenuItems { get; set; }

            public ShopkeeperSideBarFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<ShopkeeperSideBarFlyoutMenuItem>(new[]
                {
                    new ShopkeeperSideBarFlyoutMenuItem { Id = 0,  Title = "Dashboard",   },
                    new ShopkeeperSideBarFlyoutMenuItem { Id = 1,  Title = "Add Category", TargetType = typeof(Shopkeeper.Add_PRO_CATEGORY)  },
                    new ShopkeeperSideBarFlyoutMenuItem { Id = 2,  Title = "Manage Categories", TargetType = typeof(Shopkeeper.Manage_Categories) },
                    new ShopkeeperSideBarFlyoutMenuItem { Id = 3,  Title = "Add Product", TargetType = typeof(Shopkeeper.Add_PRODUCT) },
                    new ShopkeeperSideBarFlyoutMenuItem { Id = 4,  Title = "Manage Products",TargetType = typeof(Shopkeeper.Manage_Products)  },
                    new ShopkeeperSideBarFlyoutMenuItem { Id = 5,  Title = "Add Shop",  TargetType = typeof(Shopkeeper.Add_SHOP)},
                    new ShopkeeperSideBarFlyoutMenuItem { Id = 6,  Title = "Manage Shop" , TargetType = typeof(Shopkeeper.Add_PRO_CATEGORY)},
                    new ShopkeeperSideBarFlyoutMenuItem { Id = 7,  Title = "New Orders" },
                    new ShopkeeperSideBarFlyoutMenuItem { Id = 8,  Title = "Reports" },
                    new ShopkeeperSideBarFlyoutMenuItem { Id = 9,  Title = "Rent Status" },
                    new ShopkeeperSideBarFlyoutMenuItem { Id = 10, Title = "My Account" },
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