using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Interface para controle de estados dos inimigos.
public interface IState 
{
	void Enter(EnemyCharacterController parent);

	void Update();

	void Exit();
}
