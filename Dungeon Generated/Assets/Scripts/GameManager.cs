using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Room[] rooms = new Room[1];
    public Room activeRoom = null;
    public Player player = null;

    private void Start()
    {
        //activeRoom = rooms[0];
        player.Initialize();
    }

    private void Update()
    {
        //activeRoom.Tick(Time.deltaTime);
        player.Tick(Time.deltaTime);
    }
}
