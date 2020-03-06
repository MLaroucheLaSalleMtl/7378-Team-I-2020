using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHandler : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject engineer;
    [SerializeField] private GameObject sphere;
    [SerializeField] private Rigidbody rb;
    [Range(1f,100f)] public float speed;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        engineer = GameObject.FindGameObjectWithTag(GameManager.engineerTag);
        sphere = GameObject.FindGameObjectWithTag(GameManager.sphereTag);
        rb.isKinematic = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == GameManager.engineerTag)
        {
            if(Input.GetButtonDown("Action4"))
            {
                engineer.transform.parent = gameObject.transform;
                sphere.transform.parent = gameObject.transform;
                engineer.SetActive(false);
                sphere.SetActive(false);
                rb.isKinematic = false;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                anim.SetBool("Close", true);
                anim.SetTrigger("Movefwd");
            }
        }
        else if (other.gameObject.tag == GameManager.sphereTag)
        {
            FindObjectOfType<AIUI>().ShowText("Only the Engineer can drive the car");
        }
    }
}
