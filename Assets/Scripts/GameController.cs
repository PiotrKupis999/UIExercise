using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject blockPrefab;
    public GameObject wallPrefab;
    public float spawnCollisionCheckRadius;

    private int blocksNumber;
    private bool spawning;

    void Awake()
    {
        blocksNumber = PlayerPrefs.GetInt("blocksNumber");

        for (int i = 0; i < blocksNumber; i++)
        {
            SpawnTheBlock(3,false);
        }

        WallMaker();
    }

    void Update()
    {
        Debug.Log(GameObject.FindGameObjectsWithTag("Block").Length);

        if (GameObject.FindGameObjectsWithTag("Block").Length < 2)
        {
            SceneManager.LoadScene(2);
        }
    }



    public void SpawnTheBlock(int _hp, bool _delay)
    {
        var blockPosition = new Vector3(Random.Range(-Camera.main.pixelWidth, Camera.main.pixelWidth), 0, Random.Range(-Camera.main.pixelHeight, Camera.main.pixelHeight));

        if (!Physics.CheckSphere(blockPosition, spawnCollisionCheckRadius))
        {
            var block = Instantiate(blockPrefab, blockPosition, Quaternion.identity);
            block.GetComponent<BlockScript>().hp = _hp;

            if (_delay)
            {
                StartCoroutine(SpawnDelay(block));
            }

        }
        else
        {
            SpawnTheBlock(_hp, _delay);
        }
    }

    private IEnumerator SpawnDelay(GameObject _block)
    {
        MakeInvisible(_block);
        _block.tag = "InvisibleBlock";


        yield return new WaitForSeconds(2);

        MakeInvisible(_block);
        _block.tag = "Block";

    }

    private void MakeInvisible(GameObject _object)
    {
        _object.GetComponent<BlockScript>().enabled = !_object.GetComponent<BlockScript>().enabled;
        _object.GetComponent<MeshRenderer>().enabled = !_object.GetComponent<MeshRenderer>().enabled;
        //_object.GetComponent<BoxCollider>().enabled = !_object.GetComponent<BoxCollider>().enabled;

        _object.transform.GetChild(1).gameObject.SetActive(!_object.transform.GetChild(1).gameObject.active);
        

    }

    private void WallMaker()
    {
        Instantiate(wallPrefab, new Vector3(Camera.main.pixelWidth + 200f, 0, 0), Quaternion.Euler(0, 90, 0));
        Instantiate(wallPrefab, new Vector3(-Camera.main.pixelWidth - 200f, 0, 0), Quaternion.Euler(0, 90, 0));
        Instantiate(wallPrefab, new Vector3(0, 0, Camera.main.pixelHeight + 200f), Quaternion.identity);
        Instantiate(wallPrefab, new Vector3(0, 0, -Camera.main.pixelHeight - 200f), Quaternion.identity);
    }

}
