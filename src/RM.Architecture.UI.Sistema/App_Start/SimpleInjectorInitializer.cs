using RM.Architecture.Filiacao.Infra.CrossCutting.BootStrapper;
using SimpleInjector.Advanced;

[assembly: WebActivator.PostApplicationStartMethod(typeof(RM.Architecture.UI.Sistema.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace RM.Architecture.UI.Sistema.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;
    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    using RM.Architecture.Identity.Infra.CrossCuting.IoC;
    using System.Web;
    using Microsoft.Owin;

    public static class SimpleInjectorInitializer
    {
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            InitializeContainer(container);

            // Necess�rio para registrar o ambiente do Owin que � depend�ncia do Identity
            // Feito fora da camada de IoC para n�o levar o System.Web para fora

            container.Register(() =>
            {
                if (HttpContext.Current != null && HttpContext.Current.Items["owin.Environment"] == null && container.IsVerifying())
                {
                    return new OwinContext().Authentication;
                }
                return HttpContext.Current.GetOwinContext().Authentication;
            }, Lifestyle.Scoped);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void InitializeContainer(Container container)
        {
            SimpleInjectorMapping.Register(container);
            BootStrapper.RegisterServices(container);
        }
    }
}