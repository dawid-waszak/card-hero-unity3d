using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MyCard : MonoBehaviour {
    public Transform card;
    public GameObject gameManager;
    public GameObject ekwipunek;
    public GameObject defaultPos;
    public Vector2 pos;
    public int id;
    Color alpha;

    public GameObject[] ikonyTarczy;

    public GameObject[] ikonyZycia;

    public AudioClip potion;

    public AudioClip item;

    public GameObject odejmowaniePos;

    Color solid;
    // Use this for initialization
    void Start () {
        ekwipunek = GameObject.Find("Ekwipunek");
        pos = transform.position;
        transform.position = defaultPos.transform.position;
        alpha = gameObject.GetComponent<Image>().color;
        solid = gameObject.GetComponent<Image>().color;
        alpha.a = 0;
        solid.a = 255;
	}
	
	// Update is called once per frame
	void Update () {
        if (ekwipunek.GetComponent<Inventory>().iloscPrzedmiotow == 0)
            gameObject.GetComponent<Image>().color = alpha;
        else if (ekwipunek.GetComponent<Inventory>().iloscPrzedmiotow == 1)
        {
            gameObject.GetComponent<Image>().color = solid;
            transform.position = defaultPos.transform.position;
            int itemy = ekwipunek.GetComponent<Inventory>().iloscPrzedmiotow - 1;
            int id = ekwipunek.GetComponent<Inventory>().ekwipunek[itemy] - 1;
            gameObject.GetComponent<Image>().sprite = gameManager.GetComponent<GameManager>().karty[id].GetComponent<Image>().sprite;
        }
        else if (ekwipunek.GetComponent<Inventory>().iloscPrzedmiotow > 1)
        {
            gameObject.transform.position = pos;
            int itemy = ekwipunek.GetComponent<Inventory>().iloscPrzedmiotow - 1;
            int id = ekwipunek.GetComponent<Inventory>().ekwipunek[itemy] - 1;
            gameObject.GetComponent<Image>().sprite = gameManager.GetComponent<GameManager>().karty[id].GetComponent<Image>().sprite;
        }
        if (card == null)
        {
            if (!GameObject.FindGameObjectWithTag("Card"))
                card = GameObject.FindGameObjectWithTag("Przeciwnik").transform;
            else
                card = GameObject.FindGameObjectWithTag("Card").transform;
        }
        
    }

    public void onDrop()
    {
        if ((int)card.GetComponent<StatystykiPrzedmiotu>().umiejetnosc == 0 )
        {
            ekwipunek.GetComponent<Inventory>().card2 = card;
            ekwipunek.GetComponent<Inventory>().DodajPrzedmiot(card.GetComponent<Card>().id);
            id = card.GetComponent<Card>().id;
            gameObject.GetComponent<AudioSource>().PlayOneShot(item);
        }
        else if((int)card.GetComponent<StatystykiPrzedmiotu>().umiejetnosc == 1)
        {
            int tarcza = card.GetComponent<StatystykiPrzedmiotu>().obrona;
            gameManager.GetComponent<MojaPostac>().tarcza = card.GetComponent<StatystykiPrzedmiotu>().obrona;
            Destroy(card.gameObject);
            gameManager.GetComponent<GameManager>().LosujKarte();
            gameObject.GetComponent<AudioSource>().PlayOneShot(potion);
            //wyswietlanie ikonki +1 tarczy
                GameObject tarczaG = (GameObject)Instantiate(ikonyTarczy[tarcza - 1], new Vector3(odejmowaniePos.transform.position.x, odejmowaniePos.transform.position.y), Quaternion.identity, GameObject.Find("Canvas").transform);
                tarczaG.transform.localScale = odejmowaniePos.transform.localScale;
                tarczaG.transform.SetAsLastSibling();
            //
        }
        else if ((int)card.GetComponent<StatystykiPrzedmiotu>().umiejetnosc == 2)
        {
            int zycie = card.GetComponent<StatystykiPrzedmiotu>().zycie;
            gameManager.GetComponent<MojaPostac>().punktyZycia += card.GetComponent<StatystykiPrzedmiotu>().zycie;
            Destroy(card.gameObject);
            gameManager.GetComponent<GameManager>().LosujKarte();
            gameObject.GetComponent<AudioSource>().PlayOneShot(potion);
            //wyswietlanie ikonki +1 tarczy
            GameObject zycieG = (GameObject)Instantiate(ikonyZycia[zycie - 1], new Vector3(odejmowaniePos.transform.position.x, odejmowaniePos.transform.position.y), Quaternion.identity, GameObject.Find("Canvas").transform);
            zycieG.transform.localScale = odejmowaniePos.transform.localScale;
            zycieG.transform.SetAsLastSibling();
            //
        }
        else
        {
            ekwipunek.GetComponent<Inventory>().card2 = card;
            ekwipunek.GetComponent<Inventory>().DodajPrzedmiot(card.GetComponent<Card>().id);
            id = card.GetComponent<Card>().id;
            gameObject.GetComponent<AudioSource>().PlayOneShot(item);
        }
    }
    


}
