using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SavingLoading : MonoBehaviour {

    public Transform player;
    public PlayerHealth playerHealth;
    private PlayerData data;
    private PlayerData startData;

    private void Start()
    {
        Debug.Log("SavingLoading start");
        data = new PlayerData
        {
            x = player.transform.position.x,
            y = player.transform.position.y,
            z = player.transform.position.z,

            currentHealth = playerHealth.currentHealth,
            startingHealth = playerHealth.startingHealth
        };

        startData = new PlayerData
        {
            x = player.transform.position.x,
            y = player.transform.position.y,
            z = player.transform.position.z,

            currentHealth = playerHealth.currentHealth,
            startingHealth = playerHealth.startingHealth
        };
    }

    public void SavePlayer()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create);

        data.x = player.transform.position.x;
        data.y = player.transform.position.y;
        data.z = player.transform.position.z;
        data.currentHealth = playerHealth.currentHealth;
        data.startingHealth = playerHealth.startingHealth;

        bf.Serialize(stream,data);
        stream.Close();
        Debug.Log("end");
    }

    public void RestorePlayer()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create);

        bf.Serialize(stream, startData);
        stream.Close();
    }

    public void LoadPlayer()
    {
        LoadPlayer(data);
    }

    public void NewPlayer()
    {
        RestorePlayer();
        LoadPlayer(startData);
    }


    public void LoadPlayer(PlayerData d)
    {
        if (File.Exists(Application.persistentDataPath + "/player.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Open);
            d = bf.Deserialize(stream) as PlayerData;

            player.transform.position = new Vector3(d.x, d.y, d.z);
            playerHealth.currentHealth = d.currentHealth;
            playerHealth.startingHealth = d.startingHealth;
            playerHealth.setHealthSlider();

            stream.Close();
            Debug.Log("loaded");
        }
    }

    [Serializable]
    public class PlayerData
    {
        public float currentHealth;
        public float startingHealth;
        public float x;
        public float y;
        public float z;
    }
    
}
