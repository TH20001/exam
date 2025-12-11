using System.ComponentModel.DataAnnotations;

namespace finalProject.Models;

public class Junk
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
    // lets pretend things should be free
    // because things cost way to much now a days  and a price should not be required
    public decimal Price { get; set; }

    public int Quantity { get; set; }
}
