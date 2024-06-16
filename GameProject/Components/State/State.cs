using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace GameProject.States;

public abstract class State
{
    protected List<Component> _components;
    protected ContentManager _content;
    protected GraphicsDevice _graphicsDevice;
    protected GameEngine _game;

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch) 
    {
        spriteBatch.Begin();

        foreach (var component in _components)
            component.Draw(gameTime, spriteBatch);

        spriteBatch.End();
    }
    public abstract void PostUpdate(GameTime gameTime);

    public State(GameEngine game, GraphicsDevice graphicsDevice, ContentManager content)
    {
        _game = game;
        _graphicsDevice = graphicsDevice;
        _content = content;
        _components = [];
    }

    public void Update(GameTime gameTime) 
    {
        foreach (var component in _components)
            component.Update(gameTime);

        PostUpdate(gameTime);
    }
}
