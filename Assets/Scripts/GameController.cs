using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject blockPrefab;
    public float spawnCollisionCheckRadius;

    private int blocksNumber;

    void Awake()
    {
        blocksNumber = PlayerPrefs.GetInt("blocksNumber");

        for (int i = 0; i < blocksNumber; i++)
        {
            SpawnTheBlock(3);
        }

    }

    void Update()
    {
        Debug.Log(GameObject.FindGameObjectsWithTag("Block").Length);

        if (GameObject.FindGameObjectsWithTag("Block").Length < 2)
        {
            SceneManager.LoadScene(2);
        }
    }



    public void SpawnTheBlock(int _hp)
    {
        var randomPosition = new Vector3(Random.Range(-750f, 750f), 0, Random.Range(-350f, 500f));
        var blockPosition = transform.position + randomPosition;

        if (!Physics.CheckSphere(blockPosition, spawnCollisionCheckRadius))
        {
            var block = Instantiate(blockPrefab, blockPosition, Quaternion.identity);
            block.GetComponent<BlockScript>().hp = _hp;

        }
        else
        {
            SpawnTheBlock(_hp);
        }
    }

}
