using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Card : MonoBehaviour {
    public Transform card;
    public GameObject dropZone;
    public bool drop = false;
    public Vector2 cardPos;
    public int id;
	// Use this for initialization
	void Start () {
        card = this.transform;
        cardPos = card.position;
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void Move()
    {
        card.position = Input.mousePosition;
        card.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void Drop()
    {
        card.position = cardPos;
        card.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }


}
