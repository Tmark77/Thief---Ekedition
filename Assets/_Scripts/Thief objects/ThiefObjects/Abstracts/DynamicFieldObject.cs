using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DynamicFieldObject : ThiefObject, I_Highlightable {

	public abstract void Interaction (bool IsManualyOperated);

	public void Highlight()
	{
		if (this.gameObject.GetComponent<Renderer> () != null) 
		{
			this.gameObject.GetComponent<Renderer> ().material.EnableKeyword ("_EMISSION");
			this.gameObject.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", new Color (0.3f, 0.3f, 0.3f));
		}
		Renderer[] rend = this.gameObject.transform.GetComponentsInChildren<Renderer> ();
		foreach (Renderer item in rend) {
			item.material.EnableKeyword ("_EMISSION");
			item.material.SetColor ("_EmissionColor", new Color(0.3f,0.3f,0.3f));
		}
	}

	public void DeHighlight()
	{
		if (this.gameObject.GetComponent<Renderer> () != null) 
		{
			this.gameObject.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", Color.black);
		}
		Renderer[] rend = this.gameObject.transform.GetComponentsInChildren<Renderer> ();
		foreach (Renderer item in rend) {
			item.material.SetColor ("_EmissionColor", Color.black);
		}
	}

}
