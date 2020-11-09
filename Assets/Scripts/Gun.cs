using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public Transform shotOrigin;
    public ParticleSystem muzzleFlash;
    public int bullets = 30;
    public int currentbullets;
    public int mags = 4;
    public float fireRate = 15f;
    private float nextTimetoFire = 0f;

    public bool auto = false;
    // Start is called before the first frame update
    void Start()
    {
        currentbullets = bullets;
    }

    // Update is called once per frame
    void Update()
    {



        if (gameObject.GetComponentInParent<PlayerManager>().id == Client.instance.myId)
        {
            if (auto)
            {
                if (Input.GetButton("Fire1") && currentbullets > 0 && Time.time >= nextTimetoFire)
                {
                    muzzleFlash.Play();
                    ClientSend.PlayerShoot(shotOrigin.forward);
                    currentbullets--;
                    nextTimetoFire = Time.time + 1f / fireRate;
                }
              
            }
            else {
                if (Input.GetButtonDown("Fire1") && currentbullets > 0 && Time.time >= nextTimetoFire)
                {
                    muzzleFlash.Play();
                    ClientSend.PlayerShoot(shotOrigin.forward);
                    currentbullets--;
                    nextTimetoFire = Time.time + 1f / fireRate;
                }
               
            }
            if (Input.GetKeyDown(KeyCode.R) && mags > 0 && currentbullets != bullets)
            {
                currentbullets = bullets;
                mags--;
            }

            UIManager.instance.ammoText.text = currentbullets.ToString() + " / " + mags.ToString();
        }
    }
}
