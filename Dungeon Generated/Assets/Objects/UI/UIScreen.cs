using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class UIScreen : MonoBehaviour
{
    protected Animator m_animator = null;

    public virtual void Setup()
    {
        m_animator = GetComponent<Animator>();
    }

    public abstract void Activate();
}