using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapShooter : MonoBehaviour
{
    public Transform bulletSpawn;
    public GameObject Prefab;
    public float Power;

    public void Shoot()
    {
        Instantiate(Prefab, bulletSpawn.position, bulletSpawn.rotation);
    }
}
