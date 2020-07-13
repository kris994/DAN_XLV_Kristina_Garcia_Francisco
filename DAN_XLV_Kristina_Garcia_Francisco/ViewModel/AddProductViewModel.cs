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
    /// Add Product ViewModel
    /// </summary>
    class AddProductViewModel : BaseViewModel
    {
        AddProduct addProduct;
        Service service = new Service();

        #region Constructor
        /// <summary>
        /// Constructor with the add product info window opening
        /// </summary>
        /// <param name="addProductOpen">opends the add product window</param>
        public AddProductViewModel(AddProduct addProductOpen)
        {
            product = new tblProduct();
            addProduct = addProductOpen;
            productList = service.GetAllProducts().ToList();
        }


        /// <summary>
        /// Constructor with edit product window opening
        /// </summary>
        /// <param name="addProductOpen">opens the add product window</param>
        /// <param name="productEdit">gets the product info that is being edited</param>
        public AddProductViewModel(AddProduct addProductOpen, tblProduct productEdit)
        {
            product = productEdit;
            addProduct = addProductOpen;
            productList = service.GetAllProducts().ToList();
        }
        #endregion

        #region Property
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

        private List<tblProduct> productList;
        public List<tblProduct> ProductList
        {
            get
            {
                return productList;
            }
            set
            {
                productList = value;
                OnPropertyChanged("ProductList");
            }
        }

        /// <summary>
        /// Checks if its possible to execute the add and edit commands
        /// </summary>
        private bool isUpdateProduct;
        public bool IsUpdateProduct
        {
            get
            {
                return isUpdateProduct;
            }
            set
            {
                isUpdateProduct = value;
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Command that tries to save the new product
        /// </summary>
        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => this.CanSaveExecute);
                }
                return save;
            }
        }

        /// <summary>
        /// Tries the execute the save command
        /// </summary>
        private void SaveExecute()
        {
            try
            {
                service.AddProduct(Product);
                IsUpdateProduct = true;

                addProduct.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to save the product
        /// </summary>
        protected bool CanSaveExecute
        {
            get
            {
                return Product.IsValid;
            }
        }

        /// <summary>
        /// Command that closes the add product or edit worker window
        /// </summary>
        private ICommand cancel;
        public ICommand Cancel
        {
            get
            {
                if (cancel == null)
                {
                    cancel = new RelayCommand(param => CancelExecute(), param => CanCancelExecute());
                }
                return cancel;
            }
        }

        /// <summary>
        /// Executes the close command
        /// </summary>
        private void CancelExecute()
        {
            try
            {
                addProduct.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to execute the close command
        /// </summary>
        /// <returns>true</returns>
        private bool CanCancelExecute()
        {
            return true;
        }
        #endregion
    }
}
