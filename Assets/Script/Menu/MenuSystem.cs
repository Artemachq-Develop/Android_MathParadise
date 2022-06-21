using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSystem : MonoBehaviour
{
    public Text recordScoreText;
    public Text allSoulsText;
    public Text allSkullsText;
    public Text lvlCountText;

    public Animator backgroundAnimator;

    public GameObject casePanelMain;
    public GameObject shopPanelMain;
    public GameObject settingsPanelMain;

    public MainMenuSoundManager mainMenuSoundManager;

    public MainMenu_SaveLoad saveLoad;
    
    public GameObject[] offObjectForLoad;

    private void Start()
    {
        saveLoad.LoadFile();

        recordScoreText.text = "Рекорд" + "\n" + saveLoad.soulsRecord;
        lvlCountText.text = "Уровень: " + saveLoad.lvlCount.ToString();

        UpdateSkullsText();
        UpdateSoulsText();
    }

    public void playButton()
    {
        backgroundAnimator.SetTrigger("isOpenBack");
        for (int i = 0; i < offObjectForLoad.Length; i++)
        {
            offObjectForLoad[i].SetActive(false);
        }
        
        mainMenuSoundManager.RandomClickButtonSound();

        Invoke("playGameScene", 2f);
    }

    private void UpdateSoulsText()
    {
        allSoulsText.text = saveLoad.allSouls.ToString();
    }

    private void UpdateSkullsText()
    {
        allSkullsText.text = saveLoad.allSkulls.ToString();
    }

    public void playGameScene()
    {
        SceneManager.LoadScene(2);
    }

    public void OpenCasePanel()
    {
        casePanelMain.SetActive(true);
        mainMenuSoundManager.RandomClickButtonSound();
    }
    
    public void CloseCasePanel()
    {
        casePanelMain.SetActive(false);
        mainMenuSoundManager.RandomClickButtonSound();
        UpdateSoulsText();
        UpdateSkullsText();
        saveLoad.SaveFile();
    }
    
    public void OpenShopPanel()
    {
        shopPanelMain.SetActive(true);
        mainMenuSoundManager.RandomClickButtonSound();
    }
    
    public void CloseShopPanel()
    {
        shopPanelMain.SetActive(false);
        mainMenuSoundManager.RandomClickButtonSound();
        UpdateSoulsText();
        UpdateSkullsText();
        saveLoad.SaveFile();
    }

    public void OpenSettingsPanel()
    {
        settingsPanelMain.SetActive(true);
        mainMenuSoundManager.RandomClickButtonSound();
    }

    public void CloseSettingsPanel()
    {
        settingsPanelMain.SetActive(false);
        mainMenuSoundManager.RandomClickButtonSound();
    }

    /*private void OnApplicationQuit()
    {
        saveLoad.SaveFile();
        saveLoad.SaveChest();
    }*/

    public void clearSave()
    {
        PlayerPrefs.DeleteAll();
    }
}