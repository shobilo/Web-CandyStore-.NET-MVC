using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Domain.Abstract;
using Domain.Entities;
using UI.Controllers;
using UI.ViewModels;
using UI.HtmlUoW;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            // Организация (arrange)
            Mock<ICandyRepository> mock = new Mock<ICandyRepository>();
            mock.Setup(m => m.Candies).Returns(new List<Candy>
            {
                new Candy { CandyId = 1, Name = "Candy1"},
                new Candy { CandyId = 2, Name = "Candy2"},
                new Candy { CandyId = 3, Name = "Candy3"},
                new Candy { CandyId = 4, Name = "Candy4"},
                new Candy { CandyId = 5, Name = "Candy5"}
            });
            CandyController controller = new CandyController(mock.Object);
            controller.pageSize = 3;

            // Действие (act)
            CandiesListViewModel result = (CandiesListViewModel)controller.List(2).Model;

            // Утверждение (assert)
            List<Candy> candies = result.Candies.ToList();
            Assert.IsTrue(candies.Count == 2);
            Assert.AreEqual(candies[0].Name, "Candy4");
            Assert.AreEqual(candies[1].Name, "Candy5");
        }
        public void Can_Generate_Page_Links()
        {

            // Организация - определение вспомогательного метода HTML - это необходимо
            // для применения расширяющего метода
            HtmlHelper myHelper = null;

            // Организация - создание объекта PagingInfo
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            // Организация - настройка делегата с помощью лямбда-выражения
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Действие
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Утверждение
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        }
        public void Can_Send_Pagination_View_Model()
        {
            // Организация (arrange)
            Mock<ICandyRepository> mock = new Mock<ICandyRepository>();
            mock.Setup(m => m.Candies).Returns(new List<Candy>
            {
                new Candy { CandyId = 1, Name = "Candy1"},
                new Candy { CandyId = 2, Name = "Candy2"},
                new Candy { CandyId = 3, Name = "Candy3"},
                new Candy { CandyId = 4, Name = "Candy4"},
                new Candy { CandyId = 5, Name = "Candy5"}
            });
            CandyController controller = new CandyController(mock.Object);
            controller.pageSize = 3;

            // Act
            CandiesListViewModel result
                = (CandiesListViewModel)controller.List(2).Model;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }
    }
}
