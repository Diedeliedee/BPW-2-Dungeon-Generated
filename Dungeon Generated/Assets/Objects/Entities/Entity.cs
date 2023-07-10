using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Joeri.Tools.Structure;
using Joeri.Tools.Gameify;

public abstract partial class Entity : MonoBehaviour
{
    [Header("Sub-components:")]
    [SerializeField] protected Health m_health;

    [Header("Entity Properties:")]
    [SerializeField] private AudioClip m_damageAudio;
    [SerializeField] private AudioClip m_deathAudio;

    //  Run-time:
    protected Tile m_currentTile = null;
    protected Vector2Int m_coordinates;

    //  Cahce:
    private AudioSource m_audio = null;

    #region Properties
    public Vector2Int coordinates
    {
        get => m_coordinates;
        set
        {
            var pos = Dungeon.CoordsToPos(value);

            m_coordinates       = value;
            transform.position  = new Vector3(pos.x, value.y + 0.5f, transform.position.z);
        }
    }
    public Vector2 position
    {
        get => transform.position;
        set
        {
            m_coordinates       = Dungeon.PosToCoords(value);
            transform.position  = new Vector3(value.x, value.y, transform.position.z);
        }
    }

    private EventManager events { get => GameManager.instance.events; }
    #endregion

    public virtual void Setup()
    {
        //  Snap the entity to the grid by moving it to the current tile.
        Move(Vector2Int.zero);

        //  Get AudioSource.
        m_audio = GetComponent<AudioSource>();

        //  Register events.
        m_health.onDeath += OnDeath;
    }

    /// <summary>
    /// Moves the entity with the passed in offset.
    /// </summary>
    protected void Move(Vector2Int offset)
    {
        if (!GameManager.instance.dungeon.HasTile(coordinates + offset, out Tile tile)) return;

        coordinates     += offset;
        m_currentTile   = tile;
    }

    public virtual void Damage(int amount)
    {
        if (amount <= 0) return;

        PlaySound(m_damageAudio, 1f, 1f);
        m_health.AddHealth(-amount);
    }

    public virtual void OnDeath()
    {
        PlaySound(m_deathAudio, 1f, 1f);
    }

    public void PlaySound(AudioClip clip, float volume, float pitch = 1f)
    {
        if (m_audio == null || clip == null) return;
        m_audio.clip    = clip;
        m_audio.volume  = volume;
        m_audio.pitch   = pitch;
        m_audio.Play();
    }

    public void StopSound()
    {
        m_audio.clip    = null;
        m_audio.volume  = 1f;
        m_audio.pitch   = 1f;
    }
}
