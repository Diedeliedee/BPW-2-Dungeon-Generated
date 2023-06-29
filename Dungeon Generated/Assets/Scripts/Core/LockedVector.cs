using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public struct LockedVector
{
    public Axis axis;
    public int value;

    public LockedVector normalized { get => new LockedVector(axis, Mathf.Clamp(value, -1, 1)); }

    public static Vector2Int[] validDirections = new Vector2Int[]
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };

    public LockedVector(Axis axis, int value)
    {
        this.axis   = axis;
        this.value  = value;
    }

    public LockedVector(Vector2Int vector)
    {
        if (!IsValidVector(vector)) vector.y = 0;

        axis    = VectorToAxis(vector);
        value   = VectorToValue(vector);
    }

    public LockedVector(Vector2 vector)
    {
        if (Mathf.Abs(vector.y) > Mathf.Abs(vector.x))
        {
            axis    = Axis.Vertical;
            value   = Mathf.FloorToInt(vector.y);
        }
        axis    = Axis.Horizontal;
        value   = Mathf.FloorToInt(vector.x);
    }

    public LockedVector Add(int amount)
    {
        return new LockedVector(axis, value + amount);
    }

    public static LockedVector operator ++(LockedVector a) => new LockedVector(a.axis, ++a.value);
    public static LockedVector operator --(LockedVector a) => new LockedVector(a.axis, --a.value);
    public static LockedVector operator +(LockedVector a, int amount) => new LockedVector(a.axis, a.value + amount);
    public static LockedVector operator -(LockedVector a, int amount) => new LockedVector(a.axis, a.value - amount);
    public static LockedVector operator *(LockedVector a, int amount) => new LockedVector(a.axis, a.value * amount);
    public static LockedVector operator /(LockedVector a, int amount) => new LockedVector(a.axis, a.value / amount);

    public Vector2Int ToVector()
    {
        switch (axis)
        {
            case Axis.Horizontal:   return new Vector2Int(value, 0);
            case Axis.Vertical:     return new Vector2Int(0, value);
        }
        return Vector2Int.zero;
    }

    public static Axis VectorToAxis(Vector2Int vector)
    {
        if (vector.y == 0) return Axis.Horizontal;
        if (vector.x == 0) return Axis.Vertical;

        return Axis.Horizontal;
    }

    public static int VectorToValue(Vector2Int vector)
    {
        switch (VectorToAxis(vector))
        {
            case Axis.Horizontal:   return vector.x;
            case Axis.Vertical:     return vector.y;
        }
        return 0;
    }

    public static bool IsValidVector(Vector2Int dir)
    {
        dir = new Vector2Int(Mathf.Clamp(dir.x, -1, 1), Mathf.Clamp(dir.y, -1, 1));

        for (int i = 0; i < validDirections.Length; i++) if (dir == validDirections[i]) return true;
        return false;
    }

    public enum Axis
    {
        Horizontal,
        Vertical,
    }

    public enum Direction
    {
        None,
        Up,
        Down,
        Left,
        Right,
    }
}
