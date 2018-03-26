using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SavingLoading : MonoBehaviour {

    public Transform player;
    public PlayerHealth playerHealth;

    

    public void SavePlayer()
    {
        Debug.Log("start");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create);
        

        PlayerData data = new PlayerData
        {
            x = player.transform.position.x,
            y = player.transform.position.y,
            z = player.transform.position.z,

            currentHealth = playerHealth.currentHealth,
            startingHealth = playerHealth.startingHealth
        };

        bf.Serialize(stream,data);
        stream.Close();
        Debug.Log("end");
    }
    

    public void LoadPlayer()
    {
        if (File.Exists(Application.persistentDataPath + "/player.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Open);
            PlayerData data = bf.Deserialize(stream) as PlayerData;

            player.transform.position = new Vector3(data.x, data.y, data.z);
            playerHealth.currentHealth = data.currentHealth;
            playerHealth.startingHealth = data.startingHealth;
            playerHealth.setHealthSlider();

            stream.Close();
            Debug.Log("loaded");
        }
    }

    [Serializable]
    class PlayerData
    {
        public float currentHealth;
        public float startingHealth;
        public float x;
        public float y;
        public float z;
        
    }





    /*
    public void SavePosition()
    {
        float x = PlayerPrefs.GetFloat("PlayerX", transform.position.x);
        float y = PlayerPrefs.GetFloat("PlayerY", transform.position.y);
        float z = PlayerPrefs.GetFloat("PlayerZ", transform.position.z);

        Debug.Log(x +" "+ y +" "+ z);
    }
    public void LoadPosition()
    {
        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        float z = PlayerPrefs.GetFloat("PlayerZ");

        Debug.Log(x+ y+ z);

        transform.position = new Vector3(x,y,z);
    }
    */
}
