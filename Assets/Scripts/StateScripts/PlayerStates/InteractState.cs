using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StateScripts.PlayerStates
{
    public class InteractState : BaseState
    {
        public override void EnterState(PlayerStateMachine state, AgentController controller, WeaponItemSO weapon)
        {
            base.EnterState(state, controller, weapon);
            var usableStructure = controllerReference.DetectionSystem.UsableCollider;
            if (usableStructure != null)
            {
                usableStructure.GetComponent<IUsable>().Use();
                return;
            }
            var resultCollider = controllerReference.DetectionSystem.CurrentCollider;
            if (resultCollider != null)
            {
                var ipickable = resultCollider.GetComponent<IPickable>();
                var remainder = controllerReference.InventorySystem.AddToStorage(ipickable.PickUp());
                ipickable.SetCount(remainder);
                controllerReference.InteractionSound.PlayOneShotPickupItem();
                if (remainder > 0)
                {
                    Debug.Log("Can't pick it up");
                }
            }
        }

        public override void HandlePrimaryInput()
        {
            base.HandlePrimaryInput();
        }

        public override void HandleSecondaryClickInput()
        {
            base.HandleSecondaryClickInput();
        }

        public override void Update()
        {
            base.Update();
            stateMachine.TransitionToState(stateMachine.IdleState);
        }
    }
}