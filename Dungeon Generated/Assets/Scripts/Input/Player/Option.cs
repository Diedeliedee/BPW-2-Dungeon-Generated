using System;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerInput
{
    [Header("Different Options:")]
    [SerializeField] private MovementDrag m_movement;

    public Dictionary<Type, Option> GetOptions()
    {
        var dic = new Dictionary<Type, Option>
        {
            { typeof(Player), m_movement }
        };

        return dic;
    }

    public abstract class Option
    {
        public abstract void OnClick(Vector2 mousePos);

        public abstract void OnDrag(Vector2 mousePos);

        public abstract void OnRelease(Vector2 mousePos);
    }
}
