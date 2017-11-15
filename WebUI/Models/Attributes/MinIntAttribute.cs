using System.ComponentModel.DataAnnotations;

namespace WebUI.ViewModels.Attributes
{
    public class MinIntAttribute : ValidationAttribute
    {
        private readonly int minVal;

        public MinIntAttribute(int minVal)
        {
            this.minVal = minVal;
        }
        
        public override string FormatErrorMessage(string name)
        {
            return "Field " + name + " Required";
        }

        public override bool IsValid(object value)
        {
            var intVal = value as int?;
            if (intVal == null || intVal <= minVal)
                return false;
            return true;
        }
    }
}