namespace littleSnake.ExtensionMethods;

public static class SnakeExtensions
{
    public static IList<Coordenadas> CreateSnake()
    {
        IList<Coordenadas> coordenadas = new List<Coordenadas>();
        coordenadas.Add(Coordenadas.Create(9, 14));
        coordenadas.Add(Coordenadas.Create(8, 14));
        coordenadas.Add(Coordenadas.Create(7, 14));

        return coordenadas;
    }

    public static string[,] AttPosicionSnake(this string[,] tela, List<Coordenadas> coordenadas)
    {
        var coordX = coordenadas.Select(x => x.CoordenadaX);
        var coordY = coordenadas.Select(x => x.CoordenadaY);

        for (int i = 0; i < tela.GetLength(0); i++)
        {
            for (int j = 0; j < tela.GetLength(1); i++)
            {
                if (coordX.Any(x => x == i) && coordY.Any(x => x == j))
                {
                    tela[i, j] = Configs.bodySnake;
                    continue;
                }

                tela[i, j] = " ";
            }
        }

        return tela;
    }

    public static void CreateFood(this string[,] tela)
    {
        Random random = new();
        int coordenadaFoodX;
        int coordenadaFoodY;

        do
        {
            coordenadaFoodX = random.Next(tela.GetLength(0));
            coordenadaFoodY = random.Next(tela.GetLength(1));
        }
        while (tela[coordenadaFoodX, coordenadaFoodY] == null
            && tela[coordenadaFoodX, coordenadaFoodY] == " ");

        tela[coordenadaFoodX, coordenadaFoodY] = "*";
    }

    public static void ReadActionTouch()
    {
        while (Configs.runningGaming)
        {
            Console.Read();
            var tecla = Console.ReadKey();

            if (tecla.Key == ConsoleKey.UpArrow && Configs.direcao != DirecaoEnum.paraBaixa)
                Configs.direcao = DirecaoEnum.paraCima;

            if (tecla.Key == ConsoleKey.DownArrow && Configs.direcao != DirecaoEnum.paraCima)
                Configs.direcao = DirecaoEnum.paraCima;

            if (tecla.Key == ConsoleKey.LeftArrow && Configs.direcao != DirecaoEnum.paraDireita)
                Configs.direcao = DirecaoEnum.paraCima;

            if (tecla.Key == ConsoleKey.RightArrow && Configs.direcao != DirecaoEnum.paraEsquierda)
                Configs.direcao = DirecaoEnum.paraCima;
        }
    }

    public static void ReadTouch()
    {
        Thread task = new(ReadActionTouch);
        task.Start();
    }

    public static void MoveSnake(IList<Coordenadas> coordenadasSnake)
    {
        var headSnake = coordenadasSnake[0];
        var tailXCoordenada = coordenadasSnake[^1].CoordenadaX;
        var tailYCoordenada = coordenadasSnake[^2].CoordenadaY;

        for (int i = coordenadasSnake.Count - 1; i > 0; i--)
        {
            coordenadasSnake[i].CoordenadaX = coordenadasSnake[i - 1].CoordenadaX;
            coordenadasSnake[i].CoordenadaY = coordenadasSnake[i - 1].CoordenadaY;
        }

        if (Configs.direcao == DirecaoEnum.paraDireita)
        {
            headSnake.CoordenadaX += 1;
            if (headSnake.CoordenadaX > Configs.larguraTela)
                headSnake.CoordenadaX = 0;
        }

        if (Configs.direcao == DirecaoEnum.paraEsquierda)
        {
            headSnake.CoordenadaX -= 1;
            if (headSnake.CoordenadaX < 0)
                headSnake.CoordenadaX = Configs.larguraTela - 1;
        }

        if (Configs.direcao == DirecaoEnum.paraCima)
        {
            headSnake.CoordenadaY += 1;
            if (headSnake.CoordenadaY > Configs.alturaTela)
                headSnake.CoordenadaY = 0;
        }

        if (Configs.direcao == DirecaoEnum.paraBaixa)
        {
            headSnake.CoordenadaY -= 1;
            if (headSnake.CoordenadaY < Configs.alturaTela)
                headSnake.CoordenadaY = Configs.alturaTela - 1;
        }

        if (Configs.screen[headSnake.CoordenadaX, headSnake.CoordenadaY] == "*")
        {
            coordenadasSnake.Add(Coordenadas.Create(headSnake.CoordenadaX, headSnake.CoordenadaY));
            MoveSnake(coordenadasSnake);
        }

        if (Configs.screen[headSnake.CoordenadaX, headSnake.CoordenadaY] == Configs.bodySnake)
        {
            Console.WriteLine("Voce perdeu");
            return;
        }
    }

    public static void RenderizarGame()
    {
        Console.Clear();
        var renderizaionScreen = "";
        for(int a = 0; a < Configs.alturaTela; a++)
        {
            for(int i = 0; i < Configs.larguraTela; i++)
            {
                if(Configs.screen[i,a] is not null or " ")
                {
                    renderizaionScreen += Configs.screen[i,a];
                }
                else
                {
                    renderizaionScreen += " ";
                }
            }

            renderizaionScreen += "\n";
        }

        Console.WriteLine(renderizaionScreen);
    }

    public static void GameOver()
    {
        Console.Clear();
        Console.WriteLine("Voce perdeu");
    }
}