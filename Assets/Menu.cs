using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour {
    public bool on = true;

    public bool enter;

    public Sprite checkTrue;

    public Sprite checkFalse;

    public AudioSource audio;
	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("Dzwiek", 1);
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void Checkbox()
    {
        on = !on;
        if (on == true)
        {
            gameObject.GetComponent<Image>().sprite = checkTrue;
            audio.enabled = true;
            PlayerPrefs.SetInt("Dzwiek", 1);
            
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = checkFalse;
            audio.enabled = false;
            PlayerPrefs.SetInt("Dzwiek", 0);
        }
    }

    public void Wejscie()
    {
        enter = !enter;
        if(enter == true)
        {
            transform.parent.GetComponent<Animator>().SetBool("Wejscie", true);
        }
        else
        {
            transform.parent.GetComponent<Animator>().SetBool("Wejscie", false);
        }
    }

    public void NowaGra()
    {
        if(PlayerPrefs.GetInt("Samouczek") == 0)
        Application.LoadLevel(1);
        else
        Application.LoadLevel(2);
    }
}
