﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpawner : MonoBehaviour
{
    public int spawnerId;
    public bool hasItem;
    public MeshRenderer gunModel;
    


    public float itemRotationSpeed = 50f;
    public float itemBobSpeed = 2f;
    private Vector3 basePosition;


    private void Update()
    {
        if (hasItem)
        {

            transform.Rotate(Vector3.up, itemRotationSpeed * Time.deltaTime, Space.World);
            transform.position = basePosition + new Vector3(0f, 0.25f * Mathf.Sin(Time.time * itemBobSpeed));

        }
    }


    public void Initialize(int _spawnerId, bool _hasItem)
    {

        spawnerId = _spawnerId;
        hasItem = _hasItem;
        gunModel.enabled = _hasItem;

        basePosition = transform.position;

    }

    public void GunSpawned()
    {

        hasItem = true;
        gunModel.enabled = true;
    }

    public void GunPickedUp()
    {
        hasItem = false;
        gunModel.enabled = false;
        
    }
}
