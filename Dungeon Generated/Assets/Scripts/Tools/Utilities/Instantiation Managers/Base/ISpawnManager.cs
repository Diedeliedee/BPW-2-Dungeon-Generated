using UnityEngine;

namespace Joeri.Tools.Utilities.SpawnManager
{
    /// <summary>
    /// Interface for a class responsible for spawning, and managing a collection of different, but similair types of objects in the scene.
    /// </summary>
    public partial interface ISpawnManager
    {
        public GameObject Spawn(string _type, Vector3 _position, Quaternion _rotation);

        public T Spawn<T>(string _type, Vector3 _position, Quaternion _rotation);
    }
}
