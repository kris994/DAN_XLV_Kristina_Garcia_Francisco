using DAN_XLV_Kristina_Garcia_Francisco.Helper;
using System.ComponentModel;

namespace DAN_XLV_Kristina_Garcia_Francisco.Model
{
    public partial class tblProduct : IDataErrorInfo
    {
        Validations validation = new Validations();

        /// <summary>
        /// Total amount of propertis we are checking
        /// </summary>
        static readonly string[] ValidatedProperties =
        {
            "ProductCode",
            "Price",
            "Quantity"
        };

        /// <summary>
        /// Returns true if this object has no validation errors.
        /// </summary>
        public bool IsValid
        {
            get
            {
                foreach (string property in ValidatedProperties)
                {
                    // there is an error
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }

        /// <summary>
        /// Checks if the inputs are incorrect
        /// </summary>
        public string Error
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Does validations on the property location
        /// </summary>
        /// <param name="propertyName">property we are checking</param>
        /// <returns>if the property is valid (null) or error (string)</returns>
        public string this[string propertyName]
        {
            get
            {
                string result = null;

                switch (propertyName)
                {
                    case "ProductCode":
                        result = this.validation.CodeChecker(ProductCode, ProductID);
                        break;

                    case "Price":
                        result = this.validation.IsDouble(Price);
                        break;

                    case "Quantity":
                        result = this.validation.IsZero(Quantity);
                        break;

                    default:
                        result = null;
                        break;
                }

                return result;
            }
        }
    }
}
