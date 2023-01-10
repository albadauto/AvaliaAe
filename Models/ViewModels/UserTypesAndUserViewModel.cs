using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace AvaliaAe.Models
{
	public class UserTypesAndUserViewModel
	{
		[ValidateNever]
		public List<UserTypeModel> UserTypes { get; set; }	
		public UserModel UserModel { get; set; }
	}
}
