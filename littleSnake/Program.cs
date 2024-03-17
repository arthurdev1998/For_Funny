using littleSnake;
using littleSnake.ExtensionMethods;

var coordnadas = SnakeExtensions.CreateSnake();
Configs.screen.CreateFood();
SnakeExtensions.ReadTouch();

while(Configs.runningGaming)
{
    Thread.Sleep(50);
    SnakeExtensions.MoveSnake(coordnadas);
    SnakeExtensions.RenderizarGame();
}

SnakeExtensions.GameOver();




