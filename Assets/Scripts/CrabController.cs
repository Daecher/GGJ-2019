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
		InvokeRepeating("MakeUpright", 0f,3f);
	}
	
	// Update is called once per frame
	void Update () {
        //CheckGrounded();
        if (CheckGrounded() == true) keepUpright = true;
        else keepUpright = false;

        switch (state)
        {
            case (int)State.DO_NOTHING:
                if (target != null && CloseToTarget() == false) state = (int)State.MOVE;
                break;
            case (int)State.UPRIGHT:
                break;
            case (int)State.MOVE:
                if (CheckUpright() == true && CheckGrounded() == true) MoveTowardsTarget();

                // Check if sponge or mother
                if (CloseToTarget() == true) state = (int)State.DO_NOTHING;
                break;
            case (int)State.GATHER:
                break;
        }

	}

	void MakeUpright()
	{
        Debug.Log(CheckUpright() + " " + CheckGrounded());
		if (CheckUpright() == false && CheckGrounded() == true)
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

    bool CheckUpright()
    {
        Debug.Log(transform.up.y);
        if (transform.up.y < 0.75f) return false;
        else return true;
    }

    bool CheckGrounded()
    {
        int layerMask = (1 << 8);
        layerMask |= (1 << 10);
        layerMask = ~(layerMask);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.5f, layerMask);
        if (hit.collider != null && hit.collider.tag == "Ground")
        {
            Debug.Log("Grounded!");
            return true;
        }
        else if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
            return false;
        }

        return false;
    }

    bool CloseToTarget()
    {
        Vector2 dirToTarget = -(transform.position - target.position);
        Debug.Log(dirToTarget.magnitude);
        if (dirToTarget.magnitude > 0.5f) return false;
        else return true;

    }

    void MoveTowardsTarget()
    {
        Vector2 dirToTarget = -(transform.position - target.position);
        rb.velocity = new Vector2(dirToTarget.normalized.x, 0);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public Transform GetTarget()
    {
        return target;
    }
}
