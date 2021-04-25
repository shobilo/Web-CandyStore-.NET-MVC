using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Moq;
using Ninject;
using Domain.Entities;
using Domain.Abstract;

namespace UI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            // Здесь размещаются привязки
            Mock<ICandyRepository> mock = new Mock<ICandyRepository>();

            mock.Setup(m => m.Candies).Returns(new List<Candy>
            {
                new Candy { Name = "Chocolate bar", Price = 3, Weight = 150 },
                new Candy { Name = "Marshmallow", Price=5, Weight = 100 },
                new Candy { Name = "Barny", Price=1.5M, Weight = 50 }
            });
            kernel.Bind<ICandyRepository>().ToConstant(mock.Object);
        }
    }
}