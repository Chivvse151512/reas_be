using System.ComponentModel.DataAnnotations;

namespace BusinessObject.ValidateAttributes
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public class RequiredUsernameAttribute : ValidationAttribute
	{
        public string Value { get; set; }
        public override bool IsValid(object? value)
		{
			string userName = (value as string) ?? string.Empty;
			if (userName != Value)
				return false;
			return true;
		}
	}
}
