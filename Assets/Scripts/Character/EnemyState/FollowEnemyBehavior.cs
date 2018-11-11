using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemyBehavior : IState
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
			//Find direction from enemy to player
			parent.Direction = (parent.Target.transform.position - parent.transform.position).normalized;

			//Walk in direction to player
			parent.transform.position = Vector2.MoveTowards(parent.transform.position, 
															parent.Target.transform.position, 
															parent.Speed * Time.deltaTime);

		} else {
			parent.ChangeState(new IdleEnemyBehavior());
		}
	}

	public void Exit()
	{
		parent.Direction = Vector2.zero;
	}
}
