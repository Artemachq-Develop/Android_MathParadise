using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UISystem : MonoBehaviour
{
    public InputField numbersInputText;
    public Text numbersOutputText;
    public Image progressBar;
    public Image healthUI;

    public Button continueEndButton;
    
    public Image toHellCardButtonImage;
    public Image toParadiseCardButtonImage;
    public Image openChestCardButton;

    public Text death_SoulsRecordText;
    public Text death_AllSoulsText;

    public Image levelProgressImage;

    public Game_SaveLoad saveLoad;
    public MathSystem mathSystem;

    public void ContinueSkullGame_Button()
    {
        mathSystem.isDead = false;
        mathSystem.endPanel.SetActive(false);
        mathSystem.infoPanel.SetActive(true);
        saveLoad.allSkulls -= 5;
        mathSystem.health = 3;
        healthUI.fillAmount = 1f;
        mathSystem.timeLeft = 1f;
    }
}
