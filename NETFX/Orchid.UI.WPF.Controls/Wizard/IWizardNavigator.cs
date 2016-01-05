using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.UI.WPF.Controls.Wizard
{
    public interface IWizardNavigator
    {
        WizardStep GetNextStep(IList<WizardStep> steps, WizardStep step);
        WizardStep GetPreviousStep(IList<WizardStep> steps, WizardStep step);
        bool IsNextStepAvaliable(IList<WizardStep> steps, WizardStep step);
        bool IsPreviousStepAvaliable(IList<WizardStep> steps, WizardStep step);
        bool IsNextStepAccessible(IList<WizardStep> steps, WizardStep step);
    }
}
