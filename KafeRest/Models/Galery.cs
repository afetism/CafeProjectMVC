using System.ComponentModel.DataAnnotations;

namespace KafeRest.Models;

	public class Galery
	{
	   [Key]
	   public int Id { get; set; }
	   public string? Image { get; set; }

	}

