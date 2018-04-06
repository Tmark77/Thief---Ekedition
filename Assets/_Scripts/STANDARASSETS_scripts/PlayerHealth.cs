using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using TMPro;

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
	public static bool isDead = false;                                                
	bool damaged;

    


	void Awake ()
	{
		anim = GetComponent <Animator> ();
		playerAudio = GetComponent <AudioSource> ();
        restart = false;
        deadText.SetActive(false);
        currentHealth = startingHealth;
        counter = 3f;
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

        if (isDead)
        {
            if (counter < 0)
            {
                restart = true;
                isDead = false;
                currentHealth = startingHealth;
            }
            counter -= Time.deltaTime;
        }
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

    float counter;
    public void setHealthSlider()
    {
        healthSlider.value = currentHealth;
    }

    public static bool restart;
    public GameObject deadText;
    void Death()
    {
        isDead = true;
        deadText.SetActive(true);

        Debug.Log("Halál");
    }

}