using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools.Utilities;
using Joeri.Tools.AI.Steering;

namespace Joeri.Tools.Movement.ThreeDee
{
    public class AgentController : ThreeDeeMovement
    {
        private BehaviorHandler m_behaviorHandler = new BehaviorHandler();

        public AgentController(GameObject root, Settings settings) : base(root, settings) { }

        /// <summary>
        /// Apply the passed in behaviors to the controller's behavior handler.
        /// </summary>
        public void SetBehaviors(params Behavior[] behaviors)
        {
            m_behaviorHandler.SetBehaviors(behaviors);
        }

        /// <summary>
        /// Clears the behaviors set in the controller's behavior handler.
        /// </summary>
        public void ClearBehaviors()
        {
            m_behaviorHandler.ClearBehaviors();
        }

        public void ApplyBehaviorVelocity(float deltaTime)
        {
            var context = new Context(deltaTime, speed, controller.transform.position.Planar(), flatVelocity);
            var desiredVelocity = m_behaviorHandler.GetDesiredVelocity(context);

            ApplyDesiredVelocity(desiredVelocity, deltaTime);
        }

        public override void DrawGizmos()
        {
            base.DrawGizmos();
            m_behaviorHandler.DrawGizmos(controller.transform.position);
        }
    }
}
