using System.Diagnostics;
using System.Numerics;
using Raylib_cs;
using Pixelworld.Utilities;

namespace Pixelworld.Managers;

internal class WorldManager(byte width, byte height)
{
    private Terrain.Types[,] world = new Terrain.Types[width, height];

    /// <summary>
    /// Returns the width of the world.
    /// </summary>
    public int Width => world.GetLength(0);

    /// <summary>
    /// Returns the height of the world.
    /// </summary>
    public int Height => world.GetLength(1);

    public Terrain.Types[,] World => world;

    public void CreateWorld()
    {
        var s = new Stopwatch();
        s.Start();

        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                world[i, j] = Terrain.Types.Sky;
            }
        }

        // Handling the procedural surface.
        byte[] surface = new byte[Width];
        byte currentValue = (byte)Generator.Number(9, 12);

        for (short i = 0; i < Width;)
        {
            byte generationValue = (byte)Generator.NextNumber(currentValue, 11, 14);
            byte iterationCount = (byte)Generator.Number(6, 12);

            for (int j = 0; j < iterationCount && i + j < Width; j++)
                surface[i + j] = generationValue;

            currentValue = generationValue;
            i += iterationCount;
        }

        // Set grass & first stone layer.
        for (short i = 0; i < Width; i++)
        {
            world[i, surface[i]] = Terrain.Types.Grass;
            world[i, surface[i] + 6] = Terrain.Types.Stone;
        }

        // Set dirt layer.
        for (short i = 0; i < Width; i++)
            for (short j = 2; j < Height; j++)
                if (world[i, j] == Terrain.Types.Grass)
                    for (int k = 1; k < 5; k++)
                        world[i, j + k] = Terrain.Types.Dirt;

        // Set stone layer.
        for (short i = 0; i < Width; i++)
        {
            for (short j = 0; j < Height; j++)
            {
                if (world[i, j] == Terrain.Types.Dirt && world[i, j + 1] != Terrain.Types.Dirt)
                {
                    short y = j;
                    while (y < Height - 1)
                    {
                        y++;
                        world[i, y] = Terrain.Types.Stone;
                    }
                }
            }
        }

        // Set bedrock layer.
        for (short i = 0; i < Width; i++)
        {
            for (short j = (short)(Height - 2); j < Height; j++)
            {
                world[i, j] = Terrain.Types.Bedrock;

                if (Generator.Number(1, 50) % 2 == 0) world[i, j - 1] = Terrain.Types.Bedrock;
            }
        }

        CreateTrees();

        CreateOreVeins(
            type: Terrain.Types.CoalOre,
            chance: 0.5f,
            yPos: 25,
            minWidth: 2,
            minHeight: 1,
            maxWidth: 6,
            maxHeight: 3,
            veinDistance: 10
        );

        CreateOreVeins(
            type: Terrain.Types.IronOre,
            chance: 0.3f,
            yPos: 35,
            minWidth: 2,
            minHeight: 1,
            maxWidth: 4,
            maxHeight: 3,
            veinDistance: 15
        );

        CreateOreVeins(
            type: Terrain.Types.GoldOre,
            chance: 0.25f,
            yPos: 50,
            minWidth: 2,
            minHeight: 1,
            maxWidth: 4,
            maxHeight: 2,
            veinDistance: 20
        );

        CreateOreVeins(
            type: Terrain.Types.LapisOre,
            chance: 0.3f,
            yPos: 45,
            minWidth: 2,
            minHeight: 1,
            maxWidth: 4,
            maxHeight: 2,
            veinDistance: 15
        );

        CreateOreVeins(
            type: Terrain.Types.RedstoneOre,
            chance: 0.3f,
            yPos: 45,
            minWidth: 2,
            minHeight: 1,
            maxWidth: 4,
            maxHeight: 2,
            veinDistance: 15
        );

        CreateOreVeins(
            type: Terrain.Types.DiamondOre,
            chance: 0.3f,
            yPos: 55,
            minWidth: 1,
            minHeight: 1,
            maxWidth: 3,
            maxHeight: 2,
            veinDistance: 20
        );

        CreateOreVeins(
            type: Terrain.Types.EmeraldOre,
            chance: 0.25f,
            yPos: 60,
            minWidth: 1,
            minHeight: 1,
            maxWidth: 3,
            maxHeight: 1,
            veinDistance: 25
        );

        s.Stop();
        Console.WriteLine($"[APP]: The procedural world generation took {s.ElapsedMilliseconds} ms.");
        s.Reset();
    }

    private void CreateTrees()
    {
        const byte MaxDistance = 15;
        byte counter = 0;

        for (short i = 0; i < Width; i++)
        {
            counter++;
            for (short j = 0; j < Height; j++)
            {
                if (i > 1 && world[i, j] == Terrain.Types.Grass && (world[i - 1, j] != Terrain.Types.Water || world[i - 1, j] != Terrain.Types.Sand) && world[i - 2, j] != Terrain.Types.Water && world[i - 2, j] != Terrain.Types.Sand)
                {
                    if (counter > MaxDistance)
                    {

                        for (byte logs = 1; logs <= 3; logs++)
                        {
                            world[i, j - logs] = Terrain.Types.Log;
                        }

                        for (byte logLeaves = 4; logLeaves <= 6; logLeaves++)
                        {
                            world[i, j - logLeaves] = Terrain.Types.TreeLeaves;

                            if (logLeaves == 6) world[i, j - logLeaves] = Terrain.Types.Leaves;
                        }

                        for (byte leaves = 4; leaves <= 6; leaves++)
                        {
                            for (byte sideLeaves = 1; sideLeaves <= 2; sideLeaves++)
                            {
                                if (i - sideLeaves >= 0 && j - leaves >= 0 && i + sideLeaves < world.GetLength(0))
                                {
                                    world[i - sideLeaves, j - leaves] = Terrain.Types.Leaves;
                                    world[i + sideLeaves, j - leaves] = Terrain.Types.Leaves;
                                }
                            }
                        }

                        for (short leaves = -1; leaves <= 1; leaves++)
                        {
                            if (i + leaves < Width)
                                world[i + leaves, j - 7] = Terrain.Types.Leaves;
                        }

                        world[i, j] = Terrain.Types.Dirt;
                        counter = 0;
                    }
                }
            }
        }
    }

    private void CreateOreVeins(Terrain.Types type, float chance, int yPos, byte minWidth, byte minHeight, byte maxWidth, byte maxHeight, ushort veinDistance)
    {
        int veinCount = (int)Math.Ceiling((float)world.GetLength(0) / veinDistance);

        for (int i = 0; i < veinCount; i++)
        {
            int width = Generator.Number(minWidth, maxWidth);
            int height = Generator.Number(minHeight, maxHeight);
            int veinYPos;

            do
            {
                veinYPos = Generator.Number(yPos - 3, yPos + 3);
            } while (veinYPos >= world.GetLength(1) - 3);

            int startX = i * veinDistance;
            int startY = veinYPos - height / 2;

            startY = Math.Max(0, startY);
            startY = Math.Min(Height - height, startY);

            for (int x = startX; x < startX + width; x++)
            {
                for (int y = startY; y < startY + height; y++)
                {
                    if (x >= 0 && x < Width && y >= 0 && y < Height)
                    {
                        if (Generator.Chance(chance))
                        {
                            world[x, y] = type;
                        }
                    }
                }
            }
        }
    }
}
