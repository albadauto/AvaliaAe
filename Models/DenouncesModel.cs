namespace AvaliaAe.Models
{
	public class DenouncesModel
	{
		public int Id { get; set; }
		public string Comment { get; set; }
		public AvaliationModel Avaliation { get; set; }	
		public int AvaliationId { get; set; }
	}
}
