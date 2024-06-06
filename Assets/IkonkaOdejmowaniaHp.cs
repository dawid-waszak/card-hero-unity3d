using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IkonkaOdejmowaniaHp : MonoBehaviour {
    public Color kolor;
    public float timer;
    public float alpha = 1f;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > 0.005f && alpha > 0)
        {
            alpha -= 0.05f;
            kolor.a = alpha;
            timer = 0;
        }
        if(alpha <= 0)
        {
            Destroy(gameObject);
        }
        gameObject.GetComponent<Image>().color = kolor;
    }
}
