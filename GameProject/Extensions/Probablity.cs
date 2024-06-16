using System;
using System.Collections.Generic;

namespace GameProject.Extensions;

public static class Probablity
{
    private static bool[] CreateVariants(int maybeCount, int allCount)
    {
        var outcomes = new List<bool>();

        for (var i = 0; i < maybeCount; i++)
            outcomes.Add(true);

        for (var i = 0; i < allCount - maybeCount; i++)
            outcomes.Add(false);

        return [..outcomes];
    }

    public static bool GetRandom(int maybeCount, int allCount)
    {
        var variants = CreateVariants(maybeCount, allCount);
        var random = new Random();
        var randomIndex = random.NextInt64(1, variants.Length) - 1;

        return variants[randomIndex];
    }
}