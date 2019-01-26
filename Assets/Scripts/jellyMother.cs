using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jellyMother : MonoBehaviour {


    private Animator anim;

    public bool swimming;
    public bool sick;
    public float speed = 5f;
    private Vector2 moveAmount;

    private Rigidbody2D rb;

	// Use this for initialization
	private void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	private void Update () {

        //Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;


		if(swimming == true)
        {
            anim.SetBool("isSwimming", true);
            Swim();
            moveAmount = speed * transform.right;

        }
        else
        {
            anim.SetBool("isSwimming", false);
        
        }
        if (sick == true)
        {
            anim.SetBool("isSick", true);

        }
        else
        {
            anim.SetBool("isSick", false);

        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    }

    void Swim()
    {

    }
}
