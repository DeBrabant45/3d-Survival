using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StateScripts.PlayerStates
{
    public class PlacementState : MovementState
    {
        private PlacementHelper _placementHelper;
        public override void EnterState(PlayerStateMachine state, AgentController controller, WeaponItemSO weapon)
        {
            base.EnterState(state, controller, weapon);
            CreateStructureToPlace();
        }

        private void CreateStructureToPlace()
        {
            _placementHelper = ItemSpawnManager.Instance.CreateStructure(controllerReference.InventorySystem.SelectedStructureData);
            _placementHelper.PrepareForMovement();
            Debug.Log("Creating a structure to place");
        }

        public override void HandlePrimaryInput()
        {
            if (_placementHelper.CorrectLocation)
            {
                var structureComponent = _placementHelper.PrepareForPlacement();
                structureComponent.SetData(controllerReference.InventorySystem.SelectedStructureData);
                _placementHelper.enabled = false;
                controllerReference.InventorySystem.RemoveSelectedStructureFromInventory();
                controllerReference.BuildingPlacementStorage.SaveStructureReference(structureComponent);
                HandleSecondaryClickInput();
            }
        }

        public override void HandleSecondaryClickInput()
        {
            Debug.Log("Existing Placement State");
            if (_placementHelper.isActiveAndEnabled)
            {
                DestroyPlacedObject();
            }
            stateMachine.TransitionToState(stateMachine.IdleState);
        }

        private void DestroyPlacedObject()
        {
            Debug.Log("Destroying placed object");
            _placementHelper.DestroyStructure();
        }

        public override void HandleMenuInput()
        {
            stateMachine.TransitionToState(stateMachine.MenuState);
        }
    }
}
