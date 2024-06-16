using System;
using System.Threading.Tasks;
using GameProject.Extensions;
using GameProject.Models;
using GameProject.States;

namespace GameProject.Controllers.Game;

public static class ControllerGame
{
    public static async void LoadGame(this GameEngine game, object sender, EventArgs e) 
    {
        var user = await game.cache.Get();
        if (user is null)
        {
            user = await game.cache.Set(new(0, 0, 1));
        }

        await game.SetSlideState(user); 
    }

    public static async void NewGame(this GameEngine game, object sender, EventArgs e) 
    {
        var user = await game.cache.Set(new(0, 0, 1));
        await game.SetSlideState(user);
    }

    public static async void SetGameAbout(this GameEngine game, object sender, EventArgs e)
    {
        await game.SetAboutState();
    }

    public static void QuitGameButton(this GameEngine game,
        object sender, EventArgs e) => game.Exit();

    private static async Task SetSlideState(this GameEngine game, User user)
    {
        var slides = await SerializeFile.Serialize();
        
        game.ChangeState(
            new SlidesState(
                game, game.GraphicsDevice, 
                game.Content, slides, user)
        );
    }

    public static async Task SetAboutState(this GameEngine game)
        => game.ChangeState(new AboutState(game, game.GraphicsDevice, game.Content));

    public static async Task SetMenuState(this GameEngine game)
        => game.ChangeState(new MenuState(game, game.GraphicsDevice, game.Content));
}