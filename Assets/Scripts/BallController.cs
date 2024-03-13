using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float bouncForce = 400f;
    Rigidbody rb;

    public GameObject splitPrefabImage;
    public GameObject particlePrefab;

    public float yourSpeedIncreaseValue;

    SoundManager soundManager;

    private void Start()
    {
        soundManager=FindObjectOfType<SoundManager>();
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision other)
    {

        rb.velocity = new Vector3(rb.velocity.x, bouncForce * Time.deltaTime, rb.velocity.z);
        soundManager.Play("Bounce");
        GameObject newSplit = Instantiate(splitPrefabImage, new Vector3(transform.position.x, other.transform.position.y+ 0.192f, transform.position.z),transform.rotation);
        newSplit.transform.localScale = Vector3.one * Random.Range(0.8f, 1.2f);
        newSplit.transform.parent = other.transform;

        if (other.gameObject.CompareTag("deneme"))
        {
            
            GameObject newParticlePrefab = Instantiate(particlePrefab, new Vector3(transform.position.x, other.transform.position.y + 0.7f, transform.position.z), Quaternion.identity);
            Destroy(newParticlePrefab, 2f);
        }
  

        string materialName = other.transform.GetComponent<MeshRenderer>().material.name;
        
        if (materialName == "SafeMat (Instance)")
        {
            //Debug.Log("you're safe");
        }
        if (materialName == "ObstacleMat (Instance)")
        {
            GameManager.gameOver = true;
            soundManager.Play("Game Over");
        }
        if (materialName == "lastRingMat (Instance)" && !GameManager.levelWin)
        {
            GameManager.levelWin = true;
            soundManager.Play("Game Level");
        }
        
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("speed"))
        {
            
            if (rb != null)
            {
                rb.AddForce(Vector3.down * yourSpeedIncreaseValue *10, ForceMode.Acceleration);
            }

            Destroy(other.gameObject);
        }
    }

}
