namespace littleSnake;

public static class Configs
{
    public static DirecaoEnum direcao = DirecaoEnum.paraDireita;
    public static readonly int alturaTela = 230;
    public static readonly int larguraTela = 300;
    public static readonly string bodySnake = "■";
    public static bool runningGaming = true;
    public static readonly string[,] screen  = new string [larguraTela,alturaTela];

    public static string[,]? coordFood = default;
}