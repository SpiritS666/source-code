using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.components;

public class Status : Component
{
    private Rectangle StatusRectangle;
    private readonly Texture2D StatusTexture;
    private readonly SpriteFont StatusFont;

    private readonly Text StatusText;

    public Status(ContentManager content, int width)
    {
        StatusTexture = content.Load<Texture2D>("Components/Status");
        StatusFont = content.Load<SpriteFont>("Fonts/Status");
        var PositionX = (width - StatusTexture.Width) / 2;
        
        StatusRectangle = new(
            PositionX, 160, StatusTexture.Width, 
            StatusTexture.Height);

        StatusText = new(
            "", new(StatusRectangle.X + 28, StatusRectangle.Y + 12), 
            Color.White, StatusFont);
    }

    public void SetStatus(string status)
    {
        StatusText.Value = status;
    }
     
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (StatusText.Value == "") 
            return;

        spriteBatch.Draw(StatusTexture, StatusRectangle, Color.White);
        StatusText.Draw(gameTime, spriteBatch);
    }

    public override void Update(GameTime gameTime)
    {
        if (StatusText.Value is null) 
            return;
            
        StatusText.Update(gameTime);
    }
}