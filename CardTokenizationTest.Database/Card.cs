using System.ComponentModel.DataAnnotations;

namespace CardTokenizationTest.Database;
public class Card
{
    [Key]
    public int CardId { get; set; }
    public string CardToken { get; set; }
    public string UserId { get; set; }
}

