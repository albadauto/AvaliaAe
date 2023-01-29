namespace AvaliaAe.Models
{
	public class DenouncesModel
	{
		public int Id { get; set; }
		public string Comment { get; set; }
		public UserModel User { get; set; }
		public InstitutionModel Institution { get; set; }
		public int UserId { get; set; }
		public int InstitutionId { get; set; }
	}
}
