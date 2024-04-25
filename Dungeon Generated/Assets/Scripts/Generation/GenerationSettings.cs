
using UnityEngine;

[System.Serializable]
public struct GenerationSettings
{
    [Header("Apparent:")]
    public int rooms;
    [Space]
    public int minRoomWidth;
    public int maxRoomWidth;
    [Space]
    public int minRoomHeight;
    public int maxRoomHeight;

    [Header("Back-end")]
    public float circleRadius;
    public float averageSizeMulitplier;
}