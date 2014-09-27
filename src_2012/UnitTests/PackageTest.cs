using CodeTitans.TitanControlPanel;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VsSDK.UnitTestLibrary;

namespace CodeTitans.UnitTests
{
    [TestClass]
    public class PackageTest
    {
        [TestMethod]
        public void CreateInstance()
        {
            TitanControlPanelPackage package = new TitanControlPanelPackage();
        }

        [TestMethod]
        public void IsIVsPackage()
        {
            TitanControlPanelPackage package = new TitanControlPanelPackage();
            Assert.IsNotNull(package as IVsPackage, "The object does not implement IVsPackage");
        }

        [TestMethod]
        public void SetSite()
        {
            // Create the package
            IVsPackage package = new TitanControlPanelPackage() as IVsPackage;
            Assert.IsNotNull(package, "The object does not implement IVsPackage");

            // Create a basic service provider
            OleServiceProvider serviceProvider = OleServiceProvider.CreateOleServiceProviderWithBasicServices();

            // Site the package
            Assert.AreEqual(0, package.SetSite(serviceProvider), "SetSite did not return S_OK");

            // Unsite the package
            Assert.AreEqual(0, package.SetSite(null), "SetSite(null) did not return S_OK");
        }
    }
}
