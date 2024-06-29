using UnityEngine;

public class Highlighter : MonoBehaviour
{
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

    public void Tick(Vector2Int _input)
    {
        transform.position += new Vector3(_input.x, _input.y, 0f);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
