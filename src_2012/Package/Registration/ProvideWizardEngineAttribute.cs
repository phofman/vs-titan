using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace CodeTitans.TitanControlPanel.Registration
{
    /// <summary>
    /// Registration attribute that creates necessary entries to expose specified type as a custom Visual Studio wizard engine.
    /// 
    /// Note: to debug the wizard (which is outside the main package assembly) simply wrap the 'AssemblyPath'
    ///       with `#if DEBUG\n , AssemblyName = @"T:\(path)\(name).dll"\n #endif`
    ///       directive and hardcode the local path to the wizard's assembly.
    /// </summary>
    public sealed class ProvideWizardEngineAttribute : RegistrationAttribute
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ProvideWizardEngineAttribute()
        {
        }

        /// <summary>
        /// Init constructor.
        /// </summary>
        public ProvideWizardEngineAttribute(Type engineType)
        {
            if (engineType == null)
                throw new ArgumentNullException("engineType");
            if (engineType.GUID == Guid.Empty)
                throw new ArgumentOutOfRangeException("engineType");

            ClassGUID = engineType.GUID;
            ClassName = engineType.FullName;
            AssemblyName = engineType.Assembly.Location;

            var progIdAttribute = (ProgIdAttribute)GetCustomAttribute(engineType, typeof(ProgIdAttribute));
            ProgId = progIdAttribute != null ? progIdAttribute.Value : null;
        }

        #region Properties

        public object ClassGUID
        {
            get;
            set;
        }

        public string ClassName
        {
            get;
            set;
        }

        public string ProgId
        {
            get;
            set;
        }

        public string AssemblyName
        {
            get;
            set;
        }

        #endregion

        private static Guid GetGuid(object o)
        {
            var guidString = o as string;
            if (guidString != null)
            {
                return new Guid(guidString);
            }

            if (o is Guid)
            {
                return (Guid) o;
            }

            var type = o as Type;
            if (type != null)
            {
                return type.GUID;
            }

            throw new ArgumentException("Unexpected ClassGuid format", "ClassGuid");
        }

        private string GetWizardClassGuidString()
        {
            return GetGuid(ClassGUID).ToString("B").ToUpperInvariant();
        }

        public override void Register(RegistrationContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            // class registration:
            var wizardClassGuid = GetWizardClassGuidString();
            using (var key = context.CreateKey(@"CLSID\" + wizardClassGuid))
            {
                key.SetValue(null, ClassName);
                key.SetValue("Class", ClassName);

                // check if full-path specified or relative to the package location:
                if ((AssemblyName.Length > 2 && AssemblyName[1] == ':') || AssemblyName.StartsWith("file://", StringComparison.InvariantCultureIgnoreCase))
                {
                    key.SetValue("CodeBase", AssemblyName);
                }
                else
                {
                    key.SetValue("CodeBase", @"$PackageFolder$\" + AssemblyName);
                }

                key.SetValue("InprocServer32", @"$WinDir$\System32\mscoree.dll");
                key.SetValue("ThreadingModel", "Both");

                if (!string.IsNullOrEmpty(ProgId))
                {
                    using (var p = key.CreateSubkey("ProgId"))
                    {
                        p.SetValue(null, ProgId);
                    }
                }
            }

            // ProgId registration:
            if (!string.IsNullOrEmpty(ProgId))
            {
                using (var key = context.CreateKey(@"ProgId\" + ProgId))
                {
                    key.SetValue(null, ClassName);
                    using (var clsid = key.CreateSubkey("CLSID"))
                    {
                        clsid.SetValue(null, wizardClassGuid);
                    }
                }
            }
        }

        public override void Unregister(RegistrationContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            // remove class registration:
            context.RemoveKey(@"CLSID\" + GetWizardClassGuidString());

            // remove ProgId registration:
            if (!string.IsNullOrEmpty(ProgId))
            {
                context.RemoveKey(@"ProgId\" + ProgId);
            }
        }
    }
}
