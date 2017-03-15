using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;

namespace Whisperer.DependencyResolution
{
    public class StructureMapHttpControllerActivator : IHttpControllerActivator
    {
        private IContainer _container;

        public StructureMapHttpControllerActivator(IContainer container)
        {
            _container = container;
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            return _container.GetInstance(controllerType) as IHttpController;
        }
    }
}