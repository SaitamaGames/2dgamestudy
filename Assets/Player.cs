using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class Player: MonoBehaviour {
	
	public Rigidbody2D cRigidbody2D
	{
		get
		{
			if(!_cRigidbody2D)
				_cRigidbody2D = GetComponent<Rigidbody2D>();
			return _cRigidbody2D;
		}
	}
	Rigidbody2D _cRigidbody2D;
	
	public Transform cTransform
	{
		get
		{
			if(!_cTransform)
				_cTransform = transform;
			return _cTransform;
		}
	}

	public Animator cAnimator
	{
		get
		{
			if(!_cAnimator)
			_cAnimator = GetComponent<Animator>();
			return _cAnimator;
		}
	}
	Animator _cAnimator;
	Transform _cTransform;
	
	public float moveSpeed = 5;
	public float jumpForce = 300;
	
	float InputHorValue;
	
	bool isGrounded;
	bool canJump;
	
	void Update()
	{
		InputCheck();
		MecCheck();  
	}

	//キーが押されていたら
	void InputCheck()
	{
		InputHorValue = Input.GetAxisRaw("Horizontal");
		if(isGrounded && Input.GetButtonDown("Jump")) canJump = true;
	
	}
	void MecCheck()
	{
		bool isRunning = InputHorValue != 0;
		float velY = cRigidbody2D.velocity.y;
		bool isJumping = velY > 0.1f ? true:false;
		bool isFalling = velY < -0.1f ? true:false;
		cAnimator.SetBool("isRunning",isRunning);
		cAnimator.SetBool("Jamp",isJumping);
		cAnimator.SetBool("JampNow",isFalling);
		cAnimator.SetBool("JampEnd",isGrounded);
	}


	void FixedUpdate()
	{
		Move();
		Jump();
	}
	
	void Move()
	{
		if((cTransform.localScale.x > 0 && InputHorValue < 0)
		   || (cTransform.localScale.x < 0 && InputHorValue > 0))
		{
			Vector2 temp = cTransform.localScale;
			temp.x *= -1;
			cTransform.localScale = temp;
		}
		cRigidbody2D.velocity = new Vector2(moveSpeed * InputHorValue,
		                                    cRigidbody2D.velocity.y);
	}
	
	void Jump()
	{
		if(canJump)
		{
			canJump = false;
			isGrounded = false;
			cRigidbody2D.AddForce(Vector2.up * jumpForce);
		}
	}
	
	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag == "yuka")
			isGrounded = true;

		if(col.gameObject.tag == "kabe")
			isGrounded = true;
	}


}
