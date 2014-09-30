using System.Runtime.InteropServices;
using System.Windows.Forms;
using EnvDTE;

namespace CodeTitans.TitanControlPanel.Wizards
{
    [ComVisible(true)]
    [Guid("20564857-f4fe-4e1f-9cf5-ef644029b91b")]
    [ProgId("CodeTitans.MultiFileWizardEngine")]
    public sealed class MultiFileWizardEngine : IDTWizard
    {
        public void Execute(object application, int hwndOwner, ref object[] contextParams, ref object[] customParams, ref wizardResult returnValue)
        {
            returnValue = wizardResult.wizardResultSuccess;

            MessageBox.Show("I am a custom wizard engine!");
        }
    }
}
