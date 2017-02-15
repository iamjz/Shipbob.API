using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shipbob.Models;
using Shipbob.Data;

namespace Shipbob.Repositories
{
    public class ShipbobRepository : IShipbobRepository
    {
        public OrderInfo SearchForOrder(string trackingID)
        {
            OrderInfo ret = new OrderInfo();

            try
            {
                using (var context = new ShipbobEntities())
                {
                    var resp = (from o in context.Orders
                                join i in context.Items
                                on o.ItemID equals i.ItemID
                                where o.TrackingID == trackingID
                                select new
                                {
                                    o.TrackingID,
                                    o.ItemID,
                                    i.ItemName,
                                    o.Qty,
                                    o.RecipFirstName,
                                    o.RecipLastName,
                                    o.RecipAddress,
                                    o.RecipCity,
                                    o.RecipState,
                                    o.RecipCountry,
                                    o.RecipZip
                                }).ToList();

                    if (resp != null && resp.Count > 0)
                    {
                        var firstItem = resp.FirstOrDefault();

                        OrderInfo order = new OrderInfo();
                        order.TrackingID = firstItem.TrackingID;
                        order.RecipFirstName = firstItem.RecipFirstName;
                        order.RecipLastName = firstItem.RecipLastName;
                        order.RecipAddress = firstItem.RecipAddress;
                        order.RecipCity = firstItem.RecipCity;
                        order.RecipState = firstItem.RecipState;
                        order.RecipCountry = firstItem.RecipCountry;
                        order.RecipZip = firstItem.RecipZip;
                        order.ItemsQty = new Dictionary<string, int>();

                        foreach (var o in resp)
                        {
                            if (!order.ItemsQty.ContainsKey(o.ItemName))
                            {
                                order.ItemsQty.Add(o.ItemName, o.Qty);
                            }
                        }

                        ret = order;
                    }
                }
            }
            catch(Exception e)
            {
                throw new Exception("Error: " + e.Message);
            }

            return ret;
        }

        public List<string> GetItemBundleNames()
        {
            List<string> Names = new List<string>();

            try
            {
                using (var context = new ShipbobEntities())
                {
                    var list = (from items in context.Items
                                select new
                                {
                                    name = items.ItemName.ToString()
                                }).Union
                                (from bundles in context.Bundles
                                 select new
                                 {
                                     name = bundles.BundleName.ToString()
                                 }).ToList();

                    foreach (var l in list)
                    {
                        Names.Add(l.name);
                    }
                }
            }
            catch(Exception e)
            {
                throw new Exception("Error: " + e.Message);
            }

            return Names;
        }

        private Dictionary<string, int>  ConvertBundleToItems(OrderInfo orderInput)
        {
            Dictionary<string, int> returnItems = new Dictionary<string, int>();

            try
            {
                using (var context = new ShipbobEntities())
                {
                    Dictionary<string, int> inputItems = orderInput.ItemsQty;

                    if (orderInput.BundlesQty != null && orderInput.BundlesQty.Count > 0)
                    {
                        //convert the bundles to items
                        //add those items to the inputItems dictionary.

                        var bundle = (from bundles in context.Bundles
                                      join items in context.Items
                                      on bundles.ItemID equals items.ItemID
                                      where orderInput.BundlesQty.Keys.Contains(bundles.BundleName)
                                      select new
                                      {
                                          bundles.BundleName,
                                          items.ItemName,
                                          bundles.ItemQty
                                      }).ToList();

                        foreach (var b in bundle)
                        {
                            int itemQty = 0;
                            int itemsQtyToAdd = 0;
                            inputItems.TryGetValue(b.ItemName, out itemQty);
                            orderInput.BundlesQty.TryGetValue(b.BundleName, out itemsQtyToAdd);
                            itemsQtyToAdd = b.ItemQty * itemsQtyToAdd;
                            inputItems[b.ItemName] = itemQty + itemsQtyToAdd;
                        }
                    }

                    returnItems = inputItems;
                }
            }
            catch(Exception e)
            {
                throw new Exception("Error: " + e.Message);
            }

            return returnItems;
        }

        public bool CreateNewOrder(OrderInfo orderInput)
        {
            bool ret = false;

            try
            {
                Dictionary<string, int> inputItems = ConvertBundleToItems(orderInput);

                using (var context = new ShipbobEntities())
                {
                    foreach (var i in inputItems)
                    {
                        var item = (from items in context.Items
                                    where items.ItemName == i.Key
                                    select items).FirstOrDefault();

                        if (item == null)
                        {
                            throw new Exception("Invalid Item: " + item.ItemName);
                        }

                        Order order = new Order();
                        order.TrackingID = orderInput.TrackingID;
                        order.RecipFirstName = orderInput.RecipFirstName;
                        order.RecipLastName = orderInput.RecipLastName;
                        order.RecipAddress = orderInput.RecipAddress;
                        order.RecipCity = orderInput.RecipCity;
                        order.RecipState = orderInput.RecipState;
                        order.RecipCountry = orderInput.RecipCountry;
                        order.RecipZip = orderInput.RecipZip;
                        order.ItemID = item.ItemID;
                        order.Qty = i.Value;
                        context.Orders.Add(order);
                    }

                    ret = (context.SaveChanges() > 0);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error: " + e.Message);
            }

            return ret;
        }
    }
}
