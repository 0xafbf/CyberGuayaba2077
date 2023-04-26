using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerMovement _playerMovement;
    PlayerAnimsController _playerAnims;
    [SerializeField]
    CollisionControler _detectionZone;
    StrenghtSender _strengthSender;
    StrenghtController _strenghtController;

    //TODO: It shouldn't be necesary to have this reference.
    StrenghtReceiver _currStrenghtReceiverTarget;

    PlayerStates _currState;

    public Action<PlayerStates, PlayerStates> OnStateChanged;

    void OnValidate()
    {
        if (!_playerMovement) TryGetComponent(out _playerMovement);
        if (!_strengthSender) TryGetComponent(out _strengthSender);
        if (!_strenghtController) TryGetComponent(out _strenghtController);
        if (!_playerAnims) _playerAnims = GetComponentInChildren<PlayerAnimsController>();
    }

	private void Start()
	{
        _strengthSender.onStrenght += (x) => _strengthSender.SendForce(_currStrenghtReceiverTarget, x);
	}

	void Update()
    {
        RefreshPlayerState();
		if (Input.GetKeyDown(KeyCode.Space))
		{
            TryToInteractWithCloserObj();
		}
    }

    public void RefreshPlayerState()
	{
        if (_playerMovement.IsMoving)
        {
            SetState(PlayerStates.Moving);
            return;
        }
        SetState(PlayerStates.Idle);
	}

    private void SetState(PlayerStates newState)
	{
        if (newState == _currState) return;
        OnStateChanged?.Invoke(_currState, newState);
        _playerAnims.OnPlayerStateChanged(_currState, newState);
        _currState = newState;
	}

    private void TryToInteractWithCloserObj()
	{
        var availableObjs = _detectionZone.currFrameCollisions;
		for (int i = 0; i < availableObjs.Count; i++)
		{
            var curr = availableObjs[i];
            if (curr.gameObject == _detectionZone.gameObject) continue;
            if (curr.gameObject == gameObject) continue;
            if (!curr.gameObject.activeInHierarchy) continue;
            _currStrenghtReceiverTarget = curr.GetComponentInParent<StrenghtReceiver>();
            if (_currStrenghtReceiverTarget == null) continue;
            _strenghtController.Init(_currStrenghtReceiverTarget);
            return; //Exit after first sucessfull interaction
		}
	}
}

public enum PlayerStates
{
    Idle,
    Moving,

}
