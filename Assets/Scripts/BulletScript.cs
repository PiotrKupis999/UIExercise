using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    GameObject gameManager;
    GameController gameController;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameController = gameManager.GetComponent<GameController>();
    }

    private void OnTriggerEnter(Collider _other)
    {
        //the barrel does not have 'block' name
        if (_other.name == "Block(Clone)")
        {
            var currentHp = _other.gameObject.GetComponent<BlockScript>().hp;

            if (currentHp > 1)
            {
                gameController.SpawnTheBlock(currentHp - 1, true); //spawning block with damage
            }

            Destroy(_other.gameObject); //destroying the hit block
            Destroy(this.gameObject); //destroying the bullet
        }
    }






}
