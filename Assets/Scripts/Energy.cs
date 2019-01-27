using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    [SerializeField]
    float energy = 30f;

    [SerializeField]
    float energyDrain = 0.05f;

    [SerializeField]
    Animator anim;

    public GameObject influenceRadius;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e")) energy += 10;
        if (energy > 0) energy -= Time.deltaTime * energyDrain;
        var clampedEnergy = Mathf.Clamp(energy, 10, 50);
        influenceRadius.GetComponent<SpriteRenderer>().size = new Vector2(clampedEnergy, clampedEnergy);
        if (energy < 10 && anim.GetBool("isSick") == false) anim.SetBool("isSick", true);
        else if (energy > 10 && anim.GetBool("isSick") == true) anim.SetBool("isSick", false);
    }
}
