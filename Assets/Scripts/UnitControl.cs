using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitControl : MonoBehaviour {

	[SerializeField]
	int crabCount = 0;

    [SerializeField]
    List<GameObject> crabUnits;

    public Transform unitSpawn;

	public PlatformEffector2D mouth;

	public GameObject crab;

	bool assimilate = false;

	[SerializeField]
	List<Collider2D> triggerBodies;

	// Use this for initialization
	void Start () {
		triggerBodies = new List<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        foreach(GameObject crab in crabUnits)
        {
            var crabAI = crab.GetComponent<CrabController>();
        }

	}

    public void MarkSafe(GameObject unit)
    {
        if (unit.GetComponent<CrabController>() != null)
        {
            unit.GetComponent<CrabController>().Safe();
        }
    }

	public void CrabControl(bool intake)
	{
		if (intake == true)
		{
			// Bring back the crabs!
			mouth.rotationalOffset = 0;
			assimilate = true;
			StartCoroutine(Assimilate());
		}
		else
		{
			// launch ALL the crabs
			mouth.rotationalOffset = 180;
			assimilate = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!triggerBodies.Contains(collision) && collision.tag == "Unit") triggerBodies.Add(collision);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (triggerBodies.Contains(collision) && collision.tag == "Unit") triggerBodies.Remove(collision);
	}

	IEnumerator Assimilate()
	{
		InvokeRepeating("AssimilateUnits", 0, 0.01f);
		yield return new WaitForSeconds(1f);
		CancelInvoke("AssimilateUnits");
	}

	void AssimilateUnits()
	{
		//Debug.Log("WOOSH");
		foreach (Collider2D col in triggerBodies)
		{
			col.transform.GetComponent<Rigidbody2D>().AddForce((mouth.transform.position - col.transform.position) * 0.05f);
		}
	}

}
