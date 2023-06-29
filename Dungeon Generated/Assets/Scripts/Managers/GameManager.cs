using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Structure;

public class GameManager : Singleton<GameManager>
{
    [Header("References:")]
    public Dungeon dungeon;
    public PlayerInstance player;
    public Camera camera;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        dungeon.Setup();
        player.Setup();
    }

    private void Update()
    {
        
    }
}
