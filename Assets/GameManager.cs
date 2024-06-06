using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
    public int ostatniPrzeciwnik;

    public int coIleKartPrzeciwnik = 5;

    float timer;

    bool koniec;

    public int ktoraKarta = 0;

    public int ileKart;

    public int minKarta;

    public GameObject over;

    public GameObject startPos;

    public Transform canvas;

    public int numerKarty;

    public GameObject[] karty;

    public GameObject[] przeciwnicy;

    public int los;

    public int minPrzeciwnik;

    public int maxPrzeciwnik;

    public GameObject walka;

    public GameObject aktywnaKarta;

    public GameObject[] przyciskiWalki;

    public GameObject pomin;

    public GameObject score;

    public AudioSource audio1;
    // Use this for initialization
    void Start () {
        LosujKarte();
    }
	
	// Update is called once per frame
	void Update () {
        if(PlayerPrefs.GetInt("Dzwiek") == 0)
        {
            audio1.enabled = false;
        }
        if(PlayerPrefs.GetInt("Dzwiek") == 1)
        {
            audio1.enabled = true;
        }
    if(koniec == true)
        {
            timer += Time.deltaTime;
            if(timer > 5)
            Application.LoadLevel(2);
        }
        if (gameObject.GetComponent<MojaPostac>().punktyZycia <= 0)
            GameOver();
	}
    GameObject karta;
    int maxLos = 3;
    public void LosujKarte()
    {
        pomin.active = true;
        los = Random.Range(0, maxLos);
        if (ostatniPrzeciwnik < 5)
        {
            if (los <= 3)
            {
                numerKarty = Random.Range(minKarta, ileKart);
                karta = (GameObject)Instantiate(karty[numerKarty].gameObject, new Vector3(startPos.transform.position.x, startPos.transform.position.y, startPos.transform.position.z), Quaternion.identity, canvas);
                karta.transform.localScale = startPos.transform.localScale;
            }
            else if (los == 4)
            {
                numerKarty = Random.Range(0, karty.Length);
                karta = (GameObject)Instantiate(karty[numerKarty].gameObject, new Vector3(startPos.transform.position.x, startPos.transform.position.y, startPos.transform.position.z), Quaternion.identity, canvas);
                karta.transform.localScale = startPos.transform.localScale;
            }
            else if (los > 4 && ostatniPrzeciwnik != 0)
            {
                numerKarty = Random.Range(minPrzeciwnik, maxPrzeciwnik);
                karta = (GameObject)Instantiate(przeciwnicy[numerKarty].gameObject, new Vector3(startPos.transform.position.x, startPos.transform.position.y, startPos.transform.position.z), Quaternion.identity, canvas);
                karta.transform.localScale = startPos.transform.localScale;
            }
            else
            {
                LosujKarte();
            }
            aktywnaKarta = karta;
        }
        else
        {
            numerKarty = Random.Range(minPrzeciwnik, maxPrzeciwnik);
            karta = (GameObject)Instantiate(przeciwnicy[numerKarty].gameObject, new Vector3(startPos.transform.position.x, startPos.transform.position.y, startPos.transform.position.z), Quaternion.identity, canvas);
            karta.transform.localScale = startPos.transform.localScale;
        }
        if (karta.tag == "Przeciwnik")
        {
            if (GameObject.Find("Ekwipunek").GetComponent<Inventory>().iloscPrzedmiotow > 0)
            {
                przyciskiWalki[0].transform.SetAsLastSibling();
                przyciskiWalki[1].transform.SetAsLastSibling();
                przyciskiWalki[0].active = true;
                przyciskiWalki[1].active = true;
                pomin.active = false;
                walka.GetComponent<Walka>().kartaPrzeciwnika = karta;
            }
            else
            {
                GameOver();
            }
            ostatniPrzeciwnik = 0;
        }
        else
            ostatniPrzeciwnik++;
        ktoraKarta += 1;
        if (ktoraKarta > 5)
        {
            maxLos = 7;
            ileKart = 5;
        }
        if (ktoraKarta > 15)
        {
            maxPrzeciwnik = 2;
            ileKart = 7;
        }
        if(ktoraKarta > 40)
        {
            minPrzeciwnik = 2;
            maxPrzeciwnik = 4;
            minKarta = 7;
            ileKart = 13;
        }
    }
    public void KoniecWalki()
    {
        przyciskiWalki[0].active = false;
        przyciskiWalki[1].active = false;
        LosujKarte();
    }

    public void PominKarte()
    {
        Destroy(aktywnaKarta);
        LosujKarte();
    }

    public void GameOver()
    {
        over.transform.SetAsLastSibling();
        score.transform.SetAsLastSibling();
        over.gameObject.active = true;
        score.GetComponent<Text>().text = "WYNIK: " + ktoraKarta;
        if (PlayerPrefs.GetInt("Wynik") > 0)
            score.GetComponent<Text>().text += "\nNajlepszy wynik: " + PlayerPrefs.GetInt("Wynik");
        if (ktoraKarta > PlayerPrefs.GetInt("Wynik"))
            PlayerPrefs.SetInt("Wynik", ktoraKarta);
        score.active = true;
        koniec = true;
    }


}
