/*
using UnityEngine;

namespace Steering
{
    public class FollowPath : Behavior
    {
        public enum FPM { Forwards, Backwards, PingPong, Random }
        public enum PingPongMode { Forward, Backward }

        private Vector3[] m_wayPoints;

        private int currentIndex;
        private PingPongMode m_pingPongMode = PingPongMode.Forward;

        public FollowPath(Vector3[] wayPoints)
        {
            m_wayPoints = wayPoints;
        }

        public override Vector3 CalculateSteeringForce(float deltaTime, BehaviorContext context)
        {
            var currentTarget = m_wayPoints[currentIndex];
            var distanceFromTarget = Vector3.Distance(context.position, currentTarget);
            var targetReached = distanceFromTarget <= context.settings.arriveDistance;

            if (targetReached)
            {
                OnPointEnter(context);
            }

            SetTargetPosition(currentTarget, context);

            return TargetToSteeringForce(context);
        }

        public override void DrawGizmos(BehaviorContext context)
        {
            base.DrawGizmos(context);

            GizmoTools.DrawPath(m_wayPoints, context.settings.looping, Color.white);

            foreach (var position in m_wayPoints)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawWireSphere(position, context.settings.arriveDistance);
            }
        }

        private void OnPointEnter(BehaviorContext context)
        {
            switch (context.settings.followMode)
            {
                case FPM.Forwards:
                    {
                        currentIndex++;

                        if (currentIndex >= m_wayPoints.Length)
                        {
                            if (context.settings.looping)
                            {
                                currentIndex = 0;
                            }
                            else
                            {
                                currentIndex = m_wayPoints.Length - 1;
                            }
                        }
                    }
                    break;

                case FPM.Backwards:
                    {
                        currentIndex--;

                        if (currentIndex < 0)
                        {
                            if (context.settings.looping)
                            {
                                currentIndex = m_wayPoints.Length - 1;
                            }
                            else
                            {
                                currentIndex = 0;
                            }
                        }
                    }
                    break;

                case FPM.Random:
                    {
                        currentIndex = Random.Range(0, m_wayPoints.Length);
                    }
                    break;

                case FPM.PingPong:
                    {
                        switch (m_pingPongMode)
                        {
                            case PingPongMode.Forward:
                                {
                                    currentIndex++;

                                    if (currentIndex >= m_wayPoints.Length)
                                    {
                                        if (context.settings.looping)
                                        {
                                            currentIndex -= 2;
                                            m_pingPongMode = PingPongMode.Backward;
                                        }
                                        else
                                        {
                                            currentIndex = m_wayPoints.Length - 1;
                                        }
                                    }
                                }
                                break;

                            case PingPongMode.Backward:
                                {
                                    currentIndex--;

                                    if (currentIndex < 0)
                                    {
                                        if (context.settings.looping)
                                        {
                                            currentIndex += 2;
                                            m_pingPongMode = PingPongMode.Forward;
                                        }
                                        else
                                        {
                                            currentIndex = 0;
                                        }
                                    }
                                }
                                break;
                        }
                    }
                    break;
            }
        }
    }
}
*/