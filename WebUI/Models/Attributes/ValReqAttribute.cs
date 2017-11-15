//using Resources;
using System.ComponentModel.DataAnnotations;

namespace WebUI.ViewModels.Attributes
{
    public class ValReqAttribute : RequiredAttribute    
    {
        public ValReqAttribute()
            : base()
        {
            ErrorMessageResourceName = "required";
            //ErrorMessageResourceType = typeof(Mui);
        }
    }
}