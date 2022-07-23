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

namespace E_Mart.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminSideBarFlyout : ContentPage
    {
        public ListView ListView;

        public AdminSideBarFlyout()
        {
            InitializeComponent();

            BindingContext = new AdminSideBarFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class AdminSideBarFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<AdminSideBarFlyoutMenuItem> MenuItems { get; set; }

            public AdminSideBarFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<AdminSideBarFlyoutMenuItem>(new[]
                {
                     new AdminSideBarFlyoutMenuItem { Id = 0, Title = "Dashboard",  },
                     new AdminSideBarFlyoutMenuItem { Id = 1, Title = "New Messages", },
                     new AdminSideBarFlyoutMenuItem { Id = 2, Title = "Add Shopkeeper", TargetType= typeof(SHOPKEEPER_List) },
                     new AdminSideBarFlyoutMenuItem { Id = 3, Title = "Manage Shopkeepers", TargetType = typeof(RegisterShopkeeper)  },
                     new AdminSideBarFlyoutMenuItem { Id = 4, Title = "Add Category", TargetType= typeof(Add_SHP_CATEGORY)   },
                     new AdminSideBarFlyoutMenuItem { Id = 5, Title = "Manage Categories", TargetType= typeof(Manage_Categories) },
                     new AdminSideBarFlyoutMenuItem { Id = 6, Title = "Add City", TargetType= typeof(Add_CITY) },
                     new AdminSideBarFlyoutMenuItem { Id = 7, Title = "Manage Cities", TargetType= typeof(Manage_Cities) },
                     new AdminSideBarFlyoutMenuItem { Id = 8, Title = "Rent" },
                     new AdminSideBarFlyoutMenuItem { Id = 9, Title = "Add Admin", TargetType= typeof(RegisterAdmin)  },
                     new AdminSideBarFlyoutMenuItem { Id = 10,Title = "My Account" },
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