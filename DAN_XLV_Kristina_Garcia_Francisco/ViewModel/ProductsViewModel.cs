using DAN_XLV_Kristina_Garcia_Francisco.Command;
using DAN_XLV_Kristina_Garcia_Francisco.Model;
using DAN_XLV_Kristina_Garcia_Francisco.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DAN_XLV_Kristina_Garcia_Francisco.ViewModel
{
    /// <summary>
    /// Products ViewModel
    /// </summary>
    class ProductsViewModel : BaseViewModel
    {
        ManagerWindow manager;
        StorekeeperWindow storekeeper;
        Service service = new Service();

        #region Constructor
        /// <summary>
        /// Constructor with Manager Window param
        /// </summary>
        /// <param name="managerOpen">opens the manager window</param>
        public ProductsViewModel(ManagerWindow managerOpen)
        {
            manager = managerOpen;
            UnstoredProduct = service.GetAllProducts().Where(product => product.Stored == false).ToList();
            StoredProduct = service.GetAllProducts().Where(product => product.Stored == true).ToList();
            Priview();
        }

        /// <summary>
        /// Constructor with Storekeeper Window param
        /// </summary>
        /// <param name="storekeeperOpen">opens the storekeeper window</param>
        public ProductsViewModel(StorekeeperWindow storekeeperOpen)
        {
            storekeeper = storekeeperOpen;
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

        private string storeLabel;
        public string StoreLabel
        {
            get
            {
                return storeLabel;
            }
            set
            {
                storeLabel = value;
                OnPropertyChanged("StoreLabel");
            }
        }
        private string storeInfoColor;
        public string StoreInfoColor
        {
            get
            {
                return storeInfoColor;
            }
            set
            {
                storeInfoColor = value;
                OnPropertyChanged("StoreInfoColor");
            }
        }

        #endregion

        #region Event
        /// <summary>
        /// Delegate used to send store notifications depending on the parameter value.
        /// </summary>
        /// <param name="text">text that is being shown to the user</param>
        /// <param name="infoColor">info color that is shown to the user</param>
        public delegate void StoreNotification(string text, string infoColor);
        /// <summary>
        /// Event that gets triggered when a text is given
        /// </summary>
        public event StoreNotification OnStoreNotification;
        /// <summary>
        /// Checks if there is any given value to trigger the event
        /// </summary>
        /// <param name="text">Parameter given to notify</param>
        /// <param name="infoColor">Parameter color given to notify</param>
        internal void StoreNotify(string text, string infoColor)
        {
            if (OnStoreNotification != null)
            {
                OnStoreNotification(text, infoColor);
            }
        }
        #endregion

        public void StoreInfoValue(string text, string infoColor)
        {
            StoreLabel = text;
            StoreInfoColor = infoColor;
        }

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
                        service.Notify("Deleted " + product.ProductName + ", code " + product.ProductCode + ", quantity " + product.Quantity + ", price " + product.Price);
                        int productID = Product.ProductID;
                        service.DeleteProduct(productID);
                        UnstoredProduct = service.GetAllProducts().Where(product => product.Stored == false).ToList();
                        Priview();
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

                    // Save update to string
                    string productUpdateText = "Updated " + product.ProductName + ", code " + product.ProductCode + ", quantity " + product.Quantity + ", price " + product.Price;

                    StoredProduct = service.GetAllProducts().Where(product => product.Stored == true).ToList();
                    UnstoredProduct = service.GetAllProducts().Where(product => product.Stored == false).ToList();
                   
                    // Save to file only if the data was updated
                    if ((addProduct.DataContext as AddProductViewModel).IsUpdateProduct == true)
                    {
                        service.Notify(productUpdateText);
                    }
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
                    service.Notify("Added " + UnstoredProduct.LastOrDefault().ProductName + ", code " + UnstoredProduct.LastOrDefault().ProductCode 
                        + ", quantity " + UnstoredProduct.LastOrDefault().Quantity + ", price " + UnstoredProduct.LastOrDefault().Price);
                }
                Priview();
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
            OnStoreNotification = StoreInfoValue;
            return true;
        }

        /// <summary>
        /// Command that tries to store the product
        /// </summary>
        private ICommand storeProduct;
        public ICommand StoreProduct
        {
            get
            {
                if (storeProduct == null)
                {
                    storeProduct = new RelayCommand(param => StoreProductExecute(), param => CanStoreProductExecute());
                }
                return storeProduct;
            }
        }

        /// <summary>
        /// Tries the execute the store product command
        /// </summary>
        private void StoreProductExecute()
        {
            OnStoreNotification = StoreInfoValue;
            try
            {
                int countUnstored = UnstoredProduct.Count;
                service.StoreProduct(Product);

                string storedProductName = product.ProductName;
                StoredProduct = service.GetAllProducts().Where(product => product.Stored == true).ToList();
                UnstoredProduct = service.GetAllProducts().Where(product => product.Stored == false).ToList();
                Priview();

                if (countUnstored != UnstoredProduct.Count)
                {
                    StoreNotify("Stored the product " + storedProductName +
                        "\nCurrent storage count is: " + service.TotalQuantity(), "#28A745");
                }
                else
                {
                    StoreNotify("Failed to store the product\nbecause it exceedes the total store capacity.", "#FFC107");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to store the product
        /// </summary>
        protected bool CanStoreProductExecute()
        {
            int storeQuantity = service.TotalQuantity();

            if (storeQuantity >= 100)
            {
                OnStoreNotification = StoreInfoValue;
                StoreNotify("Current storage count is: " + storeQuantity, "#5BCED0");
                return false;
            }
            else
            {
                return true;
            }
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

                // Closes the current window
                Application.Current.Windows[0].Close();
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
