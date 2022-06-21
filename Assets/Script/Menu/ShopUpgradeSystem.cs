using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUpgradeSystem : MonoBehaviour
{
    public Image[] contentImage;
    public Sprite[] readycontentSprite;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShopButton_1()
    {
        contentImage[0].sprite = readycontentSprite[0];
    }
    
    public void ShopButton_2()
    {
        contentImage[1].sprite = readycontentSprite[1];
    }
    
    public void ShopButton_3()
    {
        contentImage[2].sprite = readycontentSprite[2];
    }
    
    public void ShopButton_4()
    {
        contentImage[3].sprite = readycontentSprite[3];
    }
    
    public void ShopButton_5()
    {
        contentImage[4].sprite = readycontentSprite[4];
    }
    
    public void ShopButton_6()
    {
        contentImage[5].sprite = readycontentSprite[5];
    }
    
    public void ShopButton_7()
    {
        contentImage[6].sprite = readycontentSprite[6];
    }
}
