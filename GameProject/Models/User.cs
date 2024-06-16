namespace GameProject.Models; 

public class User(int evilStat, int goodStat, int slideIndex)
{
    public int EvilStat { get; set; } = evilStat;
    public int GoodStat { get; set; } = goodStat;
    public int SlideIndex { get; set; } = slideIndex;
}

