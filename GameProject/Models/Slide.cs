namespace GameProject.Models;

public record Slide(
    int Id, int GoTo, string Image, string Sayings, 
    string Text, Choice[] Choice, string Music);