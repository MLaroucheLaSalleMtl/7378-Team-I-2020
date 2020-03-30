using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Philipe Gouveia

public class MazeHandler : MonoBehaviour
{
    #region Singleton
    public static MazeHandler instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [SerializeField] Vector3[] rotations;
    private int index;
    public float smoothness = 0.5f;
    public int countdown = 100;
    private int temp;

    private void Start()
    {
        temp = countdown;
    }

    private void Update()
    {
        if (temp <= 0)
        {
            NewRotation();
            Debug.Log("New Rotation");
        }
        temp --;
    }

    void NewRotation()
    {
        temp = countdown;
        index = Random.Range(0, rotations.Length - 1);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(rotations[index]), Time.deltaTime);
    }
}

