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
    

    public void LoadScene (int _sceneId)
    {
        SceneManager.LoadScene(_sceneId);
    }

    public void SetBlocksNumber (int _number)
    {
        PlayerPrefs.SetInt("blocksNumber", _number); //get in GameController
        ClearChecks(); //unclick all buttons
        CheckButton(_number); //clicked animation
        StartButtonActivation(); //active start button
    }

    private void CheckButton(int _number)
    {
        GameObject.FindGameObjectWithTag("Button" + _number.ToString()).GetComponent<RawImage>().texture = yellowBar; //making it yellow
        GameObject.FindGameObjectWithTag("Button" + _number.ToString()).transform.localScale *= 1.1f; //making it bigger
    }

    private void StartButtonActivation()
    {
        GameObject.FindGameObjectWithTag("ButtonStart").GetComponent<RawImage>().texture = greenBar; //making it green
        GameObject.FindGameObjectWithTag("ButtonStart").GetComponent<Button>().interactable = true; //set interactable
        GameObject.FindGameObjectWithTag("Dpad").GetComponent<RawImage>().enabled = true; //show the Dpad

    }

    private void ClearChecks()
    {
        //unclicking all buttons
        foreach (var item in FindObjectsOfType(typeof(Button)))
        {
            if (item.name.Contains("0"))
            {
                item.GetComponent<RawImage>().texture = blueBar; //!making it green
                item.GetComponent<Transform>().localScale = Vector3.one; //making it normal size

            }
            else //unclicking start button
            {
                item.GetComponent<RawImage>().texture = greyBar; //making it grey
                item.GetComponent<Button>().interactable = false; //!set interactable
                GameObject.FindGameObjectWithTag("Dpad").GetComponent<RawImage>().enabled = false; //!show the Dpad
            }
        }
    }
}
