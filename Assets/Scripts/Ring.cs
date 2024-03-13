using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    private  Transform player;
    public GameObject[] childRings;


    float radius = 100f;
    float force = 300f;

    [SerializeField]
    private int passCount = 0;
    public TrailRenderer playerTrailRenderer;

    private GameManager gameManager;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        if (transform.position.y > player.position.y)
        {
            GameManager.noOfPassingRings++;
            FindObjectOfType<SoundManager>().Play("Whoosh");

            gameManager.GameScore(5);
            passCount++;
           
            if (passCount >= 2)
            {
                ChangePlayerTrailRenderer();
            }
            for (int i = 0; i < childRings.Length; i++)
            {
                childRings[i].GetComponent<Rigidbody>().isKinematic = false;
                childRings[i].GetComponent<Rigidbody>().useGravity = true;

                Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
                foreach (Collider newCollider in colliders)
                {
                    Rigidbody rb = newCollider.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.AddExplosionForce(force, transform.position, radius);
                    }
                }
                childRings[i].GetComponent<MeshCollider>().enabled = false;
                childRings[i].transform.parent = null;
                Destroy(childRings[i].gameObject, 2f);
                Destroy(this.gameObject, 5f);
            }
            this.enabled = false;
        }


    }
    


    void ChangePlayerTrailRenderer()
    {
        playerTrailRenderer.material.color = Color.black;
        
    }

}
