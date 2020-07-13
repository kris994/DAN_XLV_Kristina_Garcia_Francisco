using DAN_XLV_Kristina_Garcia_Francisco.Model;
using DAN_XLV_Kristina_Garcia_Francisco.ViewModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace DAN_XLV_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        /// <summary>
        /// Window constructor for editing products
        /// </summary>
        /// <param name="productEdit">product that is bing edited</param>
        public AddProduct(tblProduct productEdit)
        {
            InitializeComponent();
            this.DataContext = new AddProductViewModel(this, productEdit);
        }

        /// <summary>
        /// Window constructor for creating products
        /// </summary>
        public AddProduct()
        {
            InitializeComponent();
            this.DataContext = new AddProductViewModel(this);
        }

        /// <summary>
        /// User can only imput numbers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
