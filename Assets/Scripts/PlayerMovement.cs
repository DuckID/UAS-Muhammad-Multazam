using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	public Animator animator;

	public float runSpeed = 40f;

	public float horizontalMove = 0f;
	public bool jump = false;
	public bool crouch = false;
	public float penggerak;
	bool isDead = false;
	GameObject losePanel;
	GameObject winPanel;

	void Start () {
		losePanel = GameObject.Find ("B");
		winPanel = GameObject.Find ("A");
		losePanel.SetActive (false);
		winPanel.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; 
		animator.SetFloat("Speed", Mathf.Abs(penggerak));

		if (horizontalMove == (-40))
		{
			OnPenggerakKiri ();
		}

		if (horizontalMove == (40))
		{
			OnPenggerakKanan ();
		}

		if (Input.GetButtonDown("Jump"))
		{
			OnJump ();
		}

		if (Input.GetButtonDown("Crouch"))
		{
			OnCrouch ();
		} 
		else if (Input.GetButtonUp("Crouch"))
		{
			OnCrouchUp ();
		}

	}
	public void OnPenggerakKiri ()
	{
		penggerak = -40;
		//horizontalMove = (0);
		Debug.Log ("kiri");

	}

	public void OnPenggerakKanan ()
	{
		penggerak = 40;
		//horizontalMove = (0);
		Debug.Log ("kanan");
	}

	public void OnLanding ()
	{
		animator.SetBool("IsJumping", false);
	}

	public void OnCrouching (bool isCrouching)
	{
		animator.SetBool("IsCrouching", isCrouching);
	}

	public void OnJump ()
	{
		jump = true;
		animator.SetBool("IsJumping", true);
	}

	public void OnCrouch ()
	{
		crouch = true;
	}

	public void OnCrouchUp ()
	{
		crouch = false;
	}

	public void Stop ()
	{
		if (horizontalMove == (0))
		{
			penggerak = 0;
		}
	}

	public void FixedUpdate ()
	{
		// Move our character
		controller.Move(penggerak * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.transform.tag.Equals("Coin"))
		{
			Destroy(collision.gameObject);
			Data.score += 1;
			if (Data.score == 10) {
				winPanel.SetActive (true);
				Destroy (gameObject);
				Data.score = 0;
			}
		}

		if (collision.transform.tag.Equals("Enemy"))
		{
			Debug.Log ("lol");
			losePanel.SetActive (true);
			Destroy (gameObject);
			Data.score = 0;
		}
	}
	private void Dead ()
	{
		if (!isDead) {
			if (transform.position.y < -10f) {
				// kondisi ketika jatuh
				isDead = true;
			}
		}
	}
}
