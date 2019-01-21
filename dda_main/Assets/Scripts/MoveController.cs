using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour {
	//https://unity3d.com/ru/learn/tutorials/topics/2d-game-creation/creating-basic-platformer-game


	[HideInInspector] public bool facingRight = true;//повернут ли игрок право?
	[HideInInspector] public bool jump = false;//он у прижку?

	public float moveForce = 365f;//ссила которая прикладуется при движении
	public float maxSpeed = 5f;//максимальная скорость движения
	public float jumpForce = 500f;//сила прижка

	//public Transform groundCheck;//трансофрм самой земли

	private bool grounded = false;// он на земле?
	//private Animator anim;//аниматор ОТКЛЮЧЕН
	public Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		if (Input.GetButtonDown("Jump") && grounded)//
		{
			jump = true;
		}
	}

	void FixedUpdate()
	{
		float h = Input.GetAxis("Horizontal");

		//anim.SetFloat("Speed", Mathf.Abs(h));

		/*
		if (h * rb2d.velocity.x < maxSpeed)
			rb2d.AddForce(Vector2.right * h * moveForce);

		if (Mathf.Abs (rb2d.velocity.x) > maxSpeed)
			rb2d.velocity = new Vector2(Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
		*/

		rb2d.velocity = new Vector2 (h*maxSpeed, rb2d.velocity.y);

		if (h > 0 && !facingRight)
			Flip ();
		else if (h < 0 && facingRight)
			Flip ();

		if (jump)
		{
			//anim.SetTrigger("Jump");
			rb2d.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}
	}


	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnCollisionStay2D(Collision2D collider)
	{
		CheckIfGrounded ();
	}

	void OnCollisionExit2D(Collision2D collider)
	{
		grounded = false;
	}

	private void CheckIfGrounded()
	{
		RaycastHit2D[] hits;

		//We raycast down 1 pixel from this position to check for a collider
		Vector2 positionToCheck = transform.position;
		hits = Physics2D.RaycastAll (positionToCheck, new Vector2 (0, -1), 0.01f);

		//if a collider was hit, we are grounded
		if (hits.Length > 0) {
			grounded = true;
		}
	}
}
