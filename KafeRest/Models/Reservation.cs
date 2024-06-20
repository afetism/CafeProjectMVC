using System.ComponentModel.DataAnnotations;

namespace KafeRest.Models;

	public class Reservation
	{
	   [Key]
	   public int Id { get; set; }
       [Required]
	   public string Name { get; set; }
	   [Required]
	   public string Email { get; set; }
	   [Required]
	   public string PhoneNumber { get; set; }
	   [Required]
	   public int Count {  get; set; }
       public string Clock { get; set; }
       public DateTime History { get; set; }

     }

