using System.ComponentModel.DataAnnotations;

namespace KafeRest.Models;

    public class About
    {
      [Key]
      public int Id { get; set; }
      public string Text { get; set; }
    }

