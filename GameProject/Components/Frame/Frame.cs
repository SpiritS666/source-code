using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace GameProject.components;

public class Frame : Component
{
    private Texture2D _texture;
    private readonly Rectangle ImageRectangle;
    public Frame(Texture2D texture, int width, int height, int ViewportHeight)
    {
        _texture = texture;

        var marginTop = (ViewportHeight - height) / 2;
        ImageRectangle = new(0, marginTop, width, height);
    }

    public void SetTexture(Texture2D texture)
    {
        _texture = texture;
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, ImageRectangle, Color.White);
    }

    public override void Update(GameTime gameTime) { }
}