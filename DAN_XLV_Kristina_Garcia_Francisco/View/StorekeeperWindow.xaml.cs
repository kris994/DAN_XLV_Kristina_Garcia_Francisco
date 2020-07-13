using DAN_XLV_Kristina_Garcia_Francisco.ViewModel;
using System.Windows;

namespace DAN_XLV_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for StorekeeperWindow.xaml
    /// </summary>
    public partial class StorekeeperWindow : Window
    {
        /// <summary>
        /// Storekeeper Window
        /// </summary>
        public StorekeeperWindow()
        {
            InitializeComponent();
            this.DataContext = new ProductsViewModel(this);
        }
    }
}
