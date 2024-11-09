using Raylib_cs;
using Pixelworld.Managers;

namespace Pixelworld;

internal class Game
{
    public static readonly byte RectSize = 45;

    private readonly WorldManager worldManager;
    private readonly CameraManager cameraManager;
    private readonly TextureManager textureManager;

    private readonly bool ShowFPS = true;

    public Game()
    {
        worldManager = new WorldManager(250, 70);
        cameraManager = new CameraManager(worldManager.Width / 2, 3);
        textureManager = new TextureManager();
    }

    public void Run(string windowTitle, short width, short height)
    {
        InitWindow(windowTitle, width, height);
        textureManager.Load();
        worldManager.CreateWorld();

        while (!(Raylib.WindowShouldClose() && !Raylib.IsKeyPressed(KeyboardKey.Escape)))
        {
            Render();
            cameraManager.Update(worldManager.Width, worldManager.Height);
        }

        Terminate();
    }

    private void InitWindow(string windowTitle, short width, short height)
    {
        Raylib.SetConfigFlags(ConfigFlags.ResizableWindow);
        Raylib.InitWindow(width, height, windowTitle);
        Raylib.SetTargetFPS(30);

        Image icon = Raylib.LoadImage("Assets/app-icon.png");
        Raylib.SetWindowIcon(icon);
        Raylib.UnloadImage(icon);
    }

    private void Render()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.Black);

        worldManager.Draw(cameraManager.Position, textureManager);
        
        if (ShowFPS)
        {
            int offsetX = (Raylib.GetScreenWidth() % RectSize) / 2;
            int offsetY = (Raylib.GetScreenHeight() % RectSize) / 2;

            Raylib.DrawFPS(10 + offsetX, 10 + offsetY);
        }

        Raylib.EndDrawing();
    }

    private void Terminate()
    {
        textureManager.Unload();
        Raylib.CloseWindow();
        Console.WriteLine("[APP]: Pixelworld unloaded all it's resources from the memory.");
    }
}
