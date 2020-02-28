using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private Transform respawnPlace;
    private GameObject engineer;
    private GameObject sphere;

    private void Awake()
    {
        engineer = GameObject.FindGameObjectWithTag(GameManager.engineerTag);
        sphere = GameObject.FindGameObjectWithTag(GameManager.sphereTag);
        respawnPlace = GameObject.Find("RespawnPlaceHolder").GetComponent<Transform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == GameManager.engineerTag || other.gameObject.tag == GameManager.sphereTag)
        {
            //if (other.gameObject.tag == GameManager.engineerTag)
            //{  
            //    Destroy(other);
            //    Instantiate(engineer, respawnPlace);
            //}
            //else if (other.gameObject.tag == GameManager.sphereTag)
            //{
            //    Destroy(other);
            //    Instantiate(sphere, respawnPlace);
            //}
        }
    }

}
