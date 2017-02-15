using System;
using Shipbob.API;
using Shipbob.API.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shipbob.Models;
using Shipbob.Repositories;
using System.Collections.Generic;
using System.Web.Http.Results;

namespace Shipbob.API.Tests.Controllers
{
    [TestClass]
    public class OrderControllerTest
    {
        [TestMethod]
        public void WhenTrackingIdProvided_OrderInfoShouldReturn()
        {
            OrderController controller = new OrderController(new StubShipBobRepo());
            OrderInfo expectedResult = new OrderInfo();
            expectedResult.TrackingID = "1234567";
            expectedResult.RecipFirstName = "Jacky";
            expectedResult.RecipLastName = "Zhao";
            expectedResult.RecipAddress = "123 Fake Street";
            expectedResult.RecipCity = "Austin";
            expectedResult.RecipState = "Texas";
            expectedResult.RecipCountry = "United States";
            expectedResult.RecipZip = "78727";

            var actionResult = controller.Get("1234567");
            var response = actionResult as OkNegotiatedContentResult<OrderInfo>;

            Assert.IsNotNull(response);
            Assert.AreEqual(expectedResult.RecipFirstName, response.Content.RecipFirstName);
        }

        [TestMethod]
        public void ShouldGetListOfItemBundleNames()
        {
            OrderController controller = new OrderController(new StubShipBobRepo());
            List<string> expectedResult = new List<string>();
            expectedResult.Add("Apple");
            expectedResult.Add("Banana");
            expectedResult.Add("Mixed Fruit Basket");

            var actionResult = controller.ListNames();
            var response = actionResult as OkNegotiatedContentResult<List<string>>;

            Assert.IsNotNull(response);
            Assert.AreEqual(expectedResult[0], response.Content[0]);
            Assert.AreEqual(expectedResult[1], response.Content[1]);
            Assert.AreEqual(expectedResult[2], response.Content[2]);
        }

        [TestMethod]
        public void ShouldCreateNewOrderWithValidInput()
        {
            OrderController controller = new OrderController(new StubShipBobRepo());
            OrderInfo input = new OrderInfo();
            input.ItemsQty = new Dictionary<string, int>();
            input.ItemsQty.Add("Apple", 100);

            var actionResult = controller.Post(input);
            var response = actionResult as OkNegotiatedContentResult<string>;

            Assert.IsNotNull(response);
            Assert.AreEqual("Your order has been created!", response.Content);
        }
    }

    public class StubShipBobRepo : IShipbobRepository
    {
        public bool CreateNewOrder(OrderInfo orderInput)
        {
            return true;
        }

        public List<string> GetItemBundleNames()
        {
            List<string> fakeItemBundle = new List<string>();
            fakeItemBundle.Add("Apple");
            fakeItemBundle.Add("Banana");
            fakeItemBundle.Add("Mixed Fruit Basket");

            return fakeItemBundle;
        }

        public OrderInfo SearchForOrder(string trackingID)
        {
            OrderInfo fakeResult = new OrderInfo();

            fakeResult.TrackingID = "1234567";
            fakeResult.RecipFirstName = "Jacky";
            fakeResult.RecipLastName = "Zhao";
            fakeResult.RecipAddress = "123 Fake Street";
            fakeResult.RecipCity = "Austin";
            fakeResult.RecipState = "Texas";
            fakeResult.RecipCountry = "United States";
            fakeResult.RecipZip = "78727";

            return fakeResult;
        }
    }
}
