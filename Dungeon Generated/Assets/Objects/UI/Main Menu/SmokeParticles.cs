using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeParticles : MonoBehaviour
{
    [Header("Properties:")]
    [SerializeField] private int m_amount = 3;
    [SerializeField] private float m_rotationSpeed = 3;
    [Space]
    [SerializeField] private float m_maxHeight;
    [SerializeField] private AnimationCurve m_scaleCurve;
    [SerializeField] private float m_minSize, m_maxSize;

    [Header("Reference:")]
    [SerializeField] private GameObject m_smokePrefab;
    [SerializeField] private Canvas m_canvas;

    private List<RectTransform> m_particles = new List<RectTransform>();

    private void Start()
    {
        for (int i = 0; i < m_amount; i++) m_particles.Add(GetSmoke());
    }

    private void Update()
    {
        foreach (var particle in m_particles)
        {
            var addition = m_rotationSpeed * 360f * Time.unscaledDeltaTime;

            particle.localEulerAngles = GetRotation(particle, particle.localEulerAngles.z + addition);
        }
    }

    private RectTransform GetSmoke()
    {
        var position        = new Vector2(Random.Range(0, m_canvas.pixelRect.width), Random.Range(0, m_maxHeight));
        var size            = Random.Range(m_minSize, m_maxSize);
        var rectTransform   = Instantiate(m_smokePrefab, transform).GetComponent<RectTransform>();

        rectTransform.anchoredPosition  = position;
        rectTransform.localEulerAngles  = GetRotation(rectTransform, Random.Range(0f, 360f));
        rectTransform.sizeDelta         = new Vector2(size, size);
        return rectTransform;
    }

    public Vector3 GetRotation(RectTransform transform, float angle)
    {
        return new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, angle);
    }
}
