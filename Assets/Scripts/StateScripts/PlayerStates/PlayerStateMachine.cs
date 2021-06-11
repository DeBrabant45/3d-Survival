using System.Collections;
using UnityEngine;

namespace Assets.Scripts.StateScripts.PlayerStates
{
    public class PlayerStateMachine : MonoBehaviour
    {
        private BaseState _previousState;
        private BaseState _currentState;
        private AgentController _agentController;

        public readonly BaseState JumpState = new JumpState();
        public readonly BaseState FallingState = new FallingState();

        public readonly BaseState InventoryState = new InventoryState();
        public readonly BaseState IdleState = new IdleState();
        public readonly BaseState InteractState = new InteractState();
        public readonly BaseState MenuState = new MenuState();
        public readonly BaseState PlacementState = new PlacementState();

        public readonly BaseState MeleeUnarmedAttackState = new MeleeUnarmedAttackState();
        public readonly BaseState MeleeWeaponAttackState = new MeleeWeaponAttackState();
        public readonly BaseState RangedWeaponAttackState = new RangedAttackState();

        public readonly BaseState MeleeWeaponAttackStanceState = new MeleeWeaponAttackStanceState();
        public readonly BaseState RangedWeaponAttackStanceState = new RangedWeaponAttackStanceState();

        public readonly BaseState RangedWeaponAimState = new RangedWeaponAimState();
        public readonly BaseState ReloadRangedWeaponState = new ReloadRangedWeaponState();

        public readonly BaseState EquipItemState = new EquipItemState();
        public readonly BaseState UnequipItemState = new UnequipItemState();

        public readonly BaseState BlockStanceState = new BlockStanceState();
        public readonly BaseState BlockReactionState = new BlockReactionState();
        public readonly BaseState HurtState = new HurtState();

        public BaseState PreviousState { get => _previousState; }

        private void Awake()
        {
            _agentController = GetComponent<AgentController>();
            _currentState = IdleState;
            _currentState.EnterState(this, _agentController, _agentController.EquippedItem);
        }

        private void Start()
        {
            AssignInputListeners();
        }

        private void Update()
        {
            if (Time.timeScale == 0)
            {
                return;
            }
            _currentState.Update();
        }

        public void TransitionToState(BaseState state)
        {
            _previousState = _currentState;
            //Debug.Log(_previousState + " old State");
            _currentState = state;
            _currentState.EnterState(this, _agentController, _agentController.EquippedItem);
            //Debug.Log(_currentState + " new State");
        }

        private void AssignInputListeners()
        {
            _agentController.InputFromPlayer.OnJump += HandleJump;
            _agentController.InputFromPlayer.OnHotBarKey += HandleHotBarInput;
            _agentController.InputFromPlayer.OnToggleInventory += HandleInventoryInput;
            _agentController.InputFromPlayer.OnPrimaryAction += HandlePrimaryInput;
            _agentController.InputFromPlayer.OnSecondaryClickAction += HandleSecondaryClickInput;
            _agentController.InputFromPlayer.OnMenuToggledKey += HandleMenuInput;
            _agentController.InputFromPlayer.OnReload += HandleReloadInput;
            _agentController.InputFromPlayer.OnAim += HandleAimInput;
            _agentController.InputFromPlayer.OnSecondaryHeldDownAction += HandleSecondaryHeldDownInput;
            _agentController.InputFromPlayer.OnSecondaryUpAction += HandleSecondaryUpInput;
            _agentController.InventorySystem.OnStructureUse += HandlePlacementInput;
        }

        private void OnDisable()
        {
            _agentController.InputFromPlayer.OnJump -= _currentState.HandleJumpInput;
        }

        private void HandleSecondaryUpInput()
        {
            _currentState.HandleSecondaryUpInput();
        }

        private void HandleSecondaryHeldDownInput()
        {
            _currentState.HandleSecondaryHeldDownInput();
        }

        private void HandleAimInput()
        {
            _currentState.HandleEquipItemInput();
        }

        private void HandleMenuInput()
        {
            _currentState.HandleMenuInput();
        }

        private void HandleSecondaryClickInput()
        {
            _currentState.HandleSecondaryClickInput();
        }

        private void HandlePrimaryInput()
        {
            _currentState.HandlePrimaryInput();
        }

        private void HandleJump()
        {
            _currentState.HandleJumpInput();
        }

        private void HandleInventoryInput()
        {
            _currentState.HandleInventoryInput();
        }

        private void HandleHotBarInput(int hotBarKey)
        {
            _currentState.HandleHotBarInput(hotBarKey);
        }

        private void HandleReloadInput()
        {
            _currentState.HandleReloadInput();
        }

        private void HandlePlacementInput()
        {
            _currentState.HandlePlacementInput();
        }
    }
}