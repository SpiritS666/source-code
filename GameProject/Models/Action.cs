namespace GameProject.Models;

public record SlideAction(string Type, int Count, int GoTo, bool GameOver = false);