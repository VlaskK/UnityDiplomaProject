using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public Transform obj;

    public void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/save.dat");
        SaveData data = new SaveData();

        data.x = obj.position.x;
        data.y = obj.position.y;
        data.r = obj.rotation.eulerAngles.z;

        formatter.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/save.dat", FileMode.Open);
        SaveData data = (SaveData)formatter.Deserialize(file);
        file.Close();
        obj.position = new Vector3(data.x, data.y, 0);
        obj.rotation = Quaternion.Euler(0, 0, data.r);
    }
}
