using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsInShadow : MonoBehaviour {

    public Transform player;
    public Transform head;
    public Transform chest;
    public Transform foot;

	static int vH; //head visivility  NEM STATIC AMÚGY
	static int vC; //chest visivility NEM STATIC AMÚGY
	static int vF; //foot visivility  NEM STATIC AMÚGY

    int i; //index
    int fullDark; //full shadow
    public int range;

	float distance; //distance of two objects
	public Renderer mesh;
	public Material mat;

	public Light[] lights; //array of Lights
    public bool fullHide; //character is in shadow or not

	// Use this for initialization
	void Start () {
    }

	public static int GetVH()
	{
		return vH;
	}
	public static int GetVC()
	{
		return vC;
	}
	public static int GetVF()
	{
		return vF;
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
					vH = BrightnessCalculate(lights[i].range, distance, vH);
				}
				if(VanRalatas(chest, i)){
					distance = Vector3.Distance(lights[i].transform.position, chest.position);
					vC = BrightnessCalculate(lights[i].range, distance, vC);
				}
				if(VanRalatas(foot, i)){
					distance = Vector3.Distance(lights[i].transform.position, foot.position);
					vF = BrightnessCalculate(lights[i].range, distance, vF);
				}
			}

        }

		//LightGemTemp.text = "Head: " + vH + " Chest: " + vC + " Foot: " + vF + "Overall: "+ (vH+vC+vF)/3;
		mesh.material.SetFloat ("_Emission", (((vH + vC + vF) / 3) * 0.02f));

		if (mesh.material.GetFloat ("_Emission") == 0) 
		{
			mesh.material.SetFloat ("_Emission", 0.02f);
		}
        //Debug.Log (mesh.material.GetFloat ("_Emission"));
	
    }

	bool VanRalatas (Transform obj, int index)
	{
		//RaycastHit hit;
		//Physics.Linecast (lights [index].transform.position, obj.position, out hit);
		//Physics.Raycast (lights [index].transform.position, obj.position-lights [index].transform.position, out hit);

		RaycastHit[] hits = Physics.RaycastAll (lights [index].transform.position, obj.position - lights [index].transform.position, Vector3.Distance(lights[i].transform.position, obj.position));

		List<ThiefObject> tf = new List<ThiefObject> ();
		foreach (RaycastHit hit in hits) 
		{
			if (hit.collider.gameObject.GetComponent<ThiefObject> () != null) 
			{
				tf.Add (hit.collider.gameObject.GetComponent<ThiefObject> ());
			}
		}

		int ind;
		ind = 0;
		if (tf.Count != 0) {
			do {
				if (! tf[ind].material.SeeTrough())
				{
					return false;
				}
				ind++;
			} while(ind < tf.Count);
		}
		return true;
	}

    /// <summary>
    /// ide kerül a képlet, amivel kiszámoljuk milyen erősséggel világít meg a fény. 
    /// Egyelőre csak egy egyszerű % számírás van benne.
    ///paraméterei a fényforrás intetnzitása, a távolságunk a fénytől
    ///kimenő paramétere a vizsgált ponthoz (fej, test, láb) tartozó megvilágítottság
    /// </summary>
    /// <param name="light_range"></param>
    /// <param name="distance"></param>
    /// <param name="act_lightness"></param>
    /// <returns></returns>
    int BrightnessCalculate(float light_range, float distance, int act_brightness)
	{
		int brightness = (int)((light_range - distance) / (light_range / 100));
		return act_brightness == 0 ? brightness : (int)(act_brightness + (brightness / 2));
	}
}