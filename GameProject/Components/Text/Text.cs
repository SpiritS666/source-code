using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GameProject.components;

public class Text : Component
{
    protected string _text;
    protected Color _color;
    protected Vector2 _position;
    protected SpriteFont _font;

    public string Value 
    {
        get => _text;
        set => _text = value;
    }

    public Text(string text, Vector2 position, Color color, SpriteFont font) 
    {
        _text = text;
        _color = color;
        _position = position;
        _font = font;
    }
    
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(_font, _text, _position, _color);
    }

    public override void Update(GameTime gameTime) {}
}