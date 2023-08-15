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
        //the barrel does not have 'block' tag
        if (_other.tag == "Block")
        {
            var currentHp = _other.gameObject.GetComponent<BlockScript>().hp;

            if (currentHp > 1)
            {
                gameController.SpawnTheBlock(currentHp - 1, true);
            }

            Destroy(_other.gameObject);
            Destroy(this.gameObject);
        }
    }






}
