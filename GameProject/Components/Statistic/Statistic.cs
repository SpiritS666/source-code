using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.components;

public class Statistic : Component
{
    private readonly Texture2D StatisticTexture;
    private readonly Rectangle StatisticRectangle;

    private readonly SpriteFont Font;

    private int[] StatisticData = [0, 0];

    private readonly Text GoodText;
    private readonly Text StatText;
    private readonly Text EvilText;
    
    private readonly Color NegativeColor = new(158, 1, 1); 
    private readonly Color PositiveColor = new(53, 77, 205); 

    public Statistic(ContentManager content, int width)
    {
        StatisticTexture = content.Load<Texture2D>("Components/Statistic");
        Font = content.Load<SpriteFont>("Fonts/Statistic");

        var PositionX = (width - StatisticTexture.Width) / 2;

        StatisticRectangle = new(PositionX, 0, 
            StatisticTexture.Width, StatisticTexture.Height);

        EvilText = new(
            StatisticData[1].ToString(), 
            new(PositionX + 100, 32), NegativeColor, 
            Font);

        StatText = new(
            Config.Config.GoodName, new(PositionX + 240, 32), Color.White, Font);

        GoodText = new(
            StatisticData[0].ToString(), 
            new(PositionX + 611, 32), PositiveColor, 
            Font);
    }

    public void SetStatistic(int Good, int Evil)
    {
        StatisticData = [Good, Evil];

        EvilText.Value = Evil.ToString();
        GoodText.Value = Good.ToString();

        if (Good >= Evil)
            StatText.Value = Config.Config.GoodName;
        else 
            StatText.Value = Config.Config.EvilName;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(StatisticTexture, StatisticRectangle, Color.White);

        EvilText.Draw(gameTime, spriteBatch);
        StatText.Draw(gameTime, spriteBatch);
        GoodText.Draw(gameTime, spriteBatch);
    }

    public override void Update(GameTime gameTime)
    {
        EvilText.Update(gameTime);
        StatText.Update(gameTime);
        GoodText.Update(gameTime);
    }
}