using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Tile m_currentTile = null;
    private Vector2Int m_coordinates;

    public System.Action<Vector2> onMouseClick = null;
    public System.Action<Vector2> onMouseDrag = null;
    public System.Action<Vector2> onMouseRelease = null;

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
            m_coordinates = Dungeon.PosToCoords(value);
            transform.position = new Vector3(value.x, value.y, transform.position.z);
        }
    }

    public void Setup()
    {
        Move(Vector2Int.zero);
    }

    private void Tick()
    {
        var input = Vector2Int.zero;

        if (Input.GetKeyDown(KeyCode.W)) input.y++;
        if (Input.GetKeyDown(KeyCode.S)) input.y--;
        if (Input.GetKeyDown(KeyCode.A)) input.x--;
        if (Input.GetKeyDown(KeyCode.D)) input.x++;

        if (input == Vector2Int.zero) return;
        Move(input);
    }

    public void Move(Vector2Int direction)
    {
        if (!GameManager.instance.dungeon.HasTile(coordinates + direction, out Tile tile)) return;

        coordinates     += direction;
        m_currentTile   = tile;
    }

    private void OnMouseDown()  => onMouseClick?.Invoke(Input.mousePosition);
    private void OnMouseDrag()  => onMouseDrag?.Invoke(Input.mousePosition);
    private void OnMouseUp()    => onMouseRelease?.Invoke(Input.mousePosition);
}
