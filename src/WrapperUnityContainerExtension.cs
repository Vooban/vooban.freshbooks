using System.Configuration;
using Microsoft.Practices.Unity;
using Vooban.FreshBooks.DotNet.Api.Staff;
using Vooban.FreshBooks.DotNet.Api.TimeEntry;
using Vooban.FreshBooks.DotNet.Api.TimeEntry.Models;

namespace Vooban.FreshBooks.DotNet.Api
{
    /// <summary>
    /// Unity configuration that can be used if you make use of Unity dependency injection framework
    /// </summary>
    public class WrapperUnityContainerExtension : UnityContainerExtension
    {
        /// <summary>
        /// Configure the <c>Unity</c> container with this library types
        /// </summary>
        protected override void Initialize()
        {
            var freshbooksUsername = ConfigurationManager.AppSettings["FreshbooksUsername"];
            var freshbooksApiToken = ConfigurationManager.AppSettings["FreshbooksApiToken"];

            var freshbooksInstance = new HastyAPI.FreshBooks.FreshBooks(freshbooksUsername, freshbooksApiToken);
            
            Container.RegisterInstance(freshbooksInstance, new ContainerControlledLifetimeManager());

            Container.RegisterType<StaffApi>();
            Container.RegisterType<TimeEntryApi>();
        }
    }
}