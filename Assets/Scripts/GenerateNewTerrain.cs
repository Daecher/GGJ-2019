﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateNewTerrain : MonoBehaviour
{
    public List<GameObject> plantPrefabs;
    public List<GameObject> groundPrefabs;
    public List<GameObject> startingTerrain;
    public GameObject terrain;
    public Transform terrainHolder;

    public BoxCollider2D ground_collider;

    [SerializeField]
    float previousXmin = 0;
    [SerializeField]
    float previousXMax = 0;

    float currentX;
    [SerializeField]
    int leftTerrain = 0;
    [SerializeField]
    int rightTerrain = 0;

    List<GameObject> unpopulatedTerrains;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject ter in startingTerrain)
        {
            ter.GetComponent<PopulateTerrain>().Populate(plantPrefabs, groundPrefabs);
        }

        UpdateCollider();
    }

    // Update is called once per frame
    void Update()
    {
        currentX = transform.position.x;
        if (currentX < previousXmin) previousXmin = currentX;
        else if (currentX > previousXMax) previousXMax = currentX;

        if (currentX > (rightTerrain - 7) * 5.12f)
        {
            GenerateRightTerrain();
            rightTerrain++;
        }
        else if (currentX < (leftTerrain - 6) * -5.12f)
        {
            GenerateLeftTerrain();
            leftTerrain++;
        }
        
    }

    void GenerateLeftTerrain()
    {
        var ter = Instantiate(terrain, new Vector3((leftTerrain + 1) * -5.12f, 0f, 0f), Quaternion.Euler(0f,0f,0f), terrainHolder);
        ter.GetComponent<PopulateTerrain>().Populate(plantPrefabs, groundPrefabs);
        UpdateCollider();
    }
    
    void GenerateRightTerrain()
    {
        var ter = Instantiate(terrain, new Vector3((rightTerrain) * 5.12f, 0f, 0f), Quaternion.Euler(0f, 0f, 0f), terrainHolder);
        ter.GetComponent<PopulateTerrain>().Populate(plantPrefabs, groundPrefabs);
        UpdateCollider();
    }

    void UpdateCollider()
    {
        ground_collider.size = new Vector2(5.12f * (2 * (Mathf.Max(leftTerrain, rightTerrain) + 2)), 1f);
        
    }

}
