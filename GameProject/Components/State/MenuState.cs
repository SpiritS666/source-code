using GameProject.components;
using GameProject.Controllers.Game;
using GameProject.Extensions.RecordPlayer;
using GameProject.styles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;


namespace GameProject.States;

public class MenuState : State
{
    public MenuState(GameEngine game, GraphicsDevice graphicsDevice, ContentManager content) 
        : base(game, graphicsDevice, content)
    {
        var buttonTexture = _content.Load<Texture2D>("Components/MenuButton");
        var buttonFont = _content.Load<SpriteFont>("Fonts/Button");
        var bigFont = _content.Load<SpriteFont>("Fonts/Statistic");
        var TitleFont = _content.Load<SpriteFont>("Fonts/Title");
        var music = _content.Load<Song>("Songs/menu");
        var rightMargin = 60;

        var centerPositionHeight = graphicsDevice.Viewport.Height / 2 - 330;
        var menuBackground = content.Load<Texture2D>("Components/Menu");
        var menuFrame = new Frame(menuBackground, 1920, 1080, graphicsDevice.Viewport.Height);
        var title = new Text(
            "Legenda Herois", 
            new(rightMargin, centerPositionHeight), 
            Colors.TextColor, TitleFont);

        var topPadding = 160;
        var newGameButton = new Button(buttonTexture, buttonFont)
        {
            Position = new(rightMargin, centerPositionHeight + topPadding),
            Text = "НАЧАТЬ НОВУЮ ИГРУ",
        };
        var loadGameButton = new Button(buttonTexture, buttonFont)
        {
            Position = new(rightMargin, centerPositionHeight + topPadding + 143),
            Text = "ПРОДОЛЖИТЬ ИГРУ",
        };
        var aboutGameButton = new Button(buttonTexture, buttonFont)
        {
            Position = new(rightMargin, centerPositionHeight + topPadding + 143 * 2),
            Text = "ОБ ИГРЕ",
        };
        var quitGameButton = new Button(buttonTexture, buttonFont)
        {
            Position = new(rightMargin, centerPositionHeight + topPadding + 143 * 3),
            Text = "ВЫЙТИ",
        };

        var musicSliderText = new Text("Громкость", 
            new(graphicsDevice.Viewport.Width - rightMargin - 370,
                graphicsDevice.Viewport.Height - 120 - 96),
            Colors.TextColor, bigFont);

        var musicVolumeSlider = new Slider(content,
            new(graphicsDevice.Viewport.Width - rightMargin - 370,
                graphicsDevice.Viewport.Height - 120), (int)(MediaPlayer.Volume*100));
        RecordPlayer.SetMusic(music);

        newGameButton.Click += _game.NewGame;
        loadGameButton.Click += _game.LoadGame;
        aboutGameButton.Click += _game.SetGameAbout;
        quitGameButton.Click += _game.QuitGameButton;
        musicVolumeSlider.ChangeHandler = (vol) => RecordPlayer.SetMusicVolume(vol);

        _components.Add(menuFrame);
        _components.Add(title);
        _components.Add(newGameButton);

        var GetTask = game.cache.Get();
        var user = GetTask.GetAwaiter().GetResult();
        
        if (user is not null)
            _components.Add(loadGameButton);
        else
        {
            quitGameButton.Position = new(
                rightMargin, quitGameButton.Position.Y - 143);
            aboutGameButton.Position = new(
                rightMargin, aboutGameButton.Position.Y - 143);
        } 
            
        _components.Add(aboutGameButton);
        _components.Add(quitGameButton);
        _components.Add(musicSliderText);
        _components.Add(musicVolumeSlider);
    }

    public override void PostUpdate(GameTime gameTime) {}
}  
