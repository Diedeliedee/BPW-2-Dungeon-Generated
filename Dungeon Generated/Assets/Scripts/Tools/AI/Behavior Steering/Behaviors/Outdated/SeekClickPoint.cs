/*
using UnityEngine;

namespace Steering
{
    public class SeekClickPoint : Behavior
    {
        public override Vector3 CalculateSteeringForce(float deltaTime, BehaviorContext context)
        {
            if (Input.GetMouseButton(0))
            {
                var cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);  //  Ray pointing to the clicked point.
                var maxDistance = 100f;                                             //  Max distance of the camera ray.

                //  If the ray hits something, the X and Y position of the hit point will become the new target position.
                if (Physics.Raycast(cameraRay, out RaycastHit hitInfo, maxDistance))
                {
                    SetTargetPosition(hitInfo.point, context);
                }
            }

            return TargetToSteeringForce(context);
        }

        public override void DrawGizmos(BehaviorContext context)
        {
            base.DrawGizmos(context);

            if (ArriveEnabled(context))
            {
                OnDrawArriveGizmos(context);
            }
        }
    }
}
*/