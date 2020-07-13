using DAN_XLV_Kristina_Garcia_Francisco.Model;
using System.Collections.Generic;

namespace DAN_XLV_Kristina_Garcia_Francisco.Helper
{
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

        public string IsZero(int number)
        {
            if (number <= 0)
            {
                return "Cannot be zero";
            }
            else
            {
                return null;
            }
        }
    }
}
