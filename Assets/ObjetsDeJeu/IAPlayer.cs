namespace Assets.ObjetsDeJeu
{
    public interface IAPlayer
    {
        Coordonnees GetBestMove(Goban goban);
    }
}