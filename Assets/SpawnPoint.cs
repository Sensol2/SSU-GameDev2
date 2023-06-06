using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    void Start()
    {
        GameObject.Find("Player").transform.position = this.transform.position;
    }

}
