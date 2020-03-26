﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//by Sohyun Yi

public class TerrainLoader : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == GameManager.carTag)
        {
            SceneManager.LoadScene("Planet");
        }
    }
}
