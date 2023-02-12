using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Room[] rooms     = new Room[1];
    public Room activeRoom  = null;
    public Player player    = null;
    public DungeonGenerator generator = null;

    private void Start()
    {
        player.Initialize();
        activeRoom = rooms[0];
    }

    private void Update()
    {
        player.Tick(Time.deltaTime);
        activeRoom.Tick(Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        generator.DrawGizmos();
    }
}
