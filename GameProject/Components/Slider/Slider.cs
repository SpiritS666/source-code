using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace GameProject.components;

public class Slider : Component
{
    private MouseState _currentMouse;
    private MouseState _previousMouse;

    public Action<int> ChangeHandler;

    private int Value;

    private readonly Texture2D SliderBottomTexture;
    private readonly Texture2D SliderTopTexture;
    private readonly SpriteFont CaptionFont;
    private readonly Rectangle SliderBottomRectangle;
    private Rectangle SliderTopRectangle;

    public Slider(ContentManager content, Vector2 position, int defaultVolume)
    {
        Value = defaultVolume;
        SliderBottomTexture = content.Load<Texture2D>("Components/SliderBottom");
        SliderTopTexture = content.Load<Texture2D>("Components/SliderTop");
        CaptionFont = content.Load<SpriteFont>("Fonts/Sayings");

        SliderBottomRectangle = new((int)position.X, (int)position.Y, 370, 64);
        SliderTopRectangle = new(0, 0, (int)((double)defaultVolume / 100 * 370), 64);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        var SliderTopDestination = new Rectangle(
            SliderBottomRectangle.X, SliderBottomRectangle.Y,
            SliderTopRectangle.Width, 64);

        spriteBatch.Draw(SliderBottomTexture, SliderBottomRectangle, Color.White);
        spriteBatch.Draw(
            SliderTopTexture, SliderTopDestination,
            SliderTopRectangle, Color.White, 0, Vector2.Zero,
            SpriteEffects.None, 1f);

        spriteBatch.DrawString(
            CaptionFont, $"{Value}%",
            new(SliderBottomRectangle.X + 16, SliderBottomRectangle.Y + 16), Color.Black);
    }

    public override void Update(GameTime gameTime)
    {
        _previousMouse = _currentMouse;
        _currentMouse = Mouse.GetState();
        var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

        if (mouseRectangle.Intersects(SliderBottomRectangle) 
            && _currentMouse.LeftButton == ButtonState.Pressed
            && (_previousMouse.LeftButton == ButtonState.Pressed
                || _previousMouse.LeftButton == ButtonState.Released))
        {
            var cursor = _currentMouse.X - SliderBottomRectangle.X;
            
            if (cursor < 0) cursor = 0;
            if (cursor > SliderBottomRectangle.Width) 
                cursor = SliderBottomRectangle.Width;

            
            var percent = ((double)cursor / SliderBottomRectangle.Width) * 100;
            Value = (int)Math.Round(percent);
            SliderTopRectangle.Width = (int)((double)Value / 100 * SliderBottomRectangle.Width);
            ChangeHandler.Invoke(Value);
        }
    }
}