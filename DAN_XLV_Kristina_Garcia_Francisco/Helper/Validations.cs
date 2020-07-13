using DAN_XLV_Kristina_Garcia_Francisco.Model;
using System.Collections.Generic;
using System.Linq;

namespace DAN_XLV_Kristina_Garcia_Francisco.Helper
{
    /// <summary>
    /// Validates the user inputs
    /// </summary>
    class Validations
    {
        /// <summary>
        /// Checks if the code is valid
        /// </summary>
        /// <param name="productCode">code of the product</param>
        /// <param name="id">the product id who has the code</param>
        /// <returns>null if the input is correct or string error message if its wrong</returns>
        public string CodeChecker(string productCode, int id)
        {
            Service service = new Service();
            List<tblProduct> AllProducts = service.GetAllProducts();
            string currentProductCode = "";

            if (productCode == null || productCode.Length < 0)
            {
                return "Code cannot be empty.";
            }

            // Get the current id card
            if (AllProducts != null)
            {
                for (int i = 0; i < AllProducts.Count; i++)
                {
                    if (AllProducts[i].ProductID == id)
                    {
                        currentProductCode = AllProducts[i].ProductCode;
                        break;
                    }
                }

                // Check if the id card already exists, but it is not the current user jmbg
                for (int i = 0; i < AllProducts.Count; i++)
                {
                    if (AllProducts[i].ProductCode == productCode && currentProductCode != productCode)
                    {
                        return "This Product Code already exists!";
                    }
                }
            }

            return null;
        }

        public string IsDouble(string price)
        {
            if (double.TryParse(price, out double value) == false || value < 0)
            {
                return "Not a valid price";
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Checks if the input quantity is valid
        /// </summary>
        /// <param name="number">the quantity number</param>
        /// <param name="id">the product id</param>
        /// <returns></returns>
        public string InputQunaitity(int number, int id)
        {
            Service service = new Service();
            int currentQuantity = 0;

            if (number <= 0)
            {
                return "Cannot be zero";
            }

            // Get the current quantity
            if (service.GetAllProducts() != null)
            {
                for (int i = 0; i < service.GetAllProducts().Count; i++)
                {
                    if (service.GetAllProducts()[i].ProductID == id)
                    {
                        currentQuantity = service.GetAllProducts()[i].Quantity;
                        break;
                    }
                }
            }

            // Only when editing a product that was stored
            int totalQuantity = 0;

            if (service.GetAllProducts() != null)
            {
                if (service.GetAllProducts().Where(product => product.Stored == true) != null)
                {
                    var StoredProduct = service.GetAllProducts().Where(product => product.Stored == true).ToList();

                    for (int i = 0; i < StoredProduct.Count; i++)
                    {
                        totalQuantity = totalQuantity + StoredProduct[i].Quantity;
                    }

                    for (int i = 0; i < StoredProduct.Count; i++)
                    {
                        if (StoredProduct[i].ProductID == id)
                        {
                            if (totalQuantity - currentQuantity + number > 100 && currentQuantity != number)
                            {
                                return "Total cannot be bigger than 100";
                            }
                            break;
                        }
                    }
                }
            }

            return null;
        }
    }
}
