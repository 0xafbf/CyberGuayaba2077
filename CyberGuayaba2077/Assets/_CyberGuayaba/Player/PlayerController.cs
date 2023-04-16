using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerMovement _playerMovement;
    PlayerAnimsController _playerAnims;

    PlayerStates _currState;

    public Action<PlayerStates, PlayerStates> OnStateChanged;

    void OnValidate()
    {
        if (!_playerMovement) TryGetComponent(out _playerMovement);
        if (!_playerAnims) _playerAnims = GetComponentInChildren<PlayerAnimsController>();
       
    }

    void Update()
    {
        RefreshPlayerState();
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
}

public enum PlayerStates
{
    Idle,
    Moving,

}
