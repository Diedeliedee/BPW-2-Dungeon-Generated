using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    [SerializeField] private PlayerControl  m_playerControl;
    [SerializeField] private EnemyControl   m_enemyControl;

    private ControlModule m_activeModule    = null;
    private TurnHandler m_turnHandler       = new TurnHandler();

    public void Setup()
    {
        m_playerControl .Setup(m_turnHandler);
        m_enemyControl  .Setup(m_turnHandler);
    }

    public void Tick()
    {
        m_activeModule?.Tick();
    }

    public void StartControlLoop()
    {
        m_activeModule = m_playerControl;
        m_activeModule.Activate(OnPlayerControlFinish);
    }

    private void OnPlayerControlFinish()
    {
        //  Give enemy AI control if enemies have entered combat.
        if (m_enemyControl.enemyCount > 0)
        {
            m_activeModule = m_enemyControl;
            m_activeModule.Activate(OnEnemyControlFinish);
            //  Initiate combat type stuff?
            return;
        }

        //  If not, keep giving player control.
        m_activeModule = m_playerControl;
        m_activeModule.Activate(OnPlayerControlFinish);
    }

    private void OnEnemyControlFinish()
    {
        m_activeModule = m_playerControl;
        m_activeModule.Activate(OnPlayerControlFinish);
    }
}
