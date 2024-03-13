using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellixManager : MonoBehaviour
{
    public GameObject[] ring;

    public int noOfRings;
    public float ringDistance = 5f;
    float yPos;


    private void Start()
    {
        noOfRings = GameManager.currentLevelIndex + 10;
        for (int i = 0; i < noOfRings; i++)
        {
            if (i == 0)
            {
                //ilk ringi spawnla
                SpawnRings(0);
            }
            else
            {   
                SpawnRings(Random.Range(1, ring.Length - 1));
            }
        }
        //son ringi spawnla
        SpawnRings(ring.Length - 1);
    }
    public void SpawnRings(int index)
    {
        GameObject newRing = Instantiate(ring[index], new Vector3(transform.position.x, yPos, transform.position.z), Quaternion.identity);
        yPos -= ringDistance;
        newRing.transform.parent = transform;
    }
}
