using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
	public float startingHealth = 100;
	int maximumHealth = 100;
	public float currentHealth;                                   
	public Slider healthSlider;                                 
	public Image damageImage;                                   
	public AudioClip deathClip;                                 
	public float flashSpeed = 5f;                               
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     

                        
	public static bool isDead = false;                                                
	bool damaged;

    


	void Start ()
	{
        restart = false;
        deadText.SetActive(false);
        currentHealth = startingHealth;
        counter = 3f; // ez majd később a nehézség függvénye lesz
		Debug.Log(maximumHealth.ToString());
		setHealthSlider ();
	}


	void Update ()
	{
  //      if (Input.GetKeyDown(KeyCode.B)){
		//	TakeDamage (25);
		//}

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
            deadText.SetActive(true);
            if (counter < 0)
            {
                restart = true;
                isDead = false;
                currentHealth = startingHealth;
                setHealthSlider();
            }
            counter -= Time.deltaTime;
        }
        else
        {
            deadText.SetActive(false);
        }
    }


	public void TakeDamage (float amount)
	{
		damaged = true;

		currentHealth -= amount;

        setHealthSlider();
        


		if(currentHealth <= 0 && !isDead)
		{
			Death ();
		}

        

	}

	public void GetHeal (int amount)
	{

		currentHealth += amount;
		if (currentHealth > maximumHealth) 
		{
			currentHealth = maximumHealth;
		}

		setHealthSlider();
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
        counter = 3f;
        isDead = true;

        Debug.Log("Halál");
    }

}