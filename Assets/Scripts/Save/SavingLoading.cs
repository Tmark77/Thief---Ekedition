using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SavingLoading {

    public static void SavePlayer(PlayerHealth playerHealth)
    {
        Debug.Log("start");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create);

        PlayerData data = new PlayerData(playerHealth);

        bf.Serialize(stream,data);
        stream.Close();
        Debug.Log("end");
    }

    public static float[] LoadPlayer()
    {
        if (File.Exists(Application.persistentDataPath + "/player.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Open);

            PlayerData data = bf.Deserialize(stream) as PlayerData;
            stream.Close();
            Debug.Log("loaded");
            return data.stats;
        }
        return new float[2];
    }

    [Serializable]
    public class PlayerData
    {
        public float[] stats;

        public PlayerData(PlayerHealth playerHealth)
        {
            stats = new float[2];
            stats[0] = playerHealth.currentHealth;
            stats[1] = playerHealth.startingHealth;
        }

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
