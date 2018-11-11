using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacterController : Character {

	private Transform target;

	private IState currentState;

	public Transform Target
	{
		get {
			return target;
		}

		set {
			target = value;
		}
	}

	protected void Awake()
	{
		ChangeState(new IdleEnemyBehavior());
	}

	protected override void Update () {
		currentState.Update();
		base.Update();
	}

	public void ChangeState(IState newState)
	{
		if (currentState != null) {
			currentState.Exit();
		}

		currentState = newState;
		currentState.Enter(this);
	}
}
