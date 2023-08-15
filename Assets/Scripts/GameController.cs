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
        blocksNumber = PlayerPrefs.GetInt("blocksNumber"); //was set in menu

        for (int i = 0; i < blocksNumber; i++)
        {
            SpawnTheBlock(3,false); //spawning the block without any damage
        }

        WallMaker(); //spawning the walls
    }

    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Block").Length < 2) //checking number of alive (or spawning) blocks
        {
            SceneManager.LoadScene(2); //game over
        }
    }



    public void SpawnTheBlock(int _hp, bool _delay)
    {
        var blockPosition = new Vector3(Random.Range(-Camera.main.pixelWidth, Camera.main.pixelWidth), 0, Random.Range(-Camera.main.pixelHeight, Camera.main.pixelHeight)); //random position on camera view

        if (!Physics.CheckSphere(blockPosition, spawnCollisionCheckRadius)) //checking collision
        {
            var block = Instantiate(blockPrefab, blockPosition, Quaternion.identity); //spawning the block
            block.GetComponent<BlockScript>().hp = _hp;

            if (_delay) //2 seconds delay 
            {
                StartCoroutine(SpawnDelay(block));
            }

        }
        else //if collision -> try again
        {
            SpawnTheBlock(_hp, _delay);
        }
    }

    private IEnumerator SpawnDelay(GameObject _block)
    {
        MakeInvisible(_block); 
        _block.name = "InvisibleBlock"; //impossibility of being hit by bullet

        yield return new WaitForSeconds(2);

        MakeInvisible(_block); //!MakeInvisible
        _block.name = "Block(Clone)"; //possibility of being hit by bullet

    }

    private void MakeInvisible(GameObject _object)
    {
        _object.GetComponent<BlockScript>().enabled = !_object.GetComponent<BlockScript>().enabled;
        _object.GetComponent<MeshRenderer>().enabled = !_object.GetComponent<MeshRenderer>().enabled;

        _object.transform.GetChild(1).gameObject.SetActive(!_object.transform.GetChild(1).gameObject.active); //(!)deactive gun
        

    }

    private void WallMaker()
    {
        //bigger range than camera view
        Instantiate(wallPrefab, new Vector3(Camera.main.pixelWidth + 200f, 0, 0), Quaternion.Euler(0, 90, 0));
        Instantiate(wallPrefab, new Vector3(-Camera.main.pixelWidth - 200f, 0, 0), Quaternion.Euler(0, 90, 0));
        Instantiate(wallPrefab, new Vector3(0, 0, Camera.main.pixelHeight + 200f), Quaternion.identity);
        Instantiate(wallPrefab, new Vector3(0, 0, -Camera.main.pixelHeight - 200f), Quaternion.identity);
    }

}
