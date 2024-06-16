using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GameProject.components;

public class SlideComponent : Component
{
    private Texture2D _texture;

    private Rectangle ImageRectangle;

    public SlideComponent(Texture2D texture, int width, int height, int ViewportHeight)
    {
        _texture = texture;

        var marginTop = (ViewportHeight - height) / 2;
        ImageRectangle = new(0, marginTop, width, height);
    }

    public void SetNewTexture(Texture2D texture)
    {
        _texture = texture;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, ImageRectangle, Color.White);
    }

    public override void Update(GameTime gameTime) {}
}