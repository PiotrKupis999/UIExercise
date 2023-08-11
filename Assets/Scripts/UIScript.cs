using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Texture yellowBar;
    public Texture blueBar;
    public Texture greyBar;
    public Texture greenBar;
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LoadScene (int sceneId)
    {
        SceneManager.LoadScene(sceneId);
        /*
        if (sceneId == 0)
        {
            ClearChecks();

        }
        */
    }

    public void SetBlocksNumber (int number)
    {
        PlayerPrefs.SetInt("blocksNumber", number);
        ClearChecks();
        CheckButton(number);
        StartButtonActivation();

    }

    private void CheckButton(int number)
    {
        GameObject.FindGameObjectWithTag("Button" + number.ToString()).GetComponent<RawImage>().texture = yellowBar;
        GameObject.FindGameObjectWithTag("Button" + number.ToString()).transform.localScale *= 1.1f;
    }

    private void StartButtonActivation()
    {
        GameObject.FindGameObjectWithTag("ButtonStart").GetComponent<RawImage>().texture = greenBar;
        GameObject.FindGameObjectWithTag("ButtonStart").GetComponent<Button>().interactable = true;
        GameObject.FindGameObjectWithTag("Dpad").GetComponent<RawImage>().enabled = true;

    }

    private void ClearChecks()
    {
        foreach (var item in FindObjectsOfType(typeof(Button)))
        {
            if (item.name.Contains("0"))
            {
                item.GetComponent<RawImage>().texture = blueBar;
                item.GetComponent<Transform>().localScale = Vector3.one;

            }
            else
            {
                item.GetComponent<RawImage>().texture = greyBar;
                item.GetComponent<Button>().interactable = false;

            }
        }

        GameObject.FindGameObjectWithTag("Dpad").GetComponent<RawImage>().enabled = false;

    }

}
