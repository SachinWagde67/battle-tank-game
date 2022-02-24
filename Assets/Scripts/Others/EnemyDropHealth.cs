using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyDropHealth : MonoBehaviour
{
    [SerializeField] private float rotSpeed;

    public static bool isDestroyed = false;


    void Update()
    {
        transform.Rotate(Vector3.up, rotSpeed * Time.deltaTime);
    }

    public static void destroyDropHealth(GameObject obj)
    {
        Destroy(obj);
    }
}
