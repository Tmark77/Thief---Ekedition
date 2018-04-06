using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerHealth : MonoBehaviour//, ISavable
{
	public float startingHealth = 100;                            
	public float currentHealth;                                   
	public Slider healthSlider;                                 
	public Image damageImage;                                   
	public AudioClip deathClip;                                 
	public float flashSpeed = 5f;                               
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     


	Animator anim;                                              
	AudioSource playerAudio;                          
	bool isDead = false;                                                
	bool damaged;

    


	void Awake ()
	{
		anim = GetComponent <Animator> ();
		playerAudio = GetComponent <AudioSource> ();


		currentHealth = startingHealth;
	}


	void Update ()
	{
        if (Input.GetKeyDown(KeyCode.B)){
			TakeDamage (25);
		}

		if(damaged)
		{
			damageImage.color = flashColour;
		}
		else
		{
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;
	}


	public void TakeDamage (float amount)
	{
		damaged = true;

		currentHealth -= amount;

        setHealthSlider();


        playerAudio.Play ();


		if(currentHealth <= 0 && !isDead)
		{
			Death ();
		}
	}

    public void setHealthSlider()
    {
        healthSlider.value = currentHealth;
    }


	void Death ()
	{
		isDead = true;

		Debug.Log ("Halál");


        //anim.SetTrigger ("Die");

        //playerAudio.clip = deathClip;
        //playerAudio.Play ();

        //playerMovement.enabled = false;
    }

    
}