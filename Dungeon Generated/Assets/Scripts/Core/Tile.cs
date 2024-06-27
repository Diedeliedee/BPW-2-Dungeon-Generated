using UnityEngine;

public class Tile
{
    public Vector2Int coordinates;

    public Vector2 center => new Vector2(coordinates.x + 0.5f, coordinates.y + 0.5f);

    public Tile(Vector2Int _coordinates)
    {
        coordinates = _coordinates;
    }
}
