using System;
using UnityEngine;

namespace Joeri.Tools.Gameify
{
    [System.Serializable]
    public class Health
    {
        [SerializeField] private int m_maxHealth    = 10;
        [SerializeField] private int m_health       = 10;
    
        //  Events:
        public Action onDeath                   = null; 
        public Action<int, int> onHealthChange  = null;

        //  Properties:
        public int health       { get => m_health; }
        public int maxHealth    { get => m_maxHealth; }
    
        public int SetMaxHealth(int maxHealth, bool heal)
        {
            m_maxHealth = maxHealth;
            if (heal) m_health = maxHealth;
            return maxHealth;
        }
    
        public int SetHealth(int health)
        {
            m_health = Mathf.Clamp(health, 0, m_maxHealth);
            onHealthChange?.Invoke(m_health, m_maxHealth);
            if (m_health == 0) onDeath?.Invoke();
            return health;
        }
    
        public int AddHealth(int health)
        {
            return SetHealth(m_health + health);
        }
    }
}
