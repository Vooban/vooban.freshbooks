using System.Configuration;
using Microsoft.Practices.Unity;

namespace Vooban.FreshBooks.DotNet.Api
{
    public class WrapperUnityContainerExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            var freshbooksUsername = ConfigurationManager.AppSettings["FreshbooksUsername"];
            var freshbooksApiToken = ConfigurationManager.AppSettings["FreshbooksApiToken"];
            var freshbooksInstance = new HastyAPI.FreshBooks.FreshBooks(freshbooksUsername, freshbooksApiToken);

            Container.RegisterInstance(freshbooksInstance, new ContainerControlledLifetimeManager());
        }
    }
}