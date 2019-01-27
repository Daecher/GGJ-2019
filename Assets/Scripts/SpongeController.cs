using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpongeController : MonoBehaviour
{
    [SerializeField]
    float health = 100;

    [SerializeField]
    bool alive = true;

    [SerializeField]
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && alive == true)
        {
            alive = false;
            anim.SetBool("isEating", false);
            anim.SetBool("isDead", true);
        }
    }

    public void Damage(float dmg)
    {
        if (health > 0) health -= dmg;
    }

    public bool GetAlive()
    {
        return alive;
    }

    public void SetEating(bool eat)
    {
        anim.SetBool("isEating", eat);
    }

    public bool GetEating()
    {
        return anim.GetBool("isEating");
    }
}
