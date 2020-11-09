using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;
    public float health;
    public float maxHealth;
    public int itemCount = 0;
    public Text textMesh;
    public MeshRenderer model;

    public int kills = 0;
    public int deaths = 0;


    public GameObject weaponHolder;

    public void Initialize(int _id, string _username) {

        id = _id;
        username = _username;
        textMesh.text = _username;
        health = maxHealth;

    }

    public void SetHealth(float _health) {

        health = _health;

        if (health <= 0f) {

            Die();

        }

    }


    public void SelectWeapon(int selectedWeapon) {

        int i = 0;

        foreach (Transform weapon in weaponHolder.transform) {
            if (i == selectedWeapon)
            {

                weapon.gameObject.SetActive(true);
            }
            else {

                weapon.gameObject.SetActive(false);

            }
            i++;
        }
    }

    public void Die() {
        model.enabled = false;
        
    }

    public void Respawn() {

        model.enabled = true;
        SetHealth(maxHealth);
        UIManager.instance.hpText.text = maxHealth.ToString();

    }
}
