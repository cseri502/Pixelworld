namespace Pixelworld.Utilities;

internal class Terrain
{
    public static Types[] breakWhitelist = [Types.Sky, Types.Water, Types.Bedrock];

    public enum Types
    {
        Sky,
        Grass,
        Dirt,
        Stone,
        Water,
        Leaves,
        Log,
        TreeLeaves,
        Sand,
        CoalOre,
        IronOre,
        GoldOre,
        LapisOre,
        RedstoneOre,
        DiamondOre,
        EmeraldOre,
        Bedrock,
    }
}
