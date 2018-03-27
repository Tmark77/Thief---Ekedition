using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : MonoBehaviour {

    RaycastHit hit;
    public GameObject arrow;
    float timer = 0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(this.transform.position, this.transform.forward, Color.green);
        if (Input.GetMouseButton(0))
        {
            if (timer < 0f)
            {
                Shoot();
                timer = 0.8f;
            }
        }
        timer -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject arr = Instantiate(arrow, this.transform.position, this.transform.rotation);
        Rigidbody Rig;
        Rig = arr.GetComponent<Rigidbody>();
        Rig.AddForce(transform.forward * 2500f);
        arr.transform.Translate(this.transform.forward, Space.World);

        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, 100f))
        {
            if (hit.transform.gameObject.GetComponent<ThiefObject>() != null)
            {
                ThiefObject obj = hit.transform.gameObject.GetComponent<ThiefObject>();
                //if ((obj as DynamicFieldObject).material.Soft() == false)
                //{
                //    Destroy(arr, 1f);
                //}
            }
            else if (hit.transform.gameObject.GetComponent<Creature>() != null)
            {
                ThiefObject enemy = hit.transform.gameObject.GetComponent<Creature>();
                (enemy as Creature).TakeDamage(20);
            }
        }
    }
}
