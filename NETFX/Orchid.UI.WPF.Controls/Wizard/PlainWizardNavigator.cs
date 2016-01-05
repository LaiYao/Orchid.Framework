using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.UI.WPF.Controls.Wizard
{
    internal class PlainWizardNavigator : IWizardNavigator
    {
        #region Static

        private static WizardStep GetNextStep(IList<WizardStep> steps, int stepIndex)
        {
            if (!IsNextStepAvaliable(steps, stepIndex))
                return null;

            stepIndex++;
            return steps[stepIndex];
        }

        private static WizardStep GetPreviousStep(IList<WizardStep> steps, int stepIndex)
        {
            if (!IsPreviousStepAvaliable(stepIndex))
                return null;

            stepIndex--;
            return steps[stepIndex];
        }

        private static bool IsNextStepAvaliable(ICollection<WizardStep> steps, int stepIndex)
        {
            return (stepIndex < (steps.Count - 1));
        }

        private static bool IsPreviousStepAvaliable(int stepIndex)
        {
            return (stepIndex > 0);
        }

        #endregion

        public WizardStep GetNextStep(IList<WizardStep> steps, WizardStep step)
        {
            var stepIndex = steps.IndexOf(step);
            return GetNextStep(steps, stepIndex);
        }

        public WizardStep GetPreviousStep(IList<WizardStep> steps, WizardStep step)
        {
            var stepIndex = steps.IndexOf(step);
            return GetPreviousStep(steps, stepIndex);
        }

        public bool IsNextStepAvaliable(IList<WizardStep> steps, WizardStep step)
        {
            var stepIndex = steps.IndexOf(step);
            return IsNextStepAvaliable(steps, stepIndex);
        }

        public bool IsPreviousStepAvaliable(IList<WizardStep> steps, WizardStep step)
        {
            var stepIndex = steps.IndexOf(step);
            return IsPreviousStepAvaliable(stepIndex);
        }

        public bool IsNextStepAccessible(IList<WizardStep> steps, WizardStep step)
        {
            return IsNextStepAvaliable(steps, step);
        }
    }
}
