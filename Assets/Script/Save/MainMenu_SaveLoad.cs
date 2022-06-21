using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_SaveLoad : MonoBehaviour
{
    public int soulsRecord;
    public int allSouls;
    public int allSkulls;
    public int lvlCount;
    public int chestCount;
    public float chestProgress;
    
    private Save save = new Save();
    
    public void SaveFile()
    {
        save.allSouls_Save = allSouls;
        save.lvlCount_Save = lvlCount;
        save.allSkulls_Save = allSkulls;
        
        PlayerPrefs.SetString("Save", JsonUtility.ToJson(save));
    }

    public void SaveChest()
    {
        save.chestCount_Save = chestCount;

        PlayerPrefs.SetString("Save", JsonUtility.ToJson(save));
    }

    public void SaveChestProgress()
    {
        save.chestProgress_Save = chestProgress;

        PlayerPrefs.SetString("Save", JsonUtility.ToJson(save));
    }
    
    public void LoadFile()
    {
        if (!PlayerPrefs.HasKey("Save"))
        {
            print("Dont Exist Save File (Save)");
            SaveFile();
            LoadFile();
        }
        else
        {
            save = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("Save"));

            soulsRecord = save.soulsRecord_Save;
            allSouls = save.allSouls_Save;
            allSkulls = save.allSkulls_Save;
            chestCount = save.chestCount_Save;
            chestProgress = save.chestProgress_Save;
            lvlCount = save.lvlCount_Save;
        }
    }
}
