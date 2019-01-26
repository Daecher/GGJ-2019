using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabController : MonoBehaviour {

	Rigidbody2D rb;
	public float righting;

    [SerializeField]
    Transform target;

    [SerializeField]
    int state = 0;

    bool keepUpright = true;

    enum State : int
    {
        DO_NOTHING,
        UPRIGHT,
        MOVE,
        GATHER
    }

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		InvokeRepeating("CheckUpright", 0f,3f);
	}
	
	// Update is called once per frame
	void Update () {
        //CheckGrounded();
        if (CheckGrounded() == true) keepUpright = true;
        else keepUpright = false;
        switch (state)
        {
            case (int)State.DO_NOTHING:
                break;
            case (int)State.UPRIGHT:
                break;
            case (int)State.MOVE:
                break;
            case (int)State.GATHER:
                break;
        }

	}

	void CheckUpright()
	{
		if (transform.up.y < 0.5f && CheckGrounded() == true)
		{
            //Debug.Log(transform.eulerAngles.z);
            rb.AddForce(Vector2.up * righting);
            if (transform.eulerAngles.z > 210) rb.AddTorque(righting);
			else if (transform.eulerAngles.z < 150) rb.AddTorque(-righting);
			else
			{
				rb.AddForce(Vector2.up * righting);
				rb.AddTorque(-righting);
			}
		}
	}

    bool CheckGrounded()
    {
        int layerMask = ~(1 << 8);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.5f, layerMask);
        if (hit.collider != null && hit.collider.tag == "Ground")
        {
            Debug.Log("Grounded!");
            return true;
        }
        else if (hit.collider != null)
        {
            Debug.Log(hit.collider.tag);
            return false;
        }

        return false;
    }
}
