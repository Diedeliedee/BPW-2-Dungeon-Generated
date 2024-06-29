public class MovementCallBack
{
    public Tile targetTile;
    public Condition condition;

    public enum Condition
    {
        ACCESIBLE,
        OCCUPIED,
        OUT_OF_BOUNDS,
        OUT_OF_MOVEMENT,
    }
}

