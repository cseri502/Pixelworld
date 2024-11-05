using Raylib_cs;
using Pixelworld.Utilities;

namespace Pixelworld.Managers;

internal class TextureManager
{
    public Dictionary<Terrain.Types, Texture2D> Textures { get; private set; } = [];

    public void LoadAll()
    {
        LoadOne(Terrain.Types.Grass, "Assets/Textures/grass.png");
        LoadOne(Terrain.Types.Dirt, "Assets/Textures/dirt.png");
        LoadOne(Terrain.Types.Sand, "Assets/Textures/sand.png");
        LoadOne(Terrain.Types.Stone, "Assets/Textures/stone.png");
        LoadOne(Terrain.Types.Bedrock, "Assets/Textures/bedrock.png");
        LoadOne(Terrain.Types.Log, "Assets/Textures/log.png");
        LoadOne(Terrain.Types.Leaves, "Assets/Textures/leaves.png");
        LoadOne(Terrain.Types.TreeLeaves, "Assets/Textures/tree-leaves.png");
        LoadOne(Terrain.Types.CoalOre, "Assets/Textures/coal_ore.png");
        LoadOne(Terrain.Types.IronOre, "Assets/Textures/iron_ore.png");
        LoadOne(Terrain.Types.GoldOre, "Assets/Textures/gold_ore.png");
        LoadOne(Terrain.Types.LapisOre, "Assets/Textures/lapis_ore.png");
        LoadOne(Terrain.Types.RedstoneOre, "Assets/Textures/redstone_ore.png");
        LoadOne(Terrain.Types.DiamondOre, "Assets/Textures/diamond_ore.png");
        LoadOne(Terrain.Types.EmeraldOre, "Assets/Textures/emerald_ore.png");
    }

    private void LoadOne(Terrain.Types id, string path)
    {
        Image image = Raylib.LoadImage(path);
        Texture2D texture = Raylib.LoadTextureFromImage(image);
        texture.Width = Game.RectSize;
        texture.Height = Game.RectSize;
        Raylib.UnloadImage(image);
        Textures.Add(id, texture);
    }

    public void UnloadAll()
    {
        foreach (var texture in Textures.Values)
            Raylib.UnloadTexture(texture);
    }
}
