using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementState : MovementState
{
    private PlacementHelper _placementHelper;
    public override void EnterState(AgentController controller, WeaponItemSO weapon)
    {
        base.EnterState(controller, weapon);
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
        if(_placementHelper.CorrectLocation)
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
        if(_placementHelper.isActiveAndEnabled)
        {
            DestroyPlacedObject();
        }
        controllerReference.TransitionToState(controllerReference.movementState);
    }

    private void DestroyPlacedObject()
    {
        Debug.Log("Destroying placed object");
        _placementHelper.DestroyStructure();
    }

    public override void HandleMenuInput()
    {
        base.HandleMenuInput();
    }

    public override void Update()
    {
        HandleMovement(controllerReference.InputFromPlayer.MovementInputVector);
        HandleCameraDirection(controllerReference.InputFromPlayer.MovementDirectionVector);
        HandleFallingDown();
    }

    protected new void HandleFallingDown()
    {
        if (controllerReference.Movement.CharacterIsGrounded() == false)
        {
            if (_fallingDelay > 0)
            {
                _fallingDelay -= Time.deltaTime;
                return;
            }
            DestroyPlacedObject();
            controllerReference.TransitionToState(controllerReference.fallingState);
        }
        else
        {
            _fallingDelay = _defaultFallingDelay;
        }
    }

    #region InputOverrides
    public override void HandleEquipItemInput() { }
    public override void HandleJumpInput() { }
    public override void HandleInventoryInput() { }
    public override void HandleReloadInput() { }
    public override void HandleHotBarInput(int hotbarKey) { }
    public override void HandlePlacementInput() { }
    #endregion
}
