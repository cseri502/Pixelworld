using Raylib_cs;
using System.Numerics;

namespace Pixelworld.Managers;

internal class CameraManager(int x, int y)
{
    public Vector2 Position { get; private set; } = new Vector2(x, y);

    public void Update(int worldWidth, int worldHeight)
    {
        int gridSizeX = Raylib.GetScreenWidth() / Game.RectSize;
        int gridSizeY = Raylib.GetScreenHeight() / Game.RectSize;
        int maxX = worldWidth - gridSizeX;
        int maxY = worldHeight - gridSizeY;

        Position = new Vector2(Math.Max(0, Math.Min(Position.X, maxX)), Math.Max(0, Math.Min(Position.Y, maxY)));

        if (Raylib.IsMouseButtonDown(MouseButton.Right))
        {
            Move(new Vector2(gridSizeX, gridSizeY), new Vector2(worldWidth, worldHeight));
        }
    }

    private void Move(Vector2 gridSize, Vector2 worldSize)
    {
        int width = Raylib.GetScreenWidth(), height = Raylib.GetScreenHeight();
        Vector2 mousePos = Raylib.GetMousePosition();
        Vector2 newPosition = Position;

        if (mousePos.X > width * 0.85)
        {
            if (worldSize.X > newPosition.X + gridSize.X)
                newPosition.X++;
        }

        if (mousePos.X < width * 0.15)
        {
            if (newPosition.X > 0)
                newPosition.X--;
        }

        if (mousePos.Y > height * 0.85)
        {
            if (worldSize.Y > newPosition.Y + gridSize.Y)
                newPosition.Y++;
        }

        if (mousePos.Y < height * 0.15)
        {
            if (newPosition.Y > 0)
                newPosition.Y--;
        }

        Position = newPosition;
    }
}
