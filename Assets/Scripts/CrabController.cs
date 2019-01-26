using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabController : MonoBehaviour
{

    Rigidbody2D rb;
    public float righting;

    [SerializeField]
    Transform target;

    [SerializeField]
    bool closeToTarget = false;

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
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("MakeUpright", 0f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        //CheckGrounded();
        if (CheckGrounded() == true) keepUpright = true;
        else keepUpright = false;

        if (target != null) closeToTarget = CloseToTarget();

        switch (state)
        {
            case (int)State.DO_NOTHING:
                if (target != null && closeToTarget == false) state = (int)State.MOVE;
                break;
            case (int)State.UPRIGHT:
                break;
            case (int)State.MOVE:
                if (target != null)
                {
                    //if (closeToTarget == true) Debug.Log(closeToTarget);
                    if (CheckUpright() == true && CheckGrounded() == true && closeToTarget == false) MoveTowardsTarget();

                    // Check if sponge or mother
                    else if (closeToTarget == true)
                    {
                        state = (int)State.DO_NOTHING;
                    }
                }
                else state = (int)State.DO_NOTHING;
                break;
            case (int)State.GATHER:
                break;
        }

    }

    void MakeUpright()
    {
        //Debug.Log(CheckUpright() + " " + CheckGrounded());
        if (CheckUpright() == false && CheckGrounded() == true)
        {
            //Debug.Log(transform.eulerAngles.z);
            rb.AddForce(Vector2.up * righting);
            if (transform.eulerAngles.z > 210) rb.AddTorque(righting * 0.8f);
            else if (transform.eulerAngles.z < 150) rb.AddTorque(-righting * 0.8f);
            else
            {
                rb.AddForce(Vector2.up * righting);
                rb.AddTorque(-righting);
            }
        }
    }

    public void Safe()
    {
        state = (int)State.DO_NOTHING;
        target = null;
    }

    bool CheckUpright()
    {
        var vel = rb.velocity;
        if (Mathf.Abs(vel.x) <= 0.10f) vel.x = 0;
        if (Mathf.Abs(vel.y) <= 0.10f) vel.y = 0;
        //Debug.Log(transform.up.y);
        if (transform.up.y < 0.75f) return false;
        else return true;
    }

    bool CheckGrounded()
    {
        int layerMask = (1 << 8);
        layerMask |= (1 << 9);
        layerMask |= (1 << 10);
        layerMask = ~(layerMask);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.4f, layerMask);
        if (hit.collider != null && hit.collider.tag == "Ground")
        {
            //Debug.Log("Grounded!");
            return true;
        }
        else if (hit.collider != null)
        {
            //Debug.Log(hit.collider.name);
            return false;
        }

        return false;
    }

    bool CloseToTarget()
    {
        float disToTarget = Mathf.Abs(transform.position.x - target.position.x);
        //Debug.Log(dirToTarget.magnitude);
        if (disToTarget > 0.5f) return false;
        else return true;

    }

    void MoveTowardsTarget()
    {
        Vector2 dirToTarget = -(transform.position - target.position);
        var moveSpeed = Mathf.Clamp(dirToTarget.x, -1f, 1f);
        rb.velocity = new Vector2(moveSpeed, 0);
        AdjustOrientation();
    }

    public void SetTarget(Transform newTarget)
    {
        Debug.Log("new target");
        target = newTarget;
    }

    public Transform GetTarget()
    {
        return target;
    }

    void AdjustOrientation()
    {
        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(-0.25f, 0.25f, 1f);
        }
        else if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(0.25f, 0.25f, 1f);
        }
    }
}