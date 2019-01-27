using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitControl : MonoBehaviour {

	[SerializeField]
	int crabCount = 0;

    [SerializeField]
    List<GameObject> crabUnits;

    [SerializeField]
    List<GameObject> resources;

    public Transform unitSpawn;
	public PlatformEffector2D mouth;
	public GameObject crab;
    public ResourceSensor rc;
    public Energy en;

	bool assimilate = false;

	[SerializeField]
	List<Collider2D> triggerBodies;

	// Use this for initialization
	void Start () {
		triggerBodies = new List<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void MarkSafe(GameObject unit)
    {
        if (unit.GetComponent<CrabController>() != null)
        {
            unit.GetComponent<CrabController>().Safe();
            en.AddEnergy(unit.GetComponent<CrabController>().giveFood());
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
        //Debug.Log("hurr");
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

    public void CallUnits()
    {
        Debug.Log(crabUnits.Count);
        foreach(GameObject crab in crabUnits)
        {
            crab.GetComponent<CrabController>().SetTarget(this.transform);
        }
    }

    public void OrderGatherCrabs()
    {
        resources = rc.GetResourcesInRange();
        int iter = Mathf.Min(resources.Count, crabUnits.Count);
        //Debug.Log(iter);
        for(int i = 0; i < iter; i++)
        {
            var thisSponge = resources[i];
            var thisCrab = FindClosestCrab(thisSponge.transform.position);
            //Debug.Log(thisCrab.GetComponent<CrabController>());
            //Debug.Log(thisSponge.transform);
            thisCrab.GetComponent<CrabController>().SetTarget(thisSponge.transform);
        }
    }

    GameObject FindClosestCrab(Vector2 resourcePos)
    {
        float dist = -1f;
        GameObject crab = null;
        foreach(GameObject thisCrab in crabUnits)
        {
            var crabDist = Vector2.Distance(resourcePos, thisCrab.transform.position);
            if ((dist < 0 || crabDist < dist) && thisCrab.GetComponent<CrabController>().GetTarget() == null)
            {
                dist = crabDist;
                crab = thisCrab;
            }
            else if ((dist < 0 || crabDist < dist) && thisCrab.GetComponent<CrabController>().GetTarget().tag != "Sponge")
            {
                thisCrab.GetComponent<CrabController>().SetTarget(null);
                dist = crabDist;
                crab = thisCrab;
            }
        }
        return crab;
    }

}
