using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour {
	
	public float maxVelocity = 1f;
	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("d"))
		{
			if (rb.velocity.x < maxVelocity) rb.velocity += new Vector2(0.25f, 0);
		}
		if (Input.GetKey("a"))
		{
			if (rb.velocity.x > -maxVelocity) rb.velocity += new Vector2(-0.25f, 0);
		}
		if (Input.GetKey("w"))
		{
			if (rb.velocity.y < maxVelocity) rb.velocity += new Vector2(0, 0.25f);
		}
		if (Input.GetKey("s"))
		{
			if (rb.velocity.y > -maxVelocity) rb.velocity += new Vector2(0, -0.25f);
		}
	}
	
	void FixedUpdate()
	{
		var vel = rb.velocity;
		vel.x *= 0.925f;
		vel.y *= 0.925f;
		if (Mathf.Abs(vel.x) <= 0.15f) vel.x = 0;
		if (Mathf.Abs(vel.y) <= 0.15f) vel.y = 0;
		rb.velocity = vel;
	}
}
