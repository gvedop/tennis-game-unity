using UnityEngine;

namespace TennisGame.AI.Simple
{
    public static class SimpleAIQualityExtension
    {
        public static ISimpleAIQuality GetSimpleAIQuality(this AIQuality obj)
        {
            switch (obj)
            {
                case AIQuality.Low:
                    return new LowSimpleAIQuality();
                case AIQuality.Middle:
                    return new MiddleSimpleAIQuality();
                case AIQuality.High:
                    return new HighSimpleAIQuality();
                default:
                    throw new UnityException();
            }
        }
    }
}
