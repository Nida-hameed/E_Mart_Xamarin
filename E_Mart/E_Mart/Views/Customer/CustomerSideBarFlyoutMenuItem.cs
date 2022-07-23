using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.Views.Customer
{
    public class CustomerSideBarFlyoutMenuItem
    {
        public CustomerSideBarFlyoutMenuItem()
        {
            TargetType = typeof(CustomerSideBarFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}