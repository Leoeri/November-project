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
            int timer = 0;
            int point = 0;
            int highScore = 0;
            int whatObstacle = 1;
            int inARow = 0;
            int inARow2 = 0;

            float playerX = 100;
            float playerY = windowHeight / 4 * 3 - 30;
            float obstacleX = 801;
            float gravity = 0.1f;
            float speed = 0.1f;

            bool newGame = true;
            bool tutorial = true;
            bool cooldown = false;



            Random generator = new Random();
            Rectangle ground;
            Rectangle player;
            Rectangle obstacle = new Rectangle((int)obstacleX, windowHeight / 4 * 3 - 50, 30, 50);
            Raylib.InitWindow(windowWidth, windowHeight, "The game");

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
                    if (newGame)   //om man startar om från game over scenen
                    {
                        playerY = windowHeight / 4 * 3 - 30;  // start position
                        obstacleX = 801;            //hindrets startposition
                        point = 0;                  //poäng
                        speed = 0.1f;               //farten av hinder
                        newGame = false;
                    }
                    Raylib.ClearBackground(Color.WHITE);
                    ground = new Rectangle(0, windowHeight / 4 * 3, windowWidth, windowHeight / 2);
                    player = new Rectangle((int)playerX, (int)playerY, 20, 30);
                    if (whatObstacle == 1)   //kollar vilket hinder som ska placeras
                    {
                        obstacle = new Rectangle((int)obstacleX, windowHeight / 4 * 3 - 50, 30, 50);

                    }
                    else if (whatObstacle == 2)
                    {
                        obstacle = new Rectangle((int)obstacleX, 0, 30, 430);
                        if (tutorial)       //Om det är första gången andra hindret kommer upp visar hur man duckar
                        {
                            Raylib.DrawText("press S to duck", (int)obstacleX + 50, windowHeight / 4, 25, Color.BLACK);
                        }
                    }
                    if (whatObstacle == 3)
                    {
                        obstacle = new Rectangle((int)obstacleX, windowHeight / 4 * 3 - 50, 60, 50);
                    }
                    else if (whatObstacle == 4)
                    {
                        obstacle = new Rectangle((int)obstacleX, 0, 60, 430);
                        if (tutorial)       //Om det är första gången andra hindret kommer upp visar hur man duckar
                        {
                            Raylib.DrawText("press S to duck", (int)obstacleX + 80, windowHeight / 4, 25, Color.BLACK);
                        }
                    }



                    if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN) || Raylib.IsKeyDown(KeyboardKey.KEY_S) || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_CONTROL))
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
                    if (whatObstacle == 1 || whatObstacle == 2)
                    {
                        if (obstacleX <= -30) // om hindret är utanför skärmen
                        {
                            speed += 0.01f;  // farten ökar 
                            obstacleX = 801;
                            point++;
                            if (whatObstacle == 2 || whatObstacle == 4)
                            {
                                tutorial = false;
                                inARow2 += 1;
                            }
                            else if (whatObstacle == 1 || whatObstacle == 3)
                            {
                                inARow += 1;
                            }


                            whatObstacle = generator.Next(1, 5);

                            if (inARow == 4)  //ser till så att det inte blir mer än 4 av samma hinder i rad
                            {
                                whatObstacle = 2;
                                inARow = 0;
                            }
                            else if (inARow2 == 4)
                            {
                                whatObstacle = 1;
                                inARow2 = 0;
                            }


                        }
                    }
                    else if (whatObstacle == 3 || whatObstacle == 4)
                    {
                        if (obstacleX <= -60)
                        {
                            speed += 0.01f;  // farten ökar 
                            obstacleX = 801;
                            point++;
                            if (whatObstacle == 2 || whatObstacle == 4)
                            {
                                tutorial = false;
                                inARow2 += 1;
                            }
                            else if (whatObstacle == 1 || whatObstacle == 3)
                            {
                                inARow += 1;
                            }


                            whatObstacle = generator.Next(1, 5);

                            if (inARow == 4)  //ser till så att det inte blir mer än 4 av samma hinder i rad
                            {
                                whatObstacle = 2;
                                inARow = 0;
                            }
                            else if (inARow2 == 4)
                            {
                                whatObstacle = 1;
                                inARow2 = 0;
                            }


                        }



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
                    buttonpressed();
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

        static void buttonpressed()  // visar vilka knappar man klickar på
        {
            Raylib.DrawText("space", 580, 5, 20, Color.BLACK);
            Raylib.DrawText("w", 580, 25, 20, Color.BLACK);
            Raylib.DrawText("up", 580, 45, 20, Color.BLACK);
            Raylib.DrawText("ctrl", 700, 5, 20, Color.BLACK);
            Raylib.DrawText("s", 700, 25, 20, Color.BLACK);
            Raylib.DrawText("down", 700, 45, 20, Color.BLACK);

            if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE)) { Raylib.DrawText("space", 580, 5, 20, Color.RED); }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_W)) { Raylib.DrawText("w", 580, 25, 20, Color.RED); }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_UP)) { Raylib.DrawText("up", 580, 45, 20, Color.RED); }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_CONTROL)) { Raylib.DrawText("ctrl", 700, 5, 20, Color.RED); }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_S)) { Raylib.DrawText("s", 700, 25, 20, Color.RED); }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN)) { Raylib.DrawText("down", 700, 45, 20, Color.RED); }

        }

    }

}
