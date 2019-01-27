using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour {
	
	public float maxVelocity = 1f;
	Rigidbody2D rb;
    [SerializeField]
    Animator anim;

    public List<GameObject> controlButtons;
    public List<GameObject> deathButtons;

    bool dead = false;

    // Use this for initialization
    void Start () {
		rb = GetComponent<Rigidbody2D>();
		
	}
	
	// Update is called once per frame
	void Update () {
        if (dead == false)
        {
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

            if (rb.velocity.magnitude > 0.5) anim.SetBool("isSwimming", true);
            else anim.SetBool("isSwimming", false);

            AdjustOrientation();
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

    void AdjustOrientation()
    {
        if (rb.velocity.x > 0.05)
        {
            transform.localScale = new Vector3(-0.75f, 0.75f, 1f);
            foreach(SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
            {
                if (sr.tag != "flip") sr.flipX = true;
            }
        }
        else if (rb.velocity.x < 0.05)
        {
            transform.localScale = new Vector3(0.75f, 0.75f, 1f);
            foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
            {
                if (sr.tag != "flip") sr.flipX = false;
            }
        }
    }

    public void Die()
    {
        foreach (GameObject button in controlButtons)
        {
            button.SetActive(false);
        }
        foreach (GameObject button in deathButtons)
        {
            button.SetActive(true);
        }

        dead = true;
    }
}
