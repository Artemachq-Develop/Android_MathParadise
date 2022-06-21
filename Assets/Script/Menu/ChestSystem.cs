using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ChestSystem : MonoBehaviour
{
    [HideInInspector] public bool _pressed = false;
    
    private float timeLeft = 1f;
    private float ImageProgress;
    
    public Animator caseAnimator;
    
    public GameObject shopPanel;
    public GameObject openCasePanel;
    
    public Image chestShopFull_Image;

    public Text soulsCount_Text;
    public Text buttonText;
    
    public Text chestCountText;
    public Text chestDropText;
    
    public Button openChestButton;
    
    public Image[] contentImageList;
    public Sprite[] dropSpriteList;

    public MainMenuSoundManager mainMenuSoundManager;
    public MainMenu_SaveLoad saveLoad;

    private void OnEnable()
    {
        checkChestCount();
        checkSoulsCount();
        chestShopFull_Image.fillAmount = saveLoad.chestProgress;
    }

    private void Update()
    {
        if (_pressed)
        {
            timeLeft -= Time.deltaTime * 14f;
            if (timeLeft <= 0)
            {
                if (saveLoad.chestProgress < 1f && saveLoad.allSouls - 25 > 0)
                {
                    saveLoad.chestProgress += 0.05f;
                    chestShopFull_Image.fillAmount = saveLoad.chestProgress;
                    saveLoad.allSouls -= 50;
                    checkSoulsCount();
                    ImageProgress = float.Parse(chestShopFull_Image.fillAmount.ToString("#.##"));
                }
                else if (saveLoad.chestProgress >= 1f)
                {
                    saveLoad.chestProgress = 0f;
                    chestShopFull_Image.fillAmount = saveLoad.chestProgress;
                    _pressed = false;
                    saveLoad.chestCount++;
                    checkChestCount();
                    saveLoad.SaveChest();
                    saveLoad.SaveFile();
                }

                timeLeft = 1f;
            }
        }
    }

    public void CaseOpenPanelShow()
    {
        shopPanel.SetActive(false);
        openCasePanel.SetActive(true);
        checkChestCount();
    }
    
    private void checkChestCount()
    {
        chestCountText.text = "Количество сундуков\n" + saveLoad.chestCount;
        
        if (saveLoad.chestCount >= 1)
        {
            openChestButton.interactable = true;
        }
        else if (saveLoad.chestCount <= 0)
        {
            openChestButton.interactable = false;
        }
    }

    private void checkSoulsCount()
    {
        soulsCount_Text.text = saveLoad.allSouls.ToString();
    }
    
    public void OnClickOpenCase()
    {
        if (!caseAnimator.GetBool("isOpen"))
        {
            mainMenuSoundManager.RandomClickButtonSound();
            mainMenuSoundManager.PlaySound(mainMenuSoundManager.openCaseSound);

            saveLoad.chestCount--;
            saveLoad.SaveChest();
            checkChestCount();
            
            buttonText.text = "Выйти";
            caseAnimator.SetBool("isOpen", true);
            for (int i = 0; i < contentImageList.Length; i++)
            {
                contentImageList[i].sprite = dropSpriteList[Random.Range(0, dropSpriteList.Length)];
                if (i == 10)
                {
                    int rand = Random.Range(0, dropSpriteList.Length);
                    contentImageList[10].sprite = dropSpriteList[rand];

                    if (rand == 0)
                    {
                        chestDropText.text = "ВАМ ВЫПАЛО\n" + "1 предмет";
                        print(dropSpriteList[rand].name);
                        saveLoad.allSouls += 500;
                    }
                    else if (rand == 1)
                    {
                        chestDropText.text = "ВАМ ВЫПАЛО\n" + "2 предмет";
                        print(dropSpriteList[rand].name);
                        saveLoad.allSkulls += 2;
                    }
                    else if (rand == 2)
                    {
                        chestDropText.text = "ВАМ ВЫПАЛО\n" + "3 предмет";
                        print(dropSpriteList[rand].name);
                        saveLoad.allSkulls += 5;
                    }
                    else if (rand == 3)
                    {
                        chestDropText.text = "ВАМ ВЫПАЛО\n" + "4 предмет";
                        print(dropSpriteList[rand].name);
                        saveLoad.allSouls += 500;
                    }
                    else if (rand == 4)
                    {
                        chestDropText.text = "ВАМ ВЫПАЛО\n" + "5 предмет";
                        print(dropSpriteList[rand].name);
                        saveLoad.allSouls += 500;
                    }
                    else if (rand == 4)
                    {
                        chestDropText.text = "ВАМ ВЫПАЛО\n" + "6 предмет";
                        print(dropSpriteList[rand].name);
                        saveLoad.allSouls += 500;
                    }
                }
            }
        }
        else
        {
            caseAnimator.SetBool("isOpen", false);
            buttonText.text = "Открыть";
            shopPanel.SetActive(true);
            openCasePanel.SetActive(false);
            checkSoulsCount();
        }
    }

    public void showInfoDropText()
    {
        chestDropText.enabled = true;
    }
}