using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools.Structure;

public class CameraManager : MonoBehaviour
{
    [Header("Properties:")]
    [SerializeField] private AnimationCurve m_movementCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    [SerializeField] [Min(0f)] private float m_timeToLerp   = 1f;

    private Camera m_camera             = null;
    private Coroutine m_movementRoutine = null;

    public Camera camera { get => m_camera; }

    public void Setup()
    {
        m_camera = GetComponentInChildren<Camera>();

        GameManager.instance.events.onTurnStart += MoveToCharacter;
    }

    public void MoveToCharacter(Character character)
    {
        MoveTo(character.coordinates);
    }

    public void MoveTo(Vector2Int coordinates)
    {
        var startPosition   = transform.position;
        var endPosition     = Dungeon.CoordsToPos(coordinates);

        void OnTick(float progress)
        {
            transform.position = Vector2.Lerp(startPosition, endPosition, progress);
            //transform.position = new Vector3(position.x, position.y, transform.position.z);
        }

        void OnFinish()
        {
            transform.position  = endPosition;
            m_movementRoutine   = null;
        }

        if (m_movementRoutine != null)
        {
            StopCoroutine(m_movementRoutine);
        }
        StartCoroutine(Routines.CustomProgression(m_timeToLerp, m_movementCurve, OnTick, OnFinish));
    }
}
