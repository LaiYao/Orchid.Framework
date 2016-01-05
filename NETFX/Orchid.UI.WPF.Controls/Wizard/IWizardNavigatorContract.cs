using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.UI.WPF.Controls.Wizard
{
    [ContractClassFor(typeof(IWizardNavigator))]
    abstract class IWizardNavigatorContract : IWizardNavigator
    {
        WizardStep IWizardNavigator.GetNextStep(IList<WizardStep> steps, WizardStep step)
        {
            Contract.Requires<ArgumentNullException>(steps != null,
                "Cannot determine next step because 'steps' argument is null.");
            Contract.Requires<ArgumentNullException>(step != null,
                "Cannot determine next step because 'step' argument is null.");
            // ReSharper disable AssignNullToNotNullAttribute
            Contract.Requires<ArgumentException>(Contract.Exists(steps, s => ReferenceEquals(s, step)),
                // ReSharper restore AssignNullToNotNullAttribute
                "Cannot determine next step because the wizard step does not belong to the steps collection.");
            return default(WizardStep);
        }

        WizardStep IWizardNavigator.GetPreviousStep(IList<WizardStep> steps, WizardStep step)
        {
            Contract.Requires<ArgumentNullException>(steps != null,
                "Cannot determine next step because 'steps' argument is null.");
            Contract.Requires<ArgumentNullException>(step != null,
                "Cannot determine next step because 'step' argument is null.");
            // ReSharper disable AssignNullToNotNullAttribute
            Contract.Requires<ArgumentException>(Contract.Exists(steps, s => ReferenceEquals(s, step)),
                // ReSharper restore AssignNullToNotNullAttribute
                "Cannot determine next step because the wizard step does not belong to the steps collection.");
            return default(WizardStep);
        }

        bool IWizardNavigator.IsNextStepAvaliable(IList<WizardStep> steps, WizardStep step)
        {
            Contract.Requires<ArgumentNullException>(steps != null,
                "Cannot determine next step because 'steps' argument is null.");
            Contract.Requires<ArgumentNullException>(step != null,
                "Cannot determine next step because 'step' argument is null.");
            // ReSharper disable AssignNullToNotNullAttribute
            Contract.Requires<ArgumentException>(Contract.Exists(steps, s => ReferenceEquals(s, step)),
                // ReSharper restore AssignNullToNotNullAttribute
                "Cannot determine next step because the wizard step does not belong to the steps collection.");
            return default(bool);
        }

        bool IWizardNavigator.IsPreviousStepAvaliable(IList<WizardStep> steps, WizardStep step)
        {
            Contract.Requires<ArgumentNullException>(steps != null,
                "Cannot determine next step because 'steps' argument is null.");
            Contract.Requires<ArgumentNullException>(step != null,
                "Cannot determine next step because 'step' argument is null.");
            // ReSharper disable AssignNullToNotNullAttribute
            Contract.Requires<ArgumentException>(Contract.Exists(steps, s => ReferenceEquals(s, step)),
                // ReSharper restore AssignNullToNotNullAttribute
                "Cannot determine next step because the wizard step does not belong to the steps collection.");
            return default(bool);
        }

        bool IWizardNavigator.IsNextStepAccessible(IList<WizardStep> steps, WizardStep step)
        {
            Contract.Requires<ArgumentNullException>(steps != null,
                "Cannot determine next step because 'steps' argument is null.");
            Contract.Requires<ArgumentNullException>(step != null,
                "Cannot determine next step because 'step' argument is null.");
            // ReSharper disable AssignNullToNotNullAttribute
            Contract.Requires<ArgumentException>(Contract.Exists(steps, s => ReferenceEquals(s, step)),
                // ReSharper restore AssignNullToNotNullAttribute
                "Cannot determine next step because the wizard step does not belong to the steps collection.");
            return default(bool);
        }
    }
}
