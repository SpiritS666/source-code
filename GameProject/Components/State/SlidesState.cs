using System;
using System.Linq;
using System.Threading.Tasks;
using GameProject.components;
using GameProject.Controllers.Game;
using GameProject.Extensions.RecordPlayer;
using GameProject.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace GameProject.States;

public class SlidesState : State
{
    private readonly Slide[] Slides;
    private MouseState _currentMouse;
    private MouseState _previousMouse;

    private int CurrentSlideIndex;

    private readonly SlideComponent CurrentSlide;
    private readonly Dialog GameDialog; 
    private readonly Status GameStatus;
    private readonly Statistic GameStatistic;

    private int DeletedCount = 0;
    private readonly Texture2D ButtonTexture;
    private readonly SpriteFont ButtonFont;
    private readonly int ButtonPositionX; 

    public SlidesState(
        GameEngine game, GraphicsDevice graphicsDevice, 
        ContentManager content, Slide[] slides, User user) 
        : base(game, graphicsDevice, content)
    {
        Slides = slides;
        CurrentSlideIndex = user.SlideIndex;

        var SlideHeight = 1080 * (graphicsDevice.Viewport.Width / 1920);

        ButtonTexture = _content.Load<Texture2D>("Components/Button");
        ButtonFont = _content.Load<SpriteFont>("Fonts/Button");
        ButtonPositionX = (_graphicsDevice.Viewport.Width - ButtonTexture.Width) / 2;

        GameDialog = new(_content); 
        GameStatus = new(_content, graphicsDevice.Viewport.Width);
        GameStatistic = new(_content, graphicsDevice.Viewport.Width);
        GameStatistic.SetStatistic(user.GoodStat, user.EvilStat);
        CurrentSlide = new(null, graphicsDevice.Viewport.Width, 
                           SlideHeight, graphicsDevice.Viewport.Height);
        
        _components = [CurrentSlide, GameDialog, GameStatus, GameStatistic];
        LoadPreviousMusic();
        SetSlide(CurrentSlideIndex);
    }

    private async void GetNextSlide(int nextId = 0) 
    {
        CurrentSlideIndex = await _game.GetNextSlide(
            nextId != 0 ? null : Slides.Where(s => s.Id == CurrentSlideIndex).FirstOrDefault(), 
            nextId);
        SetSlide(CurrentSlideIndex);
    }

    private void SetSlide(int index)
    {
        var currentSlide = Slides.Where(s => s.Id == index).FirstOrDefault();
        
        GameDialog.SetData(
            currentSlide.Text is null ? "" : currentSlide.Text, 
            currentSlide.Sayings is null ? "" : currentSlide.Sayings
        );

        var slideTexture = "Slides/" + (currentSlide.Image is null 
            ? "0" 
            : currentSlide.Image);
        CurrentSlide.SetNewTexture(
            _content.Load<Texture2D>(
                slideTexture
            )
        );

        if (currentSlide.Music is not null)
        {
            var music = _content.Load<Song>("Songs/" + currentSlide.Music);
            RecordPlayer.SetMusic(music);
        }

        if (currentSlide.Choice is not null)
            AddChoice(currentSlide.Choice);
    }

    private void AddChoice(Choice[] choices)
    {
        if (choices is null) return;

        for (var i = 0; i < choices.Length; i++)
        {
            var currentChoice = choices[i];
            var choiceButton = new Button(ButtonTexture, ButtonFont)
            {
                Text = currentChoice.Text,
                Position = new(ButtonPositionX, 360 + 134 * i)
            };
            var j = i;

            choiceButton.Click += (o, e) => {
                if (currentChoice.Action is not null)
                {
                    var IsEvil = currentChoice.Action.Type == "Evil";
                    _game.AddStat(
                        IsEvil ? ChoiceType.Evil : ChoiceType.Good,
                        currentChoice.Action.Count,
                        GameStatistic);
                    GameStatus.SetStatus("+ " + currentChoice.Action.Count);
                    if (currentChoice.Action.GameOver)
                    {
                        _game.cache.Clear();
                        _game.SetMenuState();
                    }
                    else
                    {
                        DeleteStatus();
                        DeletedCount = choices.Length;
                        GetNextSlide(currentChoice.Action.GoTo);
                    }
                } 
                else
                {
                    DeletedCount = choices.Length;
                    GetNextSlide();
                }
            };

            _components.Add(choiceButton);
        }
    }

    private async Task DeleteStatus()
    {
        await Task.Delay(2000);
        GameStatus.SetStatus("");
    }

    private void LoadPreviousMusic()
    {
        var slide = Slides.Where(s => s.Id == CurrentSlideIndex).FirstOrDefault();

        var index = Array.IndexOf(Slides, slide);
        var currentSlideWithMusic = Slides
            .Take(index)
            .Reverse()
            .Where(s => s.Music is not null)
            .FirstOrDefault();

        if (currentSlideWithMusic is not null)
        {
            var music = _content.Load<Song>("Songs/" + currentSlideWithMusic.Music);
            MediaPlayer.Play(music);
        }
    }

    public override void PostUpdate(GameTime gameTime) 
    {
        var IsChoise = CurrentSlideIndex < Slides.Length 
            && Slides.Where(s => s.Id == CurrentSlideIndex).FirstOrDefault().Choice is not null;
            
        while (DeletedCount > 0)
        {
            _components.RemoveAt(_components.Count - 1);
            DeletedCount--;
        }

        if (!IsChoise)
        {   
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();
        }
        if (_currentMouse.LeftButton == ButtonState.Released 
            && _previousMouse.LeftButton == ButtonState.Pressed
            && !IsChoise)
        {
            if (Slides.Where(s => s.Id == CurrentSlideIndex).FirstOrDefault().GoTo == 0)
            {
                _game.cache.Clear();
                _game.SetMenuState();
                return;
            }
            GetNextSlide();
        }
    }
}