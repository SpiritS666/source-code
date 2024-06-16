using GameProject.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GameProject.components;

public class View : Component
{
    private State CurrentState;
    private State? NextState;

    public void ChangeState(State state) => NextState = state;

    public View(State initialState) {
        CurrentState = initialState;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        => CurrentState.Draw(gameTime, spriteBatch);
    
    public override void Update(GameTime gameTime)
    {
        if(NextState is not null)
        {
            CurrentState = NextState;
            NextState = null;
        }
        CurrentState.Update(gameTime);
    }
}