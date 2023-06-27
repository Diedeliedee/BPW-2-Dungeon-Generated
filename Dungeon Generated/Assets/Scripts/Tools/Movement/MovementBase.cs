using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools.Utilities;
using Joeri.Tools.Debugging;

namespace Joeri.Tools.Movement
{
    public class MovementBase
    {
        //  Components:
        protected Accel.Flat m_horizontal = new Accel.Flat();
        protected Accel.Uncontrolled m_vertical = new Accel.Uncontrolled(0f, 0f, 0f);

        //  Run-time:
        protected bool m_onGround = false;
        protected Vector3 m_lastGroundedPos = Vector3.zero;

        private float m_rotationVelocity = 0f;

        // Reference:
        private LayerMask m_movementMask;

        #region Properties

        //  Movement Properties:
        public float speed { get; set; }
        public float grip { get; set; }
        public float gravity
        {
            get => m_vertical.acceleration;
            set => m_vertical.acceleration = value;
        }

        //  Rotation Properties:
        public float rotationTime { get; set; }
        public bool canRotate { get; set; }


        //  Run-time data:
        public Vector3 velocity
        {
            get => new Vector3(m_horizontal.velocity.x, m_vertical.velocity, m_horizontal.velocity.y);
            set
            {
                m_horizontal.velocity.x = value.x;
                m_horizontal.velocity.y = value.z;
                m_vertical.velocity = value.y;
            }
        }
        public Vector2 flatVelocity
        {
            get => m_horizontal.velocity;
            set => m_horizontal.velocity = value;
        }
        public float verticalVelocity
        {
            get => m_vertical.velocity;
            set => m_vertical.velocity = value;
        }

        public bool onGround { get => m_onGround; }
        public Vector3 lastGroundedPosition { get => m_lastGroundedPos; }

        //  Reference:
        public CharacterController controller { get; private set; }

        //  Utilities:
        protected Vector3 lowerOrbCenter
        {
            get
            {
                var position = controller.transform.position + controller.center;

                position += Vector3.down * controller.height / 2;   //  Go to the bottom of the controller's collider.
                position += Vector3.up * controller.radius;         //  Set the center so that the overlap slightly reaches under the collider.
                return position;
            }
        }

        protected Vector3 groundCheckOrigin { get => lowerOrbCenter + Vector3.down * controller.skinWidth * 2; }

        #endregion

        public MovementBase(GameObject root, Settings settings)
        {
            controller = root.GetComponent<CharacterController>();

            if (controller == null)
            {
                Debug.LogError($"No character controller found on agent: {root.name}.", root);
                return;
            }

            speed = settings.baseSpeed;
            grip = settings.baseGrip;
            gravity = settings.baseGravity;
            rotationTime = settings.baseRotationTime;
            canRotate = settings.baseRotationTime < Mathf.Infinity;

            m_movementMask = settings.movementMask;

            m_lastGroundedPos = root.transform.position;
        }

        /// <summary>
        /// Iterates the velocity based on the passed in desired velocity.
        /// </summary>
        public void ApplyDesiredVelocity(Vector2 desiredVelocity, float deltaTime)
        {
            m_horizontal.CalculateVelocity(desiredVelocity, grip, deltaTime);
            m_vertical.CalculateVelocity(deltaTime);

            if (canRotate && flatVelocity != Vector2.zero) RotateToVelocity(deltaTime);

            ApplyVelocity(deltaTime);
        }

        /// <summary>
        /// Iterates the velocity to stand still with passed in drag during deceleration.
        /// </summary>
        public void ApplyDrag(float drag, float deltaTime)
        {
            m_horizontal.CalculateVelocity(drag, deltaTime);
            m_vertical.CalculateVelocity(deltaTime);

            if (canRotate && flatVelocity != Vector2.zero) RotateToVelocity(deltaTime);

            ApplyVelocity(deltaTime);
        }

        /// <summary>
        /// Rotates the transform bearing the character controller to face the passed in direction.
        /// </summary>
        public void RotateToDir(Vector2 dir, float deltaTime)
        {
            RotateToAngle(Vectors.VectorToAngle(dir), deltaTime);
        }

        /// <summary>
        /// Rotates the transform bearing the character controller to the passed in angle.
        /// </summary>
        public void RotateToAngle(float angle, float deltaTime)
        {
            var currentAngle = controller.transform.eulerAngles.y;
            var desiredAngle = angle % 360f;

            if (rotationTime > 0)
            {
                currentAngle = Mathf.SmoothDampAngle(currentAngle, desiredAngle, ref m_rotationVelocity, rotationTime, Mathf.Infinity, deltaTime);
            }
            else
            {
                currentAngle = desiredAngle;
            }
            controller.transform.eulerAngles = new Vector3(controller.transform.eulerAngles.x, currentAngle, controller.transform.eulerAngles.z);
        }

        /// <summary>
        /// Applies the velocity of the of the two acceleration classes to the character controller.
        /// </summary>
        private void ApplyVelocity(float deltaTime)
        {
            if (canRotate && flatVelocity != Vector2.zero) RotateToVelocity(deltaTime);
            controller.Move(velocity * deltaTime);
            if (IsOnGround() && ShouldStepDown()) controller.Move(Vector3.down * controller.stepOffset);
            m_onGround = IsOnGround();
        }

        /// <summary>
        /// Rotates the transform bearing the character controller to the direction of the velocity, with 
        /// </summary>
        private void RotateToVelocity(float deltaTime)
        {
            RotateToDir(flatVelocity, deltaTime);
        }

        /// <returns>True if the player is standing on valid ground. Calculated by a Physics.OverlapSphere(...).</returns>
        public bool IsOnGround()
        {
            if (controller == null) return false;

            var overlappingColliders = Physics.OverlapSphere(groundCheckOrigin, controller.radius, m_movementMask, QueryTriggerInteraction.Ignore);

            if (overlappingColliders.Length > 0)
            {
                m_lastGroundedPos = controller.transform.position;
                return true;
            }
            return false;
        }

        public bool ShouldStepDown()
        {
            if (!m_onGround) return false;              //  Return if the player was not standing on ground the previous frame.
            if (verticalVelocity > 0f) return false;    //  Return if the player is moving upward.
            if (!Physics.Raycast(controller.transform.position, Vector3.down, out RaycastHit hit, controller.stepOffset, m_movementMask, QueryTriggerInteraction.Ignore)) return false;
            return true;
        }

        /// <summary>
        /// Draws the functionality of the movement class as shapes in 3D space. Representing their values and states.
        /// </summary>
        public virtual void DrawGizmos()
        {
            GizmoTools.DrawSphere(groundCheckOrigin, controller.radius, onGround ? Color.white : Color.black, 0.5f, true, 0.75f);
            m_horizontal.Draw(controller.transform.position, Color.blue, Color.black, 0.75f);
            m_vertical.Draw(controller.transform.position, Vector3.up, Color.green, 0.5f);
        }

        [System.Serializable]
        public class Settings
        {
            [Min(0f)] public float baseSpeed;
            [Min(0f)] public float baseGrip;
            public float baseGravity;
            [Space]
            [Min(0f)] public float baseRotationTime;
            [Space]
            public LayerMask movementMask;
        }
    }
}
