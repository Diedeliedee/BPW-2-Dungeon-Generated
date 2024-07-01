using UnityEngine;

public class Highlighter : MonoBehaviour
{
    [SerializeField] private int m_maxRange = 3;

    public Vector2Int coordinates
    {
        get => new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y));
        set => transform.position = new Vector3(value.x, value.y, transform.position.z);
    }

    public void Activate(Vector2Int _start)
    {
        gameObject.SetActive(true);
        transform.position = new Vector3(_start.x, _start.y, 0f);
    }

    public void Tick(Vector2Int _playerPos, Vector2Int _input)
    {
        var newCoords = coordinates + _input;

        if (Vector2Int.Distance(_playerPos, newCoords) > m_maxRange) return;
        coordinates = newCoords;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
