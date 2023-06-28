using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2Int m_coordinates;

    public Vector2Int coordinates
    {
        get => m_coordinates;
        set
        {
            var pos = Dungeon.CoordsToPos(value);

            m_coordinates = value;
            transform.position = new Vector3(pos.x, value.y + 0.5f, transform.position.z);
        }
    }

    public Vector2 position
    {
        get => transform.position;
        set
        {
            m_coordinates       = Dungeon.PosToCoords(value);
            transform.position  = new Vector3(value.x, value.y, transform.position.z);
        }
    }

    private void Update()
    {
        var input = Vector2Int.zero;

        if (Input.GetKeyDown(KeyCode.W)) input.y++;
        if (Input.GetKeyDown(KeyCode.S)) input.y--;
        if (Input.GetKeyDown(KeyCode.A)) input.x--;
        if (Input.GetKeyDown(KeyCode.D)) input.x++;

        if (input == Vector2Int.zero) return;
        MoveTo(input);
    }

    public void MoveTo(Vector2Int direction)
    {
        if (!Dungeon.instance.HasTile(coordinates + direction)) return;
        coordinates += direction;
    }
}
