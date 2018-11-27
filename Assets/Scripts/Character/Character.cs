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
    private Vector2 direction;  //Indica a direcao do personagem
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

    public bool IsAlive
    {
        get
        {
            return health.MyCurrentValue > 0;
        }
    }

    protected virtual void Awake() 
	{
		isAttacking = false;
		direction = Vector2.zero;
        health.MyCurrentValue = life;
	}

	void Start () 
	{
		rigidBody = GetComponent<Rigidbody2D>();
		Animator = GetComponent<Animator>();
	}
	
	protected virtual void Update () 
	{
		HandleLayers();
	}

	protected virtual void FixedUpdate() 
	{
        if (IsAlive)
		    Move();
	}

	public void Move() 
	{
		rigidBody.velocity = direction.normalized * speed;
	}

	public void HandleLayers()
	{
        if (IsAlive)
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
                StopAttack();
		    }
        } else {
            ActivateLayer("DeathLayer"); //Seta o layer para o estado death
            StopAttack();
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

    public int MeleeDamage
    {
        get
        {
            return meleeDamage;
        }

        set
        {
            meleeDamage = value;
        }
    }

    public float MeleeRange
    {
        get
        {
            return meleeRange;
        }

        set
        {
            meleeRange = value;
        }
    }

    public Stat Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
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
		}
        isAttacking = false;
        Animator.SetBool("attack", isAttacking);
        attackRoutine = null;
	}

	public virtual void TakeDamage(int value) 
	{
        health.MyCurrentValue -= value;
		if (health.MyCurrentValue <= 0) {
            Animator.SetTrigger("die");
            health.DisableCanvas(); //Hide lifebar
            rigidBody.velocity = Vector2.zero;
        }
	}
}
