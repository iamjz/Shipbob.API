using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shipbob.Models;

namespace Shipbob.Repositories
{
    public interface IShipbobRepository
    {
        OrderInfo SearchForOrder(string trackingID);
        List<string> GetItemBundleNames();
        bool CreateNewOrder(OrderInfo orderInput);
    }
}
