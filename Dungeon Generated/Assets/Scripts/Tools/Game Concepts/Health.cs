using UnityEngine;

namespace Joeri.Tools.Gameify
{
    [System.Serializable]
    public class Health
    {
        [SerializeField] private int m_maxHealth    = 10;
        [SerializeField] private int m_health       = 10;
    
        //  Events:
        public System.Action onDeath                    = null; 
        public System.Action<int, int> onHealthChange   = null;
        public System.Action<int, int> onDamage         = null;
        public System.Action<int, int> onHeal           = null;

        //  Properties:
        public int health       => m_health;
        public int maxHealth    => m_maxHealth;
        public float percentage => (float)m_health / m_maxHealth;
    
        public int SetMaxHealth(int maxHealth)
        {
            m_maxHealth = maxHealth;
            return maxHealth;
        }
    
        public int SetHealth(int health)
        {
            var difference = health - m_health;

            //  Check for difference, to differentiate what kind of health change this is.
            if (difference == 0)        return m_health;
            else if (difference < 0)    onDamage?.Invoke(-difference, m_maxHealth);
            else if (difference > 0)    onHeal?.Invoke(difference, m_maxHealth);

            //  Adjust the health field.
            m_health = Mathf.Clamp(health, 0, m_maxHealth);

            //  Call event.
            onHealthChange?.Invoke(m_health, m_maxHealth);

            //  Call death event if necessary.
            if (m_health == 0) onDeath?.Invoke();

            return health;
        }
    
        public int ChangeHealth(int health)
        {
            return SetHealth(m_health + health);
        }
    }
}
