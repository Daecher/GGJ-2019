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
        
    }

    public void SetRadius(float rad)
    {
        sensor.radius = rad;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Sponge" && !resources.Contains(collision.gameObject)) resources.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Sponge" && resources.Contains(collision.gameObject)) resources.Remove(collision.gameObject);
    }

    public List<GameObject> GetResourcesInRange()
    {
        return resources;
    }
}
