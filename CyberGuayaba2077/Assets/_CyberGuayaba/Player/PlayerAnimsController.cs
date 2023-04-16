using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimsController : MonoBehaviour
{
	private int _movAnim = Animator.StringToHash("Mov");
	private int _idleAnim = Animator.StringToHash("Idle");

	private Animator _animator;

	private void OnValidate()
	{
		if (!_animator) TryGetComponent(out _animator);
	}


	public void OnPlayerStateChanged(PlayerStates oldState, PlayerStates newState)
	{
		if(newState == PlayerStates.Moving)
		{
			_animator.SetTrigger(_movAnim);
		}
		else
		{
			_animator.SetTrigger(_idleAnim);
		}
	}


}
