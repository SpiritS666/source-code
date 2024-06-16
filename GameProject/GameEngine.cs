using GameProject.components;
using GameProject.Extensions.Cache;
using GameProject.Extensions.RecordPlayer;
using GameProject.Models;
using GameProject.States;
using GameProject.styles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject;

public class GameEngine : Game
{    
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private View _view;
    
    public Cache<User> cache = new();

    public void ChangeState(State state) => _view.ChangeState(state);

    public GameEngine()
    {
        _graphics = new GraphicsDeviceManager(this);
        RecordPlayer.SetMusicVolume(50);
    }

    protected override void Initialize() {
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;
        _graphics.ToggleFullScreen();
        _graphics.ApplyChanges();
        
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        base.Initialize(); 
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _view = new View(new MenuState(this, _graphics.GraphicsDevice, Content));
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _view.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Colors.BackgroundColor);

        _view.Draw(gameTime, _spriteBatch);
        base.Draw(gameTime);
    }
}
