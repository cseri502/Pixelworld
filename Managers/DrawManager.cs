using Pixelworld.Utilities;
using Raylib_cs;
using System.Numerics;

namespace Pixelworld.Managers;

internal class DrawManager
{
    public static void RenderWorld(Game g, CameraManager cm, TextureManager tm, WorldManager wm)
    {
        for (int x = 0; x < g.screenWidth / Game.RectSize; x++)
        {
            for (int y = 0; y < g.screenHeight / Game.RectSize; y++)
            {
                Vector2 position = new Vector2(x * Game.RectSize + g.offsetX, y * Game.RectSize + g.offsetY);
                Rectangle rect = new(position, new Vector2(Game.RectSize, Game.RectSize));
                bool isHovered = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), rect);

                DrawTile(rect, x, y, isHovered, cm.Position, tm, wm);
            }
        }
    }

    private static void DrawTile(Rectangle rect, int x, int y, bool isHovered, Vector2 camPos, TextureManager tm, WorldManager wm)
    {
        int posX = x + (int)Math.Floor(camPos.X);
        int posY = y + (int)Math.Floor(camPos.Y);
        var world = wm.World;

        if (world[posX, posY] == Terrain.Types.Sky)
            Raylib.DrawRectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height, Color.SkyBlue);
        else
        {
            Rectangle sourceRect = tm.GetTextureRegion(world[posX, posY]);
            Rectangle destRect = new Rectangle(rect.X, rect.Y, Game.RectSize, Game.RectSize);

            Raylib.DrawTexturePro(tm.Atlas, sourceRect, destRect, Vector2.Zero, 0f, Color.White);
        }

        /*if (DebugModeEnabled)
            DrawPositions(rect, new Vector2(camPos.X + x, camPos.Y + y), inter);*/

        if (isHovered && !Terrain.breakWhitelist.Contains(world[posX, posY]))
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                if (world[posX, posY] == Terrain.Types.TreeLeaves)
                    world[posX, posY] = Terrain.Types.Log;
                else
                    world[posX, posY] = Terrain.Types.Sky;
            }

            Raylib.DrawRectangleLinesEx(rect, 1, Color.Black);
        }
    }
}
