using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

	[SerializeField]
	private float speed; //Indica a velocidade do personagem
	private Animator animator;
	protected Vector2 direction; //Indica a direcao do personagem

	private void Awake() 
	{
		direction = Vector2.zero;
	}

	void Start () 
	{
		animator = GetComponent<Animator>();
	}
	
	protected virtual void Update () 
	{
		Move();
	}

	public void Move() 
	{
		transform.Translate(direction * speed * Time.deltaTime);

		//Indica se deve efetuar a animacao para movimentar-se ou nao.
		if (direction.x != 0 || direction.y != 0) {
			AnimateMovement(direction);
		} else {
			animator.SetLayerWeight(1, 0); //Reseta o layer de andar, logo mantem-se parado
		}
	}

	public void AnimateMovement(Vector2 vector2)
	{
		animator.SetLayerWeight(1, 1);
		animator.SetFloat("x", vector2.x);
		animator.SetFloat("y", vector2.y);
	}
}
