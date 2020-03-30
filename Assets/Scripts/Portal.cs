using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject portal;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject playerToDestroy;
    public int timeInterval;
    public bool isActive;

    private void OnTriggerStay(Collider other)
    {
        if (isActive)
        {
            if (other.gameObject.tag == playerToDestroy.tag)
            {
                Invoke("DestroyPlayer", timeInterval);
                Invoke("Teleport", 2f);
            }
        }
    }

    void Teleport()
    {
        portal.SetActive(false);
        explosion.SetActive(true);
        if (playerToDestroy.tag == GameManager.sphereTag) FindObjectOfType<GameManager>().sphereOn = false;
        else if (playerToDestroy.tag == GameManager.engineerTag) FindObjectOfType<GameManager>().engineerOn = false;
        Destroy(this.gameObject);
    }

    private void DestroyPlayer()
    {
        playerToDestroy.SetActive(false);
    }
}
