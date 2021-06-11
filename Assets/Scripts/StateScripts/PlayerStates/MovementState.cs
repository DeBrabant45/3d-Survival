using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StateScripts.PlayerStates
{
    public abstract class MovementState : BaseState
    {
        protected float _defaultFallingDelay = 0.2f;
        protected float _fallingDelay = 0;

        public override void EnterState(PlayerStateMachine state, AgentController controller, WeaponItemSO weapon)
        {
            base.EnterState(state, controller, weapon);
            _fallingDelay = _defaultFallingDelay;
        }

        public override void HandleCameraDirection(Vector3 input)
        {
            base.HandleCameraDirection(input);
            controllerReference.Movement.HandleMovementDirection(input);
        }

        public override void HandleMovement(Vector2 input)
        {
            base.HandleMovement(input);
            controllerReference.Movement.HandleMovement(input);
        }

        public override void HandleJumpInput()
        {
            stateMachine.TransitionToState(stateMachine.JumpState);
        }

        public override void Update()
        {
            base.Update();
            PreformDetection();
            HandleMovement(controllerReference.InputFromPlayer.MovementInputVector);
            HandleCameraDirection(controllerReference.InputFromPlayer.MovementDirectionVector);
            HandleFallingDown();
        }

        protected void HandleFallingDown()
        {
            if (controllerReference.Movement.CharacterIsGrounded() == false)
            {
                if (_fallingDelay > 0)
                {
                    _fallingDelay -= Time.deltaTime;
                    return;
                }
                stateMachine.TransitionToState(stateMachine.FallingState);
            }
            else
            {
                _fallingDelay = _defaultFallingDelay;
            }
        }

        private void PreformDetection()
        {
            controllerReference.DetectionSystem.PreformDetection(controllerReference.InputFromPlayer.MovementDirectionVector);
        }
    }
}
