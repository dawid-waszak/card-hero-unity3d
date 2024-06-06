using UnityEngine;
using System.Collections;

public class StatystykiPrzedmiotu : MonoBehaviour {
    public int obrazenia;
    public int obrona;
    public int zycie;
    public int maxMonet;
    public enum specjalnaUmiejetnosc{ NONE, tarcza, zycie, dystans, losowa, ogluszenie, odbijanieObrazens};
    public int bonusObrazen;
    public string typPrzeciwnika = "";
    public specjalnaUmiejetnosc umiejetnosc;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}
}
