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
            CandiesListViewModel result = (CandiesListViewModel)controller.List(null, 2).Model;

            // Утверждение (assert)
            List<Candy> candies = result.Candies.ToList();
            Assert.IsTrue(candies.Count == 2);
            Assert.AreEqual(candies[0].Name, "Candy4");
            Assert.AreEqual(candies[1].Name, "Candy5");
        }
        [TestMethod]
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
        [TestMethod]
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
                = (CandiesListViewModel)controller.List(null, 2).Model;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }
        [TestMethod]
        public void Can_Filter_By_Category()
        {
            // Организация (arrange)
            Mock<ICandyRepository> mock = new Mock<ICandyRepository>();
            mock.Setup(m => m.Candies).Returns(new List<Candy>
            {
                new Candy { CandyId = 1, Name = "Candy1", Category="chocolate"},
                new Candy { CandyId = 2, Name = "Candy2", Category="flakes"},
                new Candy { CandyId = 3, Name = "Candy3", Category="chocolate"},
                new Candy { CandyId = 4, Name = "Candy4", Category="flakes"},
                new Candy { CandyId = 5, Name = "Candy5", Category="gum"}
            });
            CandyController controller = new CandyController(mock.Object);
            controller.pageSize = 3;

            // Action
            List<Candy> result = ((CandiesListViewModel)controller.List("flakes", 1).Model)
                .Candies.ToList();

            // Assert
            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result[0].Name == "Candy2" && result[0].Category == "flakes");
            Assert.IsTrue(result[1].Name == "Candy4" && result[1].Category == "flakes");
        }
        [TestMethod]
        public void Can_Create_Categories()
        {
            // Организация - создание имитированного хранилища
            Mock<ICandyRepository> mock = new Mock<ICandyRepository>();
            mock.Setup(m => m.Candies).Returns(new List<Candy> 
            {
                new Candy { CandyId = 1, Name = "Candy1", Category="chocolate"},
                new Candy { CandyId = 2, Name = "Candy2", Category="chocolate"},
                new Candy { CandyId = 3, Name = "Candy3", Category="flakes"},
                new Candy { CandyId = 4, Name = "Candy4", Category="gum"},
            });

            // Организация - создание контроллера
            NavCategoryController target = new NavCategoryController(mock.Object);

            // Действие - получение набора категорий
            List<string> results = ((IEnumerable<string>)target.Menu().Model).ToList();

            // Утверждение
            Assert.AreEqual(results.Count(), 3);
            Assert.AreEqual(results[0], "chocolate");
            Assert.AreEqual(results[1], "flakes");
            Assert.AreEqual(results[2], "gum");
        }
        [TestMethod]
        public void Indicates_Selected_Category()
        {
            // Организация - создание имитированного хранилища
            Mock<ICandyRepository> mock = new Mock<ICandyRepository>();
            mock.Setup(m => m.Candies).Returns(new Candy[] 
            {
                new Candy { CandyId = 1, Name = "Candy1", Category="chocolate"},
                new Candy { CandyId = 2, Name = "Candy2", Category="flakes"}
            });

            // Организация - создание контроллера
            NavCategoryController target = new NavCategoryController(mock.Object);

            // Организация - определение выбранной категории
            string categoryToSelect = "flakes";

            // Действие
            string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            // Утверждение
            Assert.AreEqual(categoryToSelect, result);
        }

        [TestMethod]
        public void Generate_Category_Specific_Candy_Count()
        {
            /// Организация (arrange)
            Mock<ICandyRepository> mock = new Mock<ICandyRepository>();
            mock.Setup(m => m.Candies).Returns(new List<Candy>
            {
                new Candy { CandyId = 1, Name = "Candy1", Category="chocolate"},
                new Candy { CandyId = 2, Name = "Candy2", Category="flakes"},
                new Candy { CandyId = 3, Name = "Candy3", Category="chocolate"},
                new Candy { CandyId = 4, Name = "Candy4", Category="flakes"},
                new Candy { CandyId = 5, Name = "Candy5", Category="gum"}
            });
            CandyController controller = new CandyController(mock.Object);
            controller.pageSize = 3;

            // Действие - тестирование счетчиков товаров для различных категорий
            int res1 = ((CandiesListViewModel)controller.List("chocolate").Model).PagingInfo.TotalItems;
            int res2 = ((CandiesListViewModel)controller.List("flakes").Model).PagingInfo.TotalItems;
            int res3 = ((CandiesListViewModel)controller.List("gum").Model).PagingInfo.TotalItems;
            int resAll = ((CandiesListViewModel)controller.List(null).Model).PagingInfo.TotalItems;

            // Утверждение
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }
    }
}
