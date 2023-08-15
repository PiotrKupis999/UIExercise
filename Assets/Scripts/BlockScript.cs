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
    public int hp;


    private float second = 0;
    private float rotateTime = 0;


    [SerializeField]
    private Transform barrel;


    private void Start()
    {


        if (hp==2)
        {
            GetComponent<Renderer>().material.color = new Color(1, 0.4273585f, 0.4273585f);

        }
        if (hp == 1)
        {
            GetComponent<Renderer>().material.color = new Color(1, 0.159434f, 0.159434f);
        }

    }

    void Update()
    {



        TimeAdd();

        //random rotating the block
        if (rotateTime > UnityEngine.Random.Range(0f,1f))
        {
            RotateTheBlock();
            rotateTime = 0f;
        }

        //shooting
        if (second > 1f)
        {
            Shoot();
            second = 0f;
        }

    }


    private void RotateTheBlock()
    {
        var randomRotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
        transform.rotation = randomRotation;
    }

    private void Shoot()
    {
        bullet = Instantiate(bulletPrefab, barrel.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = barrel.forward * bulletSpeed;
    }

    private void TimeAdd()
    {
        second += Time.deltaTime;
        rotateTime += Time.deltaTime;
    }
    
}
