using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Samouczek : MonoBehaviour {

    public GameObject samouczek;

    public GameObject cardPos;

    public int etap = 0;

    public Sprite s1;
    public Sprite s2;
    public Sprite s3;
    public Sprite s4;
    public Sprite s5;

    public Sprite tarcza;

    public Sprite przeciwnik;

    public GameObject myCard;

    public GameObject card;

    public GameObject nextCard;

    public GameObject karta2;

    public GameObject[] walka;

    public GameObject pomin;

    public Sprite pajak;

    Color alpha;
    float timer;

	// Use this for initialization
	void Start () {
        alpha = card.GetComponent<Image>().color;
        alpha.a = 255;
	}
	
	// Update is called once per frame
	void Update () {
        if(etap == 5)
        {
            timer += Time.deltaTime;
            if (timer > 3)
            {
                PlayerPrefs.SetInt("Samouczek", 1);
                Application.LoadLevel(2);
            }
        }
	}

    public void Drop()
    {
        if(etap == 2)
        {

        }
        if (etap == 1)
        {
            myCard.transform.position = cardPos.transform.position;
            myCard.GetComponent<Image>().sprite = tarcza;
            karta2.active = true;
            samouczek.GetComponent<Image>().sprite = s2;
            etap = 2;
        }
        if (etap == 0)
        {
            myCard.GetComponent<Image>().color = alpha;
            Destroy(card);
            samouczek.GetComponent<Image>().sprite = s1;
            nextCard.active = true;
            etap = 1;
        }

    }

    public void Pomin()
    {
        if (etap == 2)
        {
            nextCard.GetComponent<Image>().sprite = przeciwnik;
            walka[0].active = true;
            walka[1].active = true;
            samouczek.GetComponent<Image>().sprite = s3;
            pomin.active = false;
            etap = 3;

        }
    }

    public void Walka()
    {
        if(etap == 3)
        {
            nextCard.GetComponent<Image>().sprite = pajak;
            samouczek.GetComponent<Image>().sprite = s4;
            etap = 4;
        }
    }

    public void Monety()
    {
        if(etap == 4)
        {
            samouczek.GetComponent<Image>().sprite = s5;
            etap = 5;
        }
    }
}
