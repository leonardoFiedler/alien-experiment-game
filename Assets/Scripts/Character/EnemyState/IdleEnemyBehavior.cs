using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleEnemyBehavior : IState 
{

	private EnemyCharacterController parent;

	public void Enter(EnemyCharacterController parent)
	{
		this.parent = parent;
	}

	public void Update()
	{
		if (parent.Target != null) 
		{
			//Change to follow state if player is close
			parent.ChangeState(new FollowEnemyBehavior());
		}
	}

	public void Exit()
	{

	}
}