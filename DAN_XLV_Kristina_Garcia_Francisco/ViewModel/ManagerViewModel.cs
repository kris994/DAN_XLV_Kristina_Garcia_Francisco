using DAN_XLV_Kristina_Garcia_Francisco.Command;
using DAN_XLV_Kristina_Garcia_Francisco.Model;
using DAN_XLV_Kristina_Garcia_Francisco.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DAN_XLV_Kristina_Garcia_Francisco.ViewModel
{
    class ManagerViewModel : BaseViewModel
    {
        ManagerWindow manager;
        Service service = new Service();

        #region Constructor
        /// <summary>
        /// Constructor with Manager Window param
        /// </summary>
        /// <param name="managerOpen">opens the manager window</param>
        public ManagerViewModel(ManagerWindow managerOpen)
        {
            manager = managerOpen;
            UnstoredProduct = service.GetAllProducts().Where(product => product.Stored == false).ToList();
            StoredProduct = service.GetAllProducts().Where(product => product.Stored == true).ToList();
            Priview();
        }
        #endregion

        #region Property
        /// <summary>
        /// List of unstored Products
        /// </summary>
        private List<tblProduct> unstoredProduct;
        public List<tblProduct> UnstoredProduct
        {
            get
            {
                return unstoredProduct;
            }
            set
            {
                unstoredProduct = value;
                OnPropertyChanged("UnstoredProduct");
            }
        }

        /// <summary>
        /// List of stored Products
        /// </summary>
        private List<tblProduct> storedProduct;
        public List<tblProduct> StoredProduct
        {
            get
            {
                return storedProduct;
            }
            set
            {
                storedProduct = value;
                OnPropertyChanged("StoredProduct");
            }
        }

        /// <summary>
        /// Specific Product
        /// </summary>
        private tblProduct product;
        public tblProduct Product
        {
            get
            {
                return product;
            }
            set
            {
                product = value;
                OnPropertyChanged("Product");
            }
        }

        /// <summary>
        /// Show unstored products
        /// </summary>
        private Visibility unstoredProductExists;
        public Visibility UnstoredProductExists
        {
            get
            {
                return unstoredProductExists;
            }
            set
            {
                unstoredProductExists = value;
                OnPropertyChanged("UnstoredProductExists");
            }
        }

        /// <summary>
        /// Show stored products
        /// </summary>
        private Visibility storedProductExists;
        public Visibility StoredProductExists
        {
            get
            {
                return storedProductExists;
            }
            set
            {
                storedProductExists = value;
                OnPropertyChanged("StoredProductExists");
            }
        }       
        #endregion

        /// <summary>
        /// Check if the product table will be shown or not depending if its empty or not
        /// </summary>
        public void Priview()
        {
            if (UnstoredProduct.Any())
            {
                UnstoredProductExists = Visibility.Visible;
            }
            else
            {
                UnstoredProductExists = Visibility.Collapsed;
            }

            if (StoredProduct.Any())
            {
                StoredProductExists = Visibility.Visible;
            }
            else
            {
                StoredProductExists = Visibility.Collapsed;
            }
        }

        #region Commands
        /// <summary>
        /// Command that tries to delete the product
        /// </summary>
        private ICommand deleteProduct;
        public ICommand DeleteProduct
        {
            get
            {
                if (deleteProduct == null)
                {
                    deleteProduct = new RelayCommand(param => DeleteProductExecute(), param => CanDeleteProductExecute());
                }
                return deleteProduct;
            }
        }

        /// <summary>
        /// Executes the delete command
        /// </summary>
        public void DeleteProductExecute()
        {
            // Checks if the user really wants to delete the product
            var result = MessageBox.Show("Are you sure you want to delete the product?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (Product != null)
                    {
                        int productID = Product.ProductID;
                        service.DeleteProduct(productID);
                        UnstoredProduct = service.GetAllProducts().ToList();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Checks if the product can be deleted
        /// </summary>
        /// <returns>true if possible</returns>
        public bool CanDeleteProductExecute()
        {
            if (UnstoredProduct == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Command that tries to open the edit product window
        /// </summary>
        private ICommand editProduct;
        public ICommand EditProduct
        {
            get
            {
                if (editProduct == null)
                {
                    editProduct = new RelayCommand(param => EditProductExecute(), param => CanEditProductExecute());
                }
                return editProduct;
            }
        }

        /// <summary>
        /// Executes the edit command
        /// </summary>
        public void EditProductExecute()
        {
            try
            {
                if (Product != null)
                {
                    AddProduct addProduct = new AddProduct(Product);
                    addProduct.ShowDialog();

                    StoredProduct = service.GetAllProducts().Where(product => product.Stored == true).ToList();
                    UnstoredProduct = service.GetAllProducts().Where(product => product.Stored == false).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if the product can be edited
        /// </summary>
        /// <returns>true if possible</returns>
        public bool CanEditProductExecute()
        {
            return true;
        }

        /// <summary>
        /// Command that tries to add a new product
        /// </summary>
        private ICommand addNewProduct;
        public ICommand AddNewProduct
        {
            get
            {
                if (addNewProduct == null)
                {
                    addNewProduct = new RelayCommand(param => AddNewProductExecute(), param => CanAddNewProductExecute());
                }
                return addNewProduct;
            }
        }

        /// <summary>
        /// Executes the add product command
        /// </summary>
        private void AddNewProductExecute()
        {
            try
            {
                AddProduct addProduct = new AddProduct();
                addProduct.ShowDialog();
                if ((addProduct.DataContext as AddProductViewModel).IsUpdateProduct == true)
                {
                    UnstoredProduct = service.GetAllProducts().Where(product => product.Stored == false).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to add the new product
        /// </summary>
        /// <returns>true</returns>
        private bool CanAddNewProductExecute()
        {
            return true;
        }

        /// <summary>
        /// Command that logs off the user
        /// </summary>
        private ICommand logoff;
        public ICommand Logoff
        {
            get
            {
                if (logoff == null)
                {
                    logoff = new RelayCommand(param => LogoffExecute(), param => CanLogoffExecute());
                }
                return logoff;
            }
        }

        /// <summary>
        /// Executes the logoff command
        /// </summary>
        private void LogoffExecute()
        {
            try
            {
                Login login = new Login();
                manager.Close();
                login.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to logoff
        /// </summary>
        /// <returns>true</returns>
        private bool CanLogoffExecute()
        {
            return true;
        }
        #endregion
    }
}
