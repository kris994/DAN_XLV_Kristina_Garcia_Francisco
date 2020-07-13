using DAN_XLV_Kristina_Garcia_Francisco.ViewModel;
using System.Windows;

namespace DAN_XLV_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public ManagerWindow()
        {
            InitializeComponent();
            this.DataContext = new ManagerViewModel(this);
        }
    }
}
