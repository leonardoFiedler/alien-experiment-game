using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

	[SerializeField]
	private float speed; //Indica a velocidade do personagem
	
	[SerializeField]
	protected int life;

	[SerializeField]
	protected int meleeDamage;

	[SerializeField]
	protected Transform meleeAttackPosition; //Transform com a posicao de ataque

	[SerializeField]
	protected float meleeRange;

	[SerializeField]
	protected Stat health;

	protected Animator animator;
	private Vector2 direction; //Indica a direcao do personagem
	protected bool isAttacking; //Indica se o personagem esta atacando

	protected Coroutine attackRoutine;
	private Rigidbody2D rigidBody;

	public Vector2 Direction
	{
		get {
			return direction;
		}

		set {
			direction = value;
		}
	}

	public float Speed
	{
		get {
			return speed;
		}

		set {
			speed = value;
		}
	}

	private void Awake() 
	{
		isAttacking = false;
		direction = Vector2.zero;
	}

	void Start () 
	{
		rigidBody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}
	
	protected virtual void Update () 
	{
		health.MyCurrentValue = this.life;
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

			StopAttack();
		} else if (isAttacking) {
			ActivateLayer("AttackLayer");
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

	public void StopAttack()
	{
		if (attackRoutine != null) {
			StopCoroutine(attackRoutine);
			isAttacking = false;
			animator.SetBool("attack", isAttacking);
		}
	}

	public virtual void TakeDamage(int value) 
	{
		life -= value;
		if (life <= 0) {
			Destroy(gameObject);
		}
	}
}
