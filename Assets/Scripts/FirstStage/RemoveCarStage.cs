using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveCarStage : MonoBehaviour
{
    [SerializeField] private GameObject platform;

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Car")
        {
            gameObject.SetActive(false);
            platform.transform.position = new Vector3(0, 0);
            FindObjectOfType<AIUI>().ShowText("Now you got permission to access <Mystery Box>.");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
