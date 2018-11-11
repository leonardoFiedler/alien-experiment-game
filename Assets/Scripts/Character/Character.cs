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

    private Animator animator;
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

    public bool IsAttacking
    {
        get
        {
            return isAttacking;
        }

        set
        {
            isAttacking = value;
        }
    }

    public Animator Animator
    {
        get
        {
            return animator;
        }

        set
        {
            animator = value;
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
		Animator = GetComponent<Animator>();
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
			Animator.SetFloat("x", direction.x);
			Animator.SetFloat("y", direction.y);

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

    public Transform MeleeAttackPosition
    {
        get
        {
            return meleeAttackPosition;
        }

        set
        {
            meleeAttackPosition = value;
        }
    }

    public void ActivateLayer(string layerName)
	{
		for (int i = 0; i < Animator.layerCount; i++)
		{
			Animator.SetLayerWeight(i, 0);
		}

		Animator.SetLayerWeight(Animator.GetLayerIndex(layerName), 1);
	}

	public void StopAttack()
	{
		if (attackRoutine != null) {
			StopCoroutine(attackRoutine);
			isAttacking = false;
			Animator.SetBool("attack", isAttacking);
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
