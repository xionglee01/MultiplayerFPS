using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet) {

        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;
        // TODO: send welcome received packet
        ClientSend.WelcomeReceived();

        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);

    }

    public static void SpawnPlayer(Packet _packet) {

        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.instance.SpawnPlayer(_id, _username, _position, _rotation);
    }

    public static void PlayerPosition(Packet _packet) {

        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();



        GameManager.players[_id].transform.position = _position;

    }

    public static void PlayerRotation(Packet _packet)
    {

        int _id = _packet.ReadInt();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.players[_id].transform.rotation = _rotation;

    }

    public static void PlayerDisconnected(Packet _packet) {

        int _id = _packet.ReadInt();

        Destroy(GameManager.players[_id].gameObject);
        GameManager.players.Remove(_id);

    }

    public static void PlayerHealth(Packet _packet) {

        int _id = _packet.ReadInt();
        float _health = _packet.ReadFloat();

        GameManager.players[_id].SetHealth(_health);
        if(_id == Client.instance.myId)
            UIManager.instance.hpText.text = _health.ToString();

    }

    public static void PlayerRespawned(Packet _packet) {

        int _id = _packet.ReadInt();

        GameManager.players[_id].Respawn();
    }

    public static void CreateItemSpawner(Packet _packet) {

        int _spawnerId = _packet.ReadInt();
        Vector3 _spawnerPosition = _packet.ReadVector3();
        bool _hasItem = _packet.ReadBool();

        GameManager.instance.CreateItemSpawner(_spawnerId, _spawnerPosition, _hasItem);

    }

    public static void ItemSpawned(Packet _packet) {

        int _spawnerId = _packet.ReadInt();

        GameManager.itemSpawners[_spawnerId].ItemSpawned();

    }

    public static void ItemPickedUp(Packet _packet) {

        int _spawnerId = _packet.ReadInt();
        int _byPlayer = _packet.ReadInt();

        GameManager.itemSpawners[_spawnerId].ItemPickedUp();
        GameManager.players[_byPlayer].itemCount++;
    }


    //Creates gun spawner with data received from packets
    public static void CreateGunSpawner(Packet _packet) {

        int _spawnerId = _packet.ReadInt();
        Vector3 _spawnerPosition = _packet.ReadVector3();
        bool _hasItem = _packet.ReadBool();
        int _gun = _packet.ReadInt();

        GameManager.instance.CreateGunSpawner(_spawnerId, _spawnerPosition, _hasItem, _gun);

    }


    public static void GunSpawned(Packet _packet) {
        int _spawnerId = _packet.ReadInt();

        GameManager.gunSpawners[_spawnerId].GunSpawned();
    }

    public static void GunPickedUp(Packet _packet) {

        int _spawnerId = _packet.ReadInt();
        int _byPlayer = _packet.ReadInt();
        int _gunnum = _packet.ReadInt();

        GameManager.gunSpawners[_spawnerId].GunPickedUp();
        if (_byPlayer == Client.instance.myId)
        {
            Transform localPlayer = GameManager.players[_byPlayer].weaponHolder.transform;
                GameObject gun = Instantiate(GameManager.instance.guns[_gunnum]);
            if (localPlayer.childCount < 1)
            {
                gun.transform.parent = localPlayer;
                gun.transform.localPosition = new Vector3(0, 0, 1);
                gun.transform.localRotation = Quaternion.Euler(0, 0, 0);
                gun.GetComponent<GunSpawner>().enabled = false;
                gun.GetComponent<Gun>().enabled = true;
            }
            else {
                foreach (Transform weapons in localPlayer) {
                    if (gun.tag == weapons.tag) {
                        weapons.GetComponent<Gun>().mags++;
                        Destroy(gun);
                        return;
                    }
                }
                gun.transform.parent = localPlayer;
                gun.transform.localPosition = new Vector3(0, 0, 1);
                gun.transform.localRotation = Quaternion.Euler(0, 0, 0);
                gun.GetComponent<GunSpawner>().enabled = false;
                gun.GetComponent<Gun>().enabled = true;
                gun.SetActive(false);
            }
           

        }
        else
        {
            Transform player = GameManager.players[_byPlayer].weaponHolder.transform;
            GameObject gun = Instantiate(GameManager.instance.guns[_gunnum]);
            if (player.childCount < 1)
            {
                gun.transform.parent = player;
                gun.transform.localPosition = new Vector3(0, 0, 1);
                gun.transform.localRotation = Quaternion.Euler(0, 0, 0);
                gun.GetComponent<GunSpawner>().enabled = false;
                gun.GetComponent<Gun>().enabled = true;
            }
            else {
                foreach (Transform weapons in player)
                {
                    if (gun.tag == weapons.tag)
                    {
                        weapons.GetComponent<Gun>().mags++;
                        Destroy(gun);
                        return;
                    }
                }
                gun.transform.parent = player.transform;
                gun.transform.localPosition = new Vector3(0, 0, 1);
                gun.transform.localRotation = Quaternion.Euler(0, 0, 0);
                gun.GetComponent<GunSpawner>().enabled = false;
                gun.GetComponent<Gun>().enabled = true;
                gun.SetActive(false);
            }
        }

    }


    public static void GunSelect(Packet _packet) {

        int _id = _packet.ReadInt();
        int _weaponSelected = _packet.ReadInt();

        GameManager.players[_id].SelectWeapon(_weaponSelected);
    }


    public static void SpawnProjectile(Packet _packet) {

        int _projectileId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        int _thrownByPlayer = _packet.ReadInt();

        GameManager.instance.SpawnProjectile(_projectileId, _position);
        GameManager.players[_thrownByPlayer].itemCount--;
    }


    public static void ProjectilePosition(Packet _packet) {
        int _projectileId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        GameManager.projectiles[_projectileId].transform.position = _position;
    }

    public static void ProjectileExploded(Packet _packet) {
        int _projectileId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        GameManager.projectiles[_projectileId].Explode(_position);

    }

    public static void KillFeed(Packet _packet) {

        int _Killer = _packet.ReadInt();
        int _playerKilled = _packet.ReadInt();
        int _playerKillerkills = _packet.ReadInt();
        int _playerKilleddeaths = _packet.ReadInt();

        PlayerManager killer = GameManager.players[_Killer];
        PlayerManager playerdead = GameManager.players[_playerKilled];


        GameManager.instance.onPlayerKilledCallback.Invoke(playerdead.username, killer.username);

        killer.kills = _playerKillerkills;
        playerdead.deaths = _playerKilleddeaths;

        if (_Killer == Client.instance.myId) {

            UIManager.instance.kills.text = "Kills : " + killer.kills;

        }

        if (_playerKilled == Client.instance.myId) {

            UIManager.instance.deaths.text = "Deaths : " + playerdead.deaths;

        }

    }

    public static void PlayerWinner(Packet _packet) {

        int _id = _packet.ReadInt();

        GameManager.instance.Winner(_id);
    }
}
