

using System.ComponentModel.DataAnnotations;

namespace KafeRest.Models;

	public class Connection
	{
	 [Key]
	 public int Id { get; set; }
    public string Email { get; set; }
	public string Phone { get; set; }
	public string Address { get; set; }
}

