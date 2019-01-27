using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSensor : MonoBehaviour
{
    CircleCollider2D sensor;

    [SerializeField]
    List<GameObject> resources;

    // Start is called before the first frame update
    void Start()
    {
        sensor = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateResources();
    }

    void UpdateResources()
    {
        var tempList = new List<GameObject>();
        foreach (GameObject sponge in resources)
        {
            if (sponge.GetComponent<SpongeController>().GetAlive() == false) tempList.Add(sponge);
        }
        foreach (GameObject rem in tempList) resources.Remove(rem);
    }

    public void SetRadius(float rad)
    {
        sensor.radius = rad;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Sponge" && !resources.Contains(collision.gameObject))
        {
            if (collision.gameObject.GetComponent<SpongeController>().GetAlive() == true) resources.Add(collision.gameObject);
            //Debug.Log(collision.gameObject.GetComponent<SpongeController>().GetAlive());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Sponge" && resources.Contains(collision.gameObject)) resources.Remove(collision.gameObject);
    }

    public List<GameObject> GetResourcesInRange()
    {
        UpdateResources();
        return resources;
    }
}
