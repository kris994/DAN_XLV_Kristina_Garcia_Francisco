﻿using DAN_XLV_Kristina_Garcia_Francisco.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace DAN_XLV_Kristina_Garcia_Francisco
{
    /// <summary>
    /// Class that includes all CRUD functions of the application
    /// </summary>
    class Service
    {
        /// <summary>
        /// Gets all information about products
        /// </summary>
        /// <returns>a list of found products</returns>
        public List<tblProduct> GetAllProducts()
        {
            try
            {
                using (ProductDBEntities context = new ProductDBEntities())
                {
                    List<tblProduct> list = new List<tblProduct>();
                    list = (from x in context.tblProducts select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        internal object GetAllReports()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates or edits a product
        /// </summary>
        /// <param name="product">the product that is added</param>
        /// <returns>a new or edited product</returns>
        public tblProduct AddProduct(tblProduct product)
        {
            try
            {
                using (ProductDBEntities context = new ProductDBEntities())
                {
                    if (product.ProductID == 0)
                    {
                        tblProduct newProduct = new tblProduct
                        {
                            ProductName = product.ProductName,
                            ProductCode = product.ProductCode,
                            Quantity = product.Quantity,
                            Price = product.Price,
                            Stored = false
                        };

                        context.tblProducts.Add(newProduct);
                        context.SaveChanges();
                        product.ProductID = newProduct.ProductID;
                        return product;
                    }
                    else
                    {
                        tblProduct productToEdit = (from ss in context.tblProducts where ss.ProductID == product.ProductID select ss).First();

                        productToEdit.ProductName = product.ProductName;
                        productToEdit.ProductCode = product.ProductCode;
                        productToEdit.Quantity = product.Quantity;
                        productToEdit.Price = product.Price;
                        productToEdit.Stored = product.Stored;
                        productToEdit.ProductID = product.ProductID;

                        context.SaveChanges();
                        return product;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Search if product with that ID exists in the product table
        /// </summary>
        /// <param name="productID">Takes the product id that we want to search for</param>
        /// <returns>True if the product exists</returns>
        public bool IsProductID(int productID)
        {
            try
            {
                using (ProductDBEntities context = new ProductDBEntities())
                {
                    int product = (from x in context.tblProducts where x.ProductID == productID select x.ProductID).FirstOrDefault();

                    if (product != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception " + ex.Message.ToString());
                return false;
            }
        }

        /// <summary>
        /// Deletes the product depending if the productID already exists
        /// </summary>
        /// <param name="productID">the product that is being deleted</param>
        /// <returns>list of products</returns>
        public void DeleteProduct(int productID)
        {
            try
            {
                using (ProductDBEntities context = new ProductDBEntities())
                {
                    bool isProduct = IsProductID(productID);

                    if (isProduct == true)
                    {
                        // find the product removing them
                        tblProduct productToDelete = (from r in context.tblProducts where r.ProductID == productID select r).First();

                        context.tblProducts.Remove(productToDelete);
                        context.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Cannot delete the product");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
    }
}