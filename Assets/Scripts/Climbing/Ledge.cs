using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour {

    public GameObject player;
    public Rigidbody rb;
    float timer;

	// Use this for initialization
	void Start () {
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag=="Player" && timer <= 0)
        {
            Maszas();
            //player.transform.position += new Vector3(0, 2, 0);

            timer = 2;
        }
    }

    void Maszas()
    {
        //player.transform.position = Vector3.MoveTowards(player.transform.position, new Vector3(0, 2, 0), 2 * Time.deltaTime);
        player.transform.position += new Vector3(0, 2, 0);
    }
}
