using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Transform respawnPlace;
    [SerializeField] private GameObject engineerPrefab;
    [SerializeField] private GameObject spherePrefab;
    public static bool begin = false;

    private void Awake()
    {
        respawnPlace = GameObject.Find("RespawnPlaceHolder").GetComponent<Transform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == GameManager.engineerTag)
        {
            StartCoroutine(RespawnChar(other.gameObject));
            Instantiate(engineerPrefab, respawnPlace);

            if (begin) { FindObjectOfType<AIUI>().ShowText("<< Try to ask the ___SPHERE___ to jump over this gap.>>"); begin = false; }
        }
    }

    IEnumerator RespawnChar(GameObject player)
    {
        player.SetActive(false);
        player.transform.position = respawnPlace.position;
        player.transform.rotation = Quaternion.identity;
        yield return new WaitForSeconds(0.1f);
        player.SetActive(true);
        StopCoroutine(RespawnChar(player));
    }
}
