using System.Diagnostics;
using System.Runtime.InteropServices;
using CodeTitans.TitanControlPanel.Registration;
using CodeTitans.TitanControlPanel.Wizards;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace CodeTitans.TitanControlPanel
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    ///
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// </summary>
    // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
    // a package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    // This attribute is used to register the information needed to show this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [Guid(GuidList.guidTitanControlPanelPkgString)]

    // This attribute adds top level node at [Add Project Item] dialog for Visual C++ projects only and showing
    // new item wizards from '<Package>/ProjectItems' folder. This is an easy way to inject new templates
    // without any need of copying them into Visual Studio folder itself.
#if PLATFORM_VS2013
    [ProvideProjectItem("{8BC9CEB8-8B4A-11D0-8D11-00A0C91BC943}", "CodeTitans", "ProjectItems", 10)]
#else
    [ProvideProjectItem("{8BC9CEB8-8B4A-11D0-8D11-00A0C91BC942}", "CodeTitans", "ProjectItems", 10)]
#endif

    // This attribute registers a custom wizard engine, that is used to populate new items into a project.
    // Reference to this engine is made directly from custom-dynamic-wizard.vsz file
    // (populated later, at runtime, under CodeTitans project items).
    [ProvideWizardEngine(typeof(MultiFileWizardEngine))]

    [ProvideAutoLoad(UIContextGuids80.NoSolution)]
    public sealed class TitanControlPanelPackage : Package
    {
        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public TitanControlPanelPackage()
        {
            Debug.WriteLine("Entering constructor for: {0}", this);
        }

        /////////////////////////////////////////////////////////////////////////////
        // Overridden Package Implementation
        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Debug.WriteLine ("Entering Initialize() of: {0}");
            base.Initialize();
        }

        #endregion
    }
}
