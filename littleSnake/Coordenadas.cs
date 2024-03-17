namespace littleSnake;

public class Coordenadas
{
    public int CoordenadaX { get; set; }
    public int CoordenadaY { get; set; }
    private Coordenadas()
    { }

    public static Coordenadas Create(int eixoX = 0, int eixoY = 0)
    {
        return new Coordenadas()
        {
            CoordenadaX = eixoX,
            CoordenadaY = eixoY
        };
    }
}