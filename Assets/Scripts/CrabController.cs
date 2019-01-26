using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabController : MonoBehaviour {

	Rigidbody2D rb;
	public float righting;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		InvokeRepeating("CheckUpright", 0f,3f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CheckUpright()
	{
		if (transform.up.y < 0.5f)
		{
			//Debug.Log(transform.eulerAngles.z);
			
			if (transform.eulerAngles.z > 210)
			{
				//Debug.Log("Turn left!");
				rb.AddForce(Vector2.up * righting);
				rb.AddTorque(righting);
			}
			else if (transform.eulerAngles.z < 150)
			{
				//Debug.Log("Turn right!");
				rb.AddForce(Vector2.up * righting);
				rb.AddTorque(-righting);
			}
			else
			{
				//Debug.Log("Flip over!");
				rb.AddForce(Vector2.up * 2 * righting);
				rb.AddTorque(-righting);
			}
		}
	}
}
