using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateTerrain : MonoBehaviour
{
    bool populated = false;

    public List<Transform> DecorSpawns;
    public List<Transform> GroundSpawns;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsPopulated()
    {
        return populated;
    }

    public void Populate(List<GameObject> front_objects, List<GameObject> ground_objects)
    {
        foreach(Transform t in DecorSpawns)
        {
            int random = Random.Range(0, front_objects.Count + 5);
            if (random < front_objects.Count) Instantiate(front_objects[random], t.position, Quaternion.Euler(0f, 0f, 0f), t);
            Debug.Log("This plant is " + random);
        }

        foreach(Transform t in GroundSpawns)
        {
            int random = Random.Range(1, 100);
            if (random <= 25) Instantiate(ground_objects[0], t.position, Quaternion.Euler(0f, 0f, 0f), t);
            else if (random <= 30) Instantiate(ground_objects[1], t.position, Quaternion.Euler(0f, 0f, 0f), t);
            else if (random <= 35) Instantiate(ground_objects[2], t.position, Quaternion.Euler(0f, 0f, 0f), t);
            else if (random <= 40) Instantiate(ground_objects[3], t.position, Quaternion.Euler(0f, 0f, 0f), t);
            else if (random <= 45) Instantiate(ground_objects[4], t.position, Quaternion.Euler(0f, 0f, 0f), t);
            Debug.Log("This ground object is " + random);
        }
    }
}
