using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();
    public static Dictionary<int, ItemSpawner> itemSpawners = new Dictionary<int, ItemSpawner>();
    public static Dictionary<int, GunSpawner> gunSpawners = new Dictionary<int, GunSpawner>();
    public static Dictionary<int, ProjectileManager> projectiles = new Dictionary<int, ProjectileManager>();

    public List<GameObject> guns = new List<GameObject>();
    //public List<GameObject> gunSpawnerPrefab = new List<GameObject>();


    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;
    public GameObject itemSpawnerPrefab;
    //public GameObject gunSpawnerPrefab;
    public GameObject projectilePrefab;

    [SerializeField]
    public delegate void OnPlayerKilledCallback(string player, string source);
    public OnPlayerKilledCallback onPlayerKilledCallback;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }


    public void SpawnPlayer(int _id, string _username, Vector3 _position, Quaternion _rotation) {

        GameObject _player;
        if (_id == Client.instance.myId)
        {

            _player = Instantiate(localPlayerPrefab, _position, _rotation);

        }
        else {

            _player = Instantiate(playerPrefab, _position, _rotation);
        }

        _player.GetComponent<PlayerManager>().Initialize(_id, _username);
        players.Add(_id, _player.GetComponent<PlayerManager>());

    }

    public void CreateItemSpawner(int _spawnerId, Vector3 _position, bool _hasItem) {

        GameObject _spawner = Instantiate(itemSpawnerPrefab, _position, itemSpawnerPrefab.transform.rotation);
        _spawner.GetComponent<ItemSpawner>().Initialize(_spawnerId, _hasItem);
        itemSpawners.Add(_spawnerId, _spawner.GetComponent<ItemSpawner>());


    }

    public void CreateGunSpawner(int _spawnerId, Vector3 _position, bool _hasItem, int _gun) {
        GameObject _spawner = Instantiate(guns[_gun], _position, guns[_gun].transform.rotation);
        _spawner.GetComponent<GunSpawner>().Initialize(_spawnerId, _hasItem);
        gunSpawners.Add(_spawnerId, _spawner.GetComponent<GunSpawner>());
    }

    public void SpawnProjectile(int _id, Vector3 _position) {

        GameObject _projectile = Instantiate(projectilePrefab, _position, Quaternion.identity);
        _projectile.GetComponent<ProjectileManager>().Initialize(_id);
        projectiles.Add(_id, _projectile.GetComponent<ProjectileManager>());
    }


    public void Winner(int _id) {

        if (_id == Client.instance.myId)
        {
            UIManager.instance.endingSign.SetActive(true);
            UIManager.instance.endingSign.GetComponent<Text>().text = "Victory";

        }
        else {

            UIManager.instance.endingSign.SetActive(true);
            UIManager.instance.endingSign.GetComponent<Text>().text = "Defeat";
        }

    }

  
}
