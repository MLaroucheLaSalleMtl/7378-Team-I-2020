using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSpider : MonoBehaviour
{

    [SerializeField] private Animator anim;
    [SerializeField] private GameObject web;
    [SerializeField] private GameObject platform;

    public void Die()
    {
        anim.SetBool("Dead", true); //phil: isntead of trigger I changed to boolean
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "PlayerSphereLeg")
        {
            Debug.Log($"Spider hit by {collision.gameObject.tag}");
            Die();
            FindObjectOfType<AIUI>().ShowText("You killed the spider and the spider web is removed. Now you can access your car.");
            web.SetActive(false); //phil: is it not better to destroy the gameObject?
            Invoke("Remove", 3f);

            platform.transform.position = new Vector3(0, 0);
        }
    }

    public void Remove()
    {
        gameObject.SetActive(false); //phil: is it not better to destroy the gameObject?
    }


    void Start()
    {
        anim = GetComponent<Animator>(); //phil: added this so the gameobject retrieves the animator automatically
    }
}
