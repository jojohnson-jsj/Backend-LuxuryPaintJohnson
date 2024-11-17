namespace LuxuryPaintJohnsonAPI.Models;

public class Photo
{
	public int Id { get; set; }
	public string? Title {  get; set; }
	public string? Url { get; set; }
	public string? Description { get; set; }
	public DateTime CreatedAt {  get; set; }

	public int ProjectId { get; set; }
	public virtual Project Project { get; set; } = null!;
}
