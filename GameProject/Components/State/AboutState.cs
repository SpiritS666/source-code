using GameProject.components;
using GameProject.Controllers.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameProject.States;

public class AboutState : State
{
    private readonly List<Texture2D> Backgrounds = new();
    private int CurrentBackground;
    private MouseState _currentMouse;
    private MouseState _previousMouse;

    private Frame AboutFrame;

    public AboutState(GameEngine game, GraphicsDevice graphicsDevice, ContentManager content)
        : base(game, graphicsDevice, content)
    {
        for (var i = 0; i < 4; i++)
            Backgrounds.Add(content.Load<Texture2D>("Abouts/" + i.ToString()));

        AboutFrame = new Frame(Backgrounds[0], 1920, 1080, graphicsDevice.Viewport.Height);
        _components = [AboutFrame];
    }

    public override void PostUpdate(GameTime gameTime) 
    {
        _previousMouse = _currentMouse;
        _currentMouse = Mouse.GetState();
        if (_currentMouse.LeftButton == ButtonState.Released
            && _previousMouse.LeftButton == ButtonState.Pressed)
        {
            if (CurrentBackground == Backgrounds.Count - 1)
                _game.SetMenuState();
            else
            {
                CurrentBackground++;
                AboutFrame.SetTexture(Backgrounds[CurrentBackground]);
            }    
        }
    }
}