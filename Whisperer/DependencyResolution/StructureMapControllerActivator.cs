using System;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;

namespace Whisperer.DependencyResolution
{
    public class StructureMapControllerActivator : IControllerActivator
    {
        private IContainer _container;

        public StructureMapControllerActivator(IContainer container)
        {
            _container = container;
        }

        public IController Create(RequestContext requestContext, Type controllerType)
        {
            return _container.GetInstance(controllerType) as IController;
        }
    }
}