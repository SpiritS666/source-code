using GameProject.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.components;

public class Dialog : Component
{
    private readonly Texture2D DialogTexture;
    private readonly SpriteFont DialogFont;

    private readonly Texture2D SayingsTexture;
    private readonly SpriteFont SayingsFont;

    private Rectangle DialogRectangle = new(0, 840, 1920, 240);
    private Rectangle SayingsRectangle = new(350, 800, 442, 55);

    private readonly Text DialogText;
    private readonly Text SayingsText;

    private readonly int DialogTextWidth = 1220;

    public Dialog(ContentManager content)
    {
        DialogTexture = content.Load<Texture2D>("Components/Dialog");
        DialogFont = content.Load<SpriteFont>("Fonts/Dialog");

        SayingsTexture = content.Load<Texture2D>("Components/Sayings");
        SayingsFont = content.Load<SpriteFont>("Fonts/Sayings");

        DialogText = new(
            "", new(DialogRectangle.X + 350, DialogRectangle.Y + 32), 
            Color.White, DialogFont);

        SayingsText = new(
            "", new(SayingsRectangle.X + 10, SayingsRectangle.Y + 10), 
            Color.White, SayingsFont);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {   
        if (DialogText.Value != "")
        {
            spriteBatch.Draw(DialogTexture, DialogRectangle, Color.White);
            DialogText.Draw(gameTime, spriteBatch);
        } 
        if (SayingsText.Value != "")
        {
            spriteBatch.Draw(SayingsTexture, SayingsRectangle, Color.White);
            SayingsText.Draw(gameTime, spriteBatch);
        }
    }

    public void SetData(string dialogText, string sayingName)
    {
        DialogText.Value = TextExtensions.Wrap(
            DialogFont, dialogText, DialogTextWidth);
        SayingsText.Value = sayingName;
    }

    public override void Update(GameTime gameTime)
    {
        DialogText.Update(gameTime);
        SayingsText.Update(gameTime);
    }
}
