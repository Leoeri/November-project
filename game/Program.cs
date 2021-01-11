using System;
using Raylib_cs;

namespace game
{
    class Program
    {
        static void Main(string[] args)
        {
            string scene = "intro";
            int windowHeight = 600;
            int windowWidth = 800;
            float playerX = 100;
            float playerY = windowHeight / 4 * 3 - 30;
            float obstacleX = 801;
            float gravity = 0.1f;
            float speed = 0.1f;
            int timer = 0;
            int point = 0;
            int highScore = 0;
            bool newGame = true;
            bool tutorial = true;
            bool cooldown = false;
            int whatObstacle = 1;
            Random generator = new Random();
            Raylib.InitWindow(windowWidth, windowHeight, "The game");
            Rectangle ground;
            Rectangle player;
            Rectangle obstacle = new Rectangle((int)obstacleX, windowHeight / 4 * 3 - 50, 30, 50);

            while (!Raylib.WindowShouldClose())
            {

                Raylib.BeginDrawing();


                if (scene == "intro")
                {
                    Raylib.ClearBackground(Color.WHITE);

                    ground = new Rectangle(0, windowHeight / 4 * 3, windowWidth, windowHeight / 2);
                    player = new Rectangle((int)playerX, (int)playerY, 20, 30);

                    Raylib.DrawText("Hurdle Sprint", 225, windowHeight / 4, 50, Color.BLACK);
                    Raylib.DrawText("press SPACE to start", 260, windowHeight / 2, 25, Color.BLACK);
                    Raylib.DrawRectangleRec(player, Color.BLACK);
                    Raylib.DrawRectangleRec(ground, Color.GRAY);
                    if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE) || Raylib.IsKeyDown(KeyboardKey.KEY_UP) || Raylib.IsKeyDown(KeyboardKey.KEY_W))
                    {
                        scene = "game";
                    }

                }

                if (scene == "game")
                {
                    if (newGame)
                    {
                        playerY = windowHeight / 4 * 3 - 30;
                        obstacleX = 801;
                        point = 0;
                        speed = 0.1f;
                        newGame = false;
                    }
                    Raylib.ClearBackground(Color.WHITE);
                    ground = new Rectangle(0, windowHeight / 4 * 3, windowWidth, windowHeight / 2);
                    player = new Rectangle((int)playerX, (int)playerY, 20, 30);
                    if (whatObstacle == 1)
                    {
                        obstacle = new Rectangle((int)obstacleX, windowHeight / 4 * 3 - 50, 30, 50);

                    }
                    if (whatObstacle == 2)
                    {
                        obstacle = new Rectangle((int)obstacleX, 0, 30, 430);
                        if (tutorial)
                        {
                            Raylib.DrawText("press S to duck", (int)obstacleX + 50, windowHeight / 4, 25, Color.BLACK);
                        }
                    }



                    if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN) || Raylib.IsKeyDown(KeyboardKey.KEY_S))
                    {
                        player = new Rectangle((int)playerX, (int)playerY, 30, 20);
                    }
                    if (!Raylib.CheckCollisionRecs(player, ground))
                    {
                        playerY += gravity;
                    }
                    if (Raylib.CheckCollisionRecs(player, ground))
                    {
                        playerY -= 1;
                    }
                    if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE) || Raylib.IsKeyDown(KeyboardKey.KEY_UP) || Raylib.IsKeyDown(KeyboardKey.KEY_W))
                    {
                        if (!cooldown)
                        {
                            timer++;
                            playerY -= 0.2f;
                        }
                        if (timer >= 2000)
                        {
                            cooldown = true;
                        }
                        if (Raylib.CheckCollisionRecs(player, ground))
                        {
                            cooldown = false;
                            timer = 0;
                        }

                    }
                    if (Raylib.IsKeyUp(KeyboardKey.KEY_SPACE) && Raylib.IsKeyUp(KeyboardKey.KEY_UP) && Raylib.IsKeyUp(KeyboardKey.KEY_W))
                    {
                        cooldown = true;
                    }

                    obstacleX -= speed;
                    if (obstacleX <= -30)
                    {
                        speed += 0.01f;
                        obstacleX = 801;
                        point++;
                        if (whatObstacle == 2)
                        {
                            tutorial = false;
                        }
                        whatObstacle = generator.Next(1, 3);
                    }
                    if (Raylib.CheckCollisionRecs(player, obstacle))
                    {
                        if (highScore < point)
                        {
                            highScore = point;
                        }
                        scene = "game over";
                    }
                    Raylib.DrawText(point.ToString(), 5, 5, 40, Color.BLACK);
                    Raylib.DrawRectangleRec(player, Color.BLACK);
                    Raylib.DrawRectangleRec(ground, Color.GRAY);
                    Raylib.DrawRectangleRec(obstacle, Color.BEIGE);
                }
                else if (scene == "game over")
                {
                    Raylib.ClearBackground(Color.BLACK);

                    Raylib.DrawText("Game Over", 150, windowHeight / 2 - 50, 100, Color.RED);
                    Raylib.DrawText("Score: " + point.ToString(), 300, 350, 50, Color.RED);
                    Raylib.DrawText("HighScore: " + highScore.ToString(), 250, 400, 50, Color.RED);
                    Raylib.DrawText("press R to retry", 300, windowHeight / 4 * 3, 25, Color.RED);
                    if (Raylib.IsKeyDown(KeyboardKey.KEY_R))
                    {
                        scene = "game";
                        newGame = true;
                    }
                }

                Raylib.EndDrawing();
            }
        }
    }
}
