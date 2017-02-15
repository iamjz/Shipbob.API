using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipbob.Models
{
    public class OrderInfo
    {
        public string TrackingID { get; set; }

        public string RecipFirstName { get; set; }
        public string RecipLastName { get; set; }
        public string RecipAddress { get; set; }
        public string RecipCity { get; set; }
        public string RecipState { get; set; }
        public string RecipCountry { get; set; }
        public string RecipZip { get; set; }

        public Dictionary<string, int> ItemsQty { get; set; }
        public Dictionary<string, int> BundlesQty { get; set; }
    }
}
