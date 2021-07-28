using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript : MonoBehaviour {

	static Rigidbody rb;
	public static Vector3 diceVelocity;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		diceVelocity = rb.velocity;
	}

    public void throwDice()
    {
        DiceNumberTextScript.diceNumber = 0;
        float dirX = Random.Range(0, 500);
        float dirY = Random.Range(0, 500);
        float dirZ = Random.Range(0, 500);
        transform.position = new Vector3(100, 2, 0);
        transform.rotation = Quaternion.identity;
        rb.AddForce(transform.up * 500);
        rb.AddTorque(dirX, dirY, dirZ);
    }

    public void hideUI()
    {
        bool aux = GameObject.Find("CanvasUI").GetComponent<Canvas>().enabled;
        if (aux)
        {
            GameObject.Find("CanvasUI").GetComponent<Canvas>().enabled = false;
            return;
        }
        if (aux == false)
        {
            GameObject.Find("CanvasUI").GetComponent<Canvas>().enabled = true;
        }
    }
}
