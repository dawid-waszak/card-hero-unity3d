using UnityEngine;
using System.Collections;

public class Walka : MonoBehaviour {
    public GameObject ekwipunek;

    public GameObject gameManager;

    public GameObject kartaGracza;

    public GameObject kartaPrzeciwnika;

    public enum specjalnaUmiejetnosc { NONE, tarcza, atak };

    public int mojAtak;

    public int mojaObrona;

    public int atakPrzeciwnika;
    
    public AudioClip coins;

    public AudioClip walka;

    public AudioClip smierc;

    public int obronaPrzeciwnika;

    public GameObject ogluszenieSprite;

    public GameObject[] odejmowanieHp;

    public GameObject odejmowaniePos;

    public bool ogluszenie;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public int id;
    public void Walcz()
    {
        mojAtak = 0;
        mojaObrona = 0;
        atakPrzeciwnika = 0;
        obronaPrzeciwnika = 0;
        //pobieranie obrazen mojej postaci i przeciwnika
        int przedmiot = ekwipunek.GetComponent<Inventory>().iloscPrzedmiotow - 1;
        id = ekwipunek.GetComponent<Inventory>().ekwipunek[przedmiot];
        kartaGracza = gameManager.GetComponent<GameManager>().karty[id - 1];
        if ((int)kartaGracza.GetComponent<StatystykiPrzedmiotu>().umiejetnosc != 4)
        {
            ekwipunek.GetComponent<Inventory>().ekwipunek[przedmiot] = 0;
            ekwipunek.GetComponent<Inventory>().iloscPrzedmiotow--;
        }
        else
        {
            ekwipunek.GetComponent<Inventory>().ekwipunek[przedmiot] = Random.Range(1, 3);
        }
        int umiejetnosc = (int)kartaGracza.GetComponent<StatystykiPrzedmiotu>().umiejetnosc;
        mojAtak = kartaGracza.GetComponent<StatystykiPrzedmiotu>().obrazenia;
        mojaObrona = kartaGracza.GetComponent<StatystykiPrzedmiotu>().obrona + gameManager.GetComponent<MojaPostac>().tarcza;
        gameManager.GetComponent<MojaPostac>().tarcza = 0;
        //Umiejetnosci specjalne
        if (kartaGracza.GetComponent<StatystykiPrzedmiotu>().typPrzeciwnika == "Dystans" && kartaPrzeciwnika.GetComponent<StatystykiPrzedmiotu>().typPrzeciwnika == "Dystans")
        {
            mojAtak += kartaGracza.GetComponent<StatystykiPrzedmiotu>().bonusObrazen;
        }
        if ((int)kartaGracza.GetComponent<StatystykiPrzedmiotu>().umiejetnosc == 5)
        {
            mojAtak = kartaGracza.GetComponent<StatystykiPrzedmiotu>().bonusObrazen; 
        }
        //
            atakPrzeciwnika = kartaPrzeciwnika.GetComponent<StatystykiPrzedmiotu>().obrazenia;
        obronaPrzeciwnika = kartaPrzeciwnika.GetComponent<StatystykiPrzedmiotu>().obrona;
        //obliczanie obrazen
        int obrazeniaDlaPrzeciwnika = mojAtak - obronaPrzeciwnika;
        int obrazeniaDlaMnie = atakPrzeciwnika - mojaObrona;
        if ((int)kartaGracza.GetComponent<StatystykiPrzedmiotu>().umiejetnosc == 6)
        {
            obrazeniaDlaPrzeciwnika += kartaGracza.GetComponent<StatystykiPrzedmiotu>().bonusObrazen;
        }
            if (ogluszenie == true)
        {
            obrazeniaDlaMnie = 0;
        }
        if (obrazeniaDlaMnie > 0)
        {
            gameManager.GetComponent<MojaPostac>().punktyZycia -= obrazeniaDlaMnie;
        }
        
        if (obrazeniaDlaPrzeciwnika > 0 && umiejetnosc != 1)
        {
            kartaPrzeciwnika.GetComponent<StatystykiPrzedmiotu>().zycie -= obrazeniaDlaPrzeciwnika;
        }
        ekwipunek.GetComponent<Inventory>().AktualizujEkwipunek();

        //jesli przeciwnik nie zyje losowana jest nowa karta, jesli zyje walka trwa
        if(kartaPrzeciwnika.GetComponent<StatystykiPrzedmiotu>().zycie <= 0)
        {
            int monety = Random.Range(kartaPrzeciwnika.GetComponent<StatystykiPrzedmiotu>().maxMonet / 2, kartaPrzeciwnika.GetComponent<StatystykiPrzedmiotu>().maxMonet);
            gameManager.GetComponent<MojaPostac>().monety += monety;
            Destroy(kartaPrzeciwnika);
            gameManager.GetComponent<GameManager>().KoniecWalki();
            //wyswietlanie ikonki odejmowania hp
            if (obrazeniaDlaPrzeciwnika > 0)
            {
                GameObject odejmowanie = (GameObject)Instantiate(odejmowanieHp[obrazeniaDlaPrzeciwnika - 1], new Vector3(odejmowaniePos.transform.position.x, odejmowaniePos.transform.position.y), Quaternion.identity, GameObject.Find("Canvas").transform);
                odejmowanie.transform.localScale = odejmowaniePos.transform.localScale;
                odejmowanie.transform.SetAsLastSibling();
            }
            //
            gameObject.GetComponent<AudioSource>().PlayOneShot(smierc);
            ogluszenie = false;
            ogluszenieSprite.active = false;
        }
        else
        {
            //wyswietlanie ikonki odejmowania hp
            if (obrazeniaDlaPrzeciwnika > 0)
            {
                GameObject odejmowanie = (GameObject)Instantiate(odejmowanieHp[obrazeniaDlaPrzeciwnika - 1], new Vector3(odejmowaniePos.transform.position.x, odejmowaniePos.transform.position.y), Quaternion.identity, GameObject.Find("Canvas").transform);
                odejmowanie.transform.localScale = odejmowaniePos.transform.localScale;
                odejmowanie.transform.SetAsLastSibling();
            }
            //
            if ((int)kartaGracza.GetComponent<StatystykiPrzedmiotu>().umiejetnosc == 5)
            {
                ogluszenie = true;
                ogluszenieSprite.transform.SetAsLastSibling();
                ogluszenieSprite.active = true;
            }
            else
            {
                ogluszenie = false;
                ogluszenieSprite.active = false;
            }
            gameObject.GetComponent<AudioSource>().PlayOneShot(walka);
            if (ekwipunek.GetComponent<Inventory>().iloscPrzedmiotow <= 0 && gameManager.GetComponent<MojaPostac>().monety < 50)
            {
                gameManager.GetComponent<GameManager>().GameOver();
            }
        }
    }

    public void Omin()
    {
        if(gameManager.GetComponent<MojaPostac>().monety >= 50)
        {
            Destroy(kartaPrzeciwnika);
            gameManager.GetComponent<GameManager>().KoniecWalki();
            gameManager.GetComponent<MojaPostac>().monety -= 50;
            gameObject.GetComponent<AudioSource>().PlayOneShot(coins);
        }
    }
}
