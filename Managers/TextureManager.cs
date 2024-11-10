using Raylib_cs;
using Pixelworld.Utilities;

namespace Pixelworld.Managers;

internal class TextureManager
{
    public Dictionary<Terrain.Types, Rectangle> TextureRegions { get; private set; } = [];
    private Texture2D atlas;
    private const byte textureSize = 32;

    public Texture2D Atlas
    {
        get
        {
            return atlas;
        }
    }

    public void Load()
    {
        atlas = Raylib.LoadTexture("Assets/Textures/atlas.png");

        SetTextureRegion(Terrain.Types.Bedrock, 0, 0);
        SetTextureRegion(Terrain.Types.Stone, 1, 0);
        SetTextureRegion(Terrain.Types.Dirt, 2, 0);
        SetTextureRegion(Terrain.Types.Grass, 3, 0);
        SetTextureRegion(Terrain.Types.Sand, 4, 0);
        SetTextureRegion(Terrain.Types.Gravel, 5, 0);
        SetTextureRegion(Terrain.Types.Log, 6, 0);
        SetTextureRegion(Terrain.Types.Leaves, 7, 0);
        SetTextureRegion(Terrain.Types.TreeLeaves, 8, 0);
        SetTextureRegion(Terrain.Types.Water, 9, 0);
        SetTextureRegion(Terrain.Types.CoalOre, 10, 0);
        SetTextureRegion(Terrain.Types.IronOre, 11, 0);
        SetTextureRegion(Terrain.Types.GoldOre, 12, 0);
        SetTextureRegion(Terrain.Types.LapisOre, 13, 0);
        SetTextureRegion(Terrain.Types.RedstoneOre, 14, 0);
        SetTextureRegion(Terrain.Types.EmeraldOre, 15, 0);
        SetTextureRegion(Terrain.Types.DiamondOre, 0, 1);
    }

    private void SetTextureRegion(Terrain.Types type, int column, int row)
    {
        TextureRegions[type] = new Rectangle(column * textureSize, row * textureSize, textureSize, textureSize);
    }

    public Rectangle GetTextureRegion(Terrain.Types type)
    {
        return TextureRegions[type];
    }

    public void Unload()
    {
        Raylib.UnloadTexture(atlas);
    }
}
