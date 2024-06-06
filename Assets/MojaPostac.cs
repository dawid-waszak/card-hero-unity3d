using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MojaPostac : MonoBehaviour {

    public int punktyZycia = 10;

    public int maxHp = 10;

    public int monety = 0;

    public GameObject zycieText;

    public GameObject monetyText;

    public int tarcza = 0;

    public GameObject bonus;

    public Sprite[] ikonyTarczy;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        monetyText.GetComponent<Text>().text = monety + "";
        zycieText.GetComponent<Text>().text = punktyZycia + "";
        if(punktyZycia > maxHp)
        {
            punktyZycia = maxHp;
        }
        if(tarcza <= 0)
        {
            bonus.active = false;
        }
        else
        {
            bonus.GetComponent<Image>().sprite =  ikonyTarczy[tarcza - 1];
            bonus.active = true;
            
        }
	}

}
