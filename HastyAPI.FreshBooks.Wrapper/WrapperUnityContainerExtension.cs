using System.Configuration;
using Microsoft.Practices.Unity;

namespace HastyAPI.FreshBooks.Wrapper
{
    public class WrapperUnityContainerExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            var freshbooksUsername = ConfigurationManager.AppSettings["FreshbooksUsername"];
            var freshbooksApiToken = ConfigurationManager.AppSettings["FreshbooksApiToken"];
            var freshbooksInstance = new FreshBooks(freshbooksUsername, freshbooksApiToken);

            Container.RegisterInstance(freshbooksInstance, new ContainerControlledLifetimeManager());
        }
    }
}