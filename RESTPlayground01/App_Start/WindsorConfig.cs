using System.Web.Http;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using RESTPlayground01.Core.Repositories;
using RESTPlayground01.Core.Services;

namespace RESTPlayground01
{
    public class WindsorConfig
    {
        public static IWindsorContainer BuildUpContainer()
        {
            var container = new WindsorContainer();
            container
                .Register(Component.For<IDiffRequestsRepository>()
                    .ImplementedBy<InMemoryDiffRequestsRepository>().LifestyleSingleton())
                .Register(Component.For<IBinaryDataDiffAnalyzer>()
                    .ImplementedBy<BinaryDataDiffAnalyzer>().LifestyleSingleton());

            RegisterControllers(container);
            return container;
        }

        private static void RegisterControllers(IWindsorContainer container)
        {
            container.Register(Types.FromThisAssembly()
                .BasedOn<ApiController>()
                .If(t => t.Name.EndsWith("Controller"))
                .WithService.Self().LifestyleTransient());
        }
    }
}