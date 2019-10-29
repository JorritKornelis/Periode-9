using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine.UI;

public class Saving : MonoBehaviour
{
    public string fileName;
    public SaveDataBase data;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        LoadData();
    }

    //LoadData
    ///Loads your XML save file
    public void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/" + fileName + ".xml"))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SaveDataBase));
            FileStream stream = new FileStream(Application.persistentDataPath + "/" + fileName + ".xml", FileMode.Open);
            data = (SaveDataBase)serializer.Deserialize(stream);
            stream.Close();
        }
        else
        {
            data = new SaveDataBase();
            data.inventory = new List<ItemSaveSlot>();
        }
    }

    //SaveData
    ///Saves your data as a XML file
    public void SaveData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SaveDataBase));
        FileStream stream = new FileStream(Application.persistentDataPath + "/" + fileName + ".xml", FileMode.Create);
        serializer.Serialize(stream, data);
        stream.Close();
    }

    //QuitFunction
    ///Saves your data when you quit
    public void OnApplicationQuit()
    {
        SaveData();
    }
}

//SaveInfo
///This is the info that will get saved
[System.Serializable]
public class SaveDataBase
{
    public List<ItemSaveSlot> inventory;
    public int currency;
    public UpgradeUnlocks unlocks;
    public bool hadTutorialShop;
    public bool hadTutorialDungeon;
}

[System.Serializable]
public class ItemSaveSlot
{
    public int itemIndex;
    public int slot;
    public int amount;
}

[System.Serializable]
public class UpgradeUnlocks
{
    public int isUpgradeInv;
    public bool isUpgradeHealth;
    public bool isUpgradeSword;
    public int isUpgradeShop;
}
