using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabController : MonoBehaviour
{

    Rigidbody2D rb;
    public float righting;

    [SerializeField]
    float foodConsumed;

    [SerializeField]
    Transform target;

    SpongeController targetController;

    [SerializeField]
    bool closeToTarget = false;

    [SerializeField]
    int state = 0;

    public Transform motherJelly;

    public Animator anim;
    public SpriteRenderer energySprite;
    public SpriteRenderer loveSprite;
    public SpriteRenderer completeSprite;

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
        anim.SetBool("isHiding", true);
        energySprite.enabled = false;
        loveSprite.enabled = false;
        completeSprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //CheckGrounded();
        if (CheckGrounded() == true) keepUpright = true;
        else keepUpright = false;

        if (target != null)
        {
            closeToTarget = CloseToTarget();
            if (target.tag == "Sponge") targetController = target.gameObject.GetComponent<SpongeController>();
            else targetController = null;
        }

        switch (state)
        {
            case (int)State.DO_NOTHING:
                if (anim.GetBool("isWalking") == true) anim.SetBool("isWalking", false);
                //if (anim.GetBool("isAlerting") == true) anim.SetBool("isAlerting", false);
                if (target != null && closeToTarget == false) state = (int)State.MOVE;
                else if (target != null)
                {
                    anim.SetBool("isAlerting", true);
                    EnableEnergyStatus();
                }
                break;
            case (int)State.UPRIGHT:
                break;
            case (int)State.MOVE:
                if (target != null)
                {
                    if (anim.GetBool("isAlerting") == true) anim.SetBool("isAlerting", false);
                    DisableStatus();
                    //if (closeToTarget == true) Debug.Log(closeToTarget);
                    //Debug.Log(CheckUpright() + " " + CheckGrounded() + " " + closeToTarget);
                    if (CheckUpright() == true && CheckGrounded() == true && closeToTarget == false)
                    {
                        MoveTowardsTarget();
                        
                    }

                    // Check if sponge or mother
                    else if (closeToTarget == true)
                    {
                        if (target.tag == "Sponge")
                        {
                            state = (int)State.GATHER;
                            anim.SetBool("isGathering", true);
                            anim.SetBool("isWalking", false);
                        }
                        else
                        {
                            state = (int)State.DO_NOTHING;
                        }
                    }
                }
                else state = (int)State.DO_NOTHING;
                break;
            case (int)State.GATHER:
                if (target.tag != "Sponge")
                {
                    state = (int)State.DO_NOTHING;
                    anim.SetBool("isGathering", false);
                    break;
                }

                targetController = target.gameObject.GetComponent<SpongeController>();
                if (targetController.GetAlive() == true)
                {
                    if (targetController.GetEating() == false) targetController.SetEating(true);
                    targetController.Damage(1f);
                    foodConsumed += 1f;
                }
                else if (targetController.GetAlive() == false)
                {
                    target = null;
                    Debug.Log("Finished eating!");
                    state = (int)State.DO_NOTHING;
                    anim.SetBool("isGathering", false);
                    anim.SetBool("isAlerting", true);
                    EnableEnergyStatus();
                }

                break;
        }

    }

    void DisableStatus()
    {
        energySprite.enabled = false;
        loveSprite.enabled = false;
        completeSprite.enabled = false;
    }

    void EnableEnergyStatus()
    {
        energySprite.enabled = true;
        loveSprite.enabled = false;
        completeSprite.enabled = true;
    }

    void EnableLoveStatus()
    {
        energySprite.enabled = false;
        loveSprite.enabled = true;
        completeSprite.enabled = true;
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
        anim.SetBool("isAlerting", false);
        DisableStatus();
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.35f, layerMask);
        if (hit.collider != null && hit.collider.tag == "Ground")
        {
            //Debug.Log("Grounded!");
            anim.SetBool("isHiding", false);
            return true;
        }
        else if (hit.collider != null)
        {
            //Debug.Log(hit.collider.name);
            anim.SetBool("isHiding", true);
            return false;
        }
        anim.SetBool("isHiding", true);
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
        if (anim.GetBool("isWalking") == false) anim.SetBool("isWalking", true);
        Vector2 dirToTarget = -(transform.position - target.position);
        var moveSpeed = Mathf.Clamp(dirToTarget.x, -1f, 1f);
        rb.velocity = new Vector2(moveSpeed, 0);
        AdjustOrientation();
    }

    public void SetTarget(Transform newTarget)
    {
        //Debug.Log("new target");
        if (state == (int)State.GATHER) targetController.SetEating(false);
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

    public float giveFood()
    {
        var food = foodConsumed;
        foodConsumed = 0;
        return food;
    }
}