using System.Threading.Tasks;
using GameProject.components;
using GameProject.Models;

namespace GameProject.Controllers.Game;

public static class ControllerUser
{
    public static async void AddStat(
        this GameEngine game, ChoiceType type, int count, 
        Statistic GameStatistic) 
    {
        var user = await game.cache.Get();

        if (user is null) return;

        if (type == ChoiceType.Evil)
            user.EvilStat += count;
        else 
            user.GoodStat += count;

        GameStatistic.SetStatistic(user.GoodStat, user.EvilStat);
        await game.cache.Set(user);
    }

    public static async Task<int> GetNextSlide(this GameEngine game, Slide currentSlide = null, int nextId = 0)
    {
        var user = await game.cache.Get();

        if (user is null) return 0;

        if (currentSlide is not null)
            user.SlideIndex = currentSlide.GoTo;

        if (nextId != 0)
            user.SlideIndex = nextId;

        await game.cache.Set(user);

        return user.SlideIndex;
    }
}