using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsInShadow : MonoBehaviour {

    public Transform player;
    public Transform head;
    public Transform chest;
    public Transform foot;

    bool hH; //head is in 
    bool hC; //chest is in shadow
    bool hF; //foot is in shadow

	int vH; //head visivility
	int vC; //chest visivility
	int vF; //foot visivility

    int i; //index
    int fullDark; //teljes árnyék
    public int range;

	float distance; // segédváltozó
	public Text LightGemTemp; //csak addig kell, amíg nincs light gem

	public Light[] lights;
    public bool fullHide;

	// Use this for initialization
	void Start () {
        hH = false;
        hC = false;
        hF = false;
    }
	
	// Update is called once per frame
	void Update () {

		vH = 0;
		vC = 0;
		vF = 0;
	
        for (i = 0; i < lights.Length; i++)
        {
			distance = Vector3.Distance(lights[i].transform.position, player.position);
			if (lights[i].range >= distance) {
				if(VanRalatas(head, i)){
					distance = Vector3.Distance(lights[i].transform.position, head.position);
					vH = FenyerossegSzamitas(lights[i].range, distance, vH);
				}
				if(VanRalatas(chest, i)){
					distance = Vector3.Distance(lights[i].transform.position, chest.position);
					vC = FenyerossegSzamitas(lights[i].range, distance, vC);
				}
				if(VanRalatas(foot, i)){
					distance = Vector3.Distance(lights[i].transform.position, foot.position);
					vF = FenyerossegSzamitas(lights[i].range, distance, vF);
				}
			}
           // if(Hide(i) == true)
           // {
           //     fullDark++;
           // }

        }

		LightGemTemp.text = "Head: " + vH + " Chest: " + vC + " Foot:" + vF + "Overall: "+ (vH+vC+vF)/3;
      //  if(fullDark == lights.Length)
      //  {
      //      fullHide = true;
      //  }
      //  else
      //  {
      //      fullHide = false;
      //  }
      //  fullDark = 0;
      //  i = 0;
    }

	bool VanRalatas (Transform obj, int index)
	{
		RaycastHit hit;
		Physics.Linecast (lights [index].transform.position, obj.position, out hit);
		if(hit.transform.name == "Player")
		return true;
		else
			return false;
	}

    // bool Hide(int index)
    // {
    //     Debug.DrawLine(lights[index].transform.position, head.position, Color.red);
    //     Debug.DrawLine(lights[index].transform.position, chest.position, Color.blue);
    //     Debug.DrawLine(lights[index].transform.position, foot.position, Color.green);
	// 
    //     RaycastHit hit;
    //     float distance = Vector3.Distance(lights[i].transform.position, player.position);
    //     hH = Sensor(head, hH, index);
	// 	hC = Sensor(chest, hC, index);
	// 	hF = Sensor(foot, hF, index);
	// 
    //     if ((hH && hC || hH && hF || hC && hF) || distance > range)
    //     {
    //         return true;
    //     }
    //     else return false;
    // }

    // bool Sensor(Transform obj,bool sensor, int i)
    // {
    //     RaycastHit hit;
    //     if (Physics.Linecast(lights[i].transform.position, obj.position, out hit))
    //     {
    //         Debug.Log(hit.transform.name);
    //         if (hit.transform.name != "Player")
    //         {
    //             return true;
    //         }
    //         else
    //         {
    //             return false;
    //         }
    //     }
    //     else return false;
    // }

	int FenyerossegSzamitas(float light_range, float distance, int act_lightness)
	//ide kerül a képlet, amivel kiszámoljuk milyen erősséggel világít meg a fény. Egyelőre csak egy egyszerű % számírás van benne.
	//paraméterei a fényforrás intetnzitása, a távolságunk a fénytől
	//kimenő paramétere a vizsgált ponthoz (fej, test, láb) tartozó megvilágítottság
	{
		int lightness = (int)((light_range - distance) / (light_range / 100));
		return act_lightness == 0 ? lightness : (int)(act_lightness + (lightness / 2));
	}
}


//!!!!!!!!!!!! Basszus a magyar elnevezések heylett találjatok ki valamit, én nem rendelkezem ilyesféle kompetenciákkal :D