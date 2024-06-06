using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Inventory : MonoBehaviour {


    float timer;

    public GameObject Warning;

    public GameObject ilosc;

    public Transform card2;

    public int[] ekwipunek;

    public int maxItemow = 10;

    public int iloscPrzedmiotow;

    public GameObject nextCard;

    public GameObject gameManager;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if(iloscPrzedmiotow > 1)
        {
            nextCard.active = true;
            int idNastItemu = ekwipunek[iloscPrzedmiotow - 2];
                        nextCard.GetComponent<Image>().sprite = gameManager.GetComponent<GameManager>().karty[idNastItemu - 1].GetComponent<Image>().sprite;
        }
        else
            nextCard.active = false;
    }

    public void DodajPrzedmiot(int id)
    {
        iloscPrzedmiotow = 0;
        for (int i = 0; i < ekwipunek.Length; i++)
        {
            if (ekwipunek[i] > 0)
                iloscPrzedmiotow++;
        }
        if (iloscPrzedmiotow < maxItemow)
        {
            for (int i = 0; i < ekwipunek.Length; i++)
            {
                if (ekwipunek[i] == 0)
                {
                    ekwipunek[i] = id;
                    iloscPrzedmiotow++;
                    ilosc.GetComponent<Text>().text = (iloscPrzedmiotow) + "/" + maxItemow;
                    Destroy(card2.gameObject);
                    GameObject.Find("GameManager").GetComponent<GameManager>().LosujKarte(); break;
                }
            }
        }
    }

    public void AktualizujEkwipunek()
    {
        ilosc.GetComponent<Text>().text = (iloscPrzedmiotow) + "/" + maxItemow;
    }
}
