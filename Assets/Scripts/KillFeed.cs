using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFeed : MonoBehaviour
{

    [SerializeField]
    GameObject killfeeditemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.onPlayerKilledCallback += OnKill;
    }

    public void OnKill(string player, string source) {

        //Debug.Log(source + " killed " + player);
        GameObject go = (GameObject)Instantiate(killfeeditemPrefab, this.transform);
        go.GetComponent<KillFeedItem>().Setup(player, source);

        Destroy(go, 2f);

    }
}
