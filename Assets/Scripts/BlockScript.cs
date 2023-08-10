using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BlockScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    private GameObject bullet;

    public float bulletSpeed;

    private float second = 0;
    private float rotateTime = 0;

    [SerializeField]
    private Transform barrel;


    void Update()
    {
        TimeAdd();

        //random rotating the block
        if (rotateTime > UnityEngine.Random.Range(0f,1f))
        {

            var randomRotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360),  0);
            transform.rotation = randomRotation;
            rotateTime = 0f;

        }

        //shooting
        if (second > 1f)
        {

            bullet = Instantiate(bulletPrefab, barrel.position, transform.rotation);
            bullet.GetComponent<Rigidbody>().velocity = barrel.forward * bulletSpeed;
            second = 0f;

        }

    }

    private void TimeAdd()
    {
        second += Time.deltaTime;
        rotateTime += Time.deltaTime;
    }
    
}
