using System;
using System.ComponentModel.DataAnnotations;


namespace WebUI.ViewModels.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class LoginUniqueAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "login already exists";

        public LoginUniqueAttribute()
            : base(DefaultErrorMessage)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            return DefaultErrorMessage;
        }

        //public override bool IsValid(object value)
        //{
        //    if (string.IsNullOrEmpty((string)value)) return true;
        //    return IoC.Resolve<IUserService>().IsUnique((string)value);
        //}
    }
}