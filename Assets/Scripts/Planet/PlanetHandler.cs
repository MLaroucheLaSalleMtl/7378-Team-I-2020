﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Sohyun Yi & Philipe Gouveia
public class PlanetHandler : MonoBehaviour
{
    #region Spawn Attributes
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private Transform initalSpawner;

    internal static Transform spawnerPos;
    #endregion

    #region Singleton
    public static PlanetHandler instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            spawnerPos = initalSpawner;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    #endregion

    private void Start()
    {
        carPrefab.transform.position = spawnerPos.position;
    }
}
