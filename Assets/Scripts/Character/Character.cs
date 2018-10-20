using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

	[SerializeField]
	private float speed; //Indica a velocidade do personagem
	
	[SerializeField]
	protected int life;

	protected Animator animator;
	protected Vector2 direction; //Indica a direcao do personagem
	protected bool isAttacking; //Indica se o personagem esta atacando

	private Rigidbody2D rigidBody;

	private void Awake() 
	{
		direction = Vector2.zero;
	}

	void Start () 
	{
		rigidBody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}
	
	protected virtual void Update () 
	{
		HandleLayers();
	}

	protected virtual void  FixedUpdate() 
	{
		Move();
	}

	public void Move() 
	{
		//transform.Translate(direction * speed * Time.deltaTime);
		rigidBody.velocity = direction.normalized * speed;
	}

	public void HandleLayers()
	{
		//Indica se deve efetuar a animacao para movimentar-se ou nao.
		if (isMoving) {
			ActivateLayer("WalkLayer");
			animator.SetFloat("x", direction.x);
			animator.SetFloat("y", direction.y);
		} else {
			ActivateLayer("IdleLayer"); //Reseta o layer de andar, logo mantem-se parado
		}
	}

	public bool isMoving
	{
		get {
			return direction.x != 0 || direction.y != 0;
		}
	}

	public void ActivateLayer(string layerName)
	{
		for (int i = 0; i < animator.layerCount; i++)
		{
			animator.SetLayerWeight(i, 0);
		}

		animator.SetLayerWeight(animator.GetLayerIndex(layerName), 1);
	}
}
