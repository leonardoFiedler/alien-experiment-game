using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacterController : Character {

	private Transform target;

	private IState currentState;
    private float attackRange;

    public float AttackTime { get; set; }

    public float AttackRange
    {
        get
        {
            return attackRange;
        }
    }

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
        attackRange = 1;
        ChangeState(new IdleEnemyBehavior());
	}

	protected override void Update () {

        if (!IsAttacking)
        {
            AttackTime += Time.deltaTime; 
        }

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
