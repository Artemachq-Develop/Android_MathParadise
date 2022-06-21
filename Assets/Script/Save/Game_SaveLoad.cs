using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Game_SaveLoad : MonoBehaviour
{
    public int soulsRecord;
    public int allSouls;
    public int allSkulls;
    public int nowSkulls;
    public bool isSound;

    public SoundManager soundManager;
    
    private Save save = new Save();

    private void Start()
    {
        LoadFile();
    }

    public void SaveFile()
    {
        save.allSouls_Save += allSouls;
        save.allSkulls_Save = allSkulls;
        save.allSkulls_Save += nowSkulls;
        save.isSound = isSound;
        
        if (soulsRecord >= save.soulsRecord_Save)
        {
            save.soulsRecord_Save = soulsRecord;
        }

        //Сохранение
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
            allSkulls = save.allSkulls_Save;
            isSound = save.isSound;
            soundManager.isSound = isSound;
            if (isSound)
            {
                soundManager.musicManager.enabled = true;
            }
            else
            {
                soundManager.musicManager.enabled = false;
            }
        }
    }
}
