using UnityEngine;

public class SelfMemory
{
    public Transform transform = null;
    public Vector3 position = default;
    public Quaternion rotation = default;
    public Vector3 velocity = default;

    public void Update(Transform _transform, Vector3 _velocity = default)
    {
        transform = _transform;
        position = _transform.position;
        rotation = _transform.rotation;
        velocity = _velocity;
    }
}
