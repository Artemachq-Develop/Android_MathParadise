using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MathSystem : MonoBehaviour
{
    public UISystem uiSystem;
    public SoundManager SoundManager;
    public Game_SaveLoad saveLoad;

    public int score;
    public int skull;
    
    public int levelNow;

    private int a;
    private int b;
    private int x;
    private int v;
    private short rand;
    private short compl;
    private short multiX = 1;

    private bool isHuman;
    private bool isChest;
    private bool heartWasBeat;
    public bool needHelp;
    public bool isDead;

    public short enemyHealth = 2;

    public int health = 3;

    public float timeLeft = 1f;
    private float TimeLeftChange = 20f;

    [Header("Particle")]
    
    public UIParticleSystem _UiParticleSystem_Down;
    public UIParticleSystem _UiParticleSystem_Top;

    [Header("GameObject")]
    
    public GameObject ButtonsMath;
    public GameObject ButtonsHellParadise;

    public GameObject endPanel;
    public GameObject infoPanel;

    [Header("Animator")]
    public Animator enemyRockAnimator;
    public Animator toHellPanelAnimator;
    public Animator[] needHelpHellPanelAnimator;
    public Animator toMathPanelAnimator;
    private Animator correctTextAnimator;
    public Animator healthUIAnimator;
    public Animator playerAnimator;
    public Animator bladeAnimator;
    public Animator skullDropAnimator;
    public Animator deadPanelAnimator;

    [Header("UI")]
    
    public Text correctText;

    public Text multiXText;
    public Text scoreText;

    public Image enemySprite;
    public Image signImage;
    public Image ComplexityImage;
    public Image screamerImage;
    private Image bladeImage;
    public Sprite[] signChooseList;
    public Sprite[] enemySpriteList;
    
    public Image enemyHealthImage;
    public Sprite[] enemyHealthSprite;

    private void Awake()
    {
        
    }

    void Start()
    {
        correctTextAnimator = correctText.GetComponent<Animator>();
        bladeImage = bladeAnimator.GetComponent<Image>();
        signImage.enabled = false;

        multiX = 1;

        randomSkinEnemy();

        GenerateNumber((short) Random.Range(0, 3), (short) Random.Range(0, 2));

        TimeLeftChange = 20f;

        toHellPanelAnimator.SetBool("isOpen", true);
    }
    
    void Update()
    {
        TimerProgressBar();
        scoreText.text = "Ваш счет: " + score;
    }

    public void NumberClickButton(int number)
    {
        uiSystem.numbersInputText.text += number;

        SoundManager.RandomClickButtonSound();
    }

    void GenerateNumber(short type, short complexity)
    {
        //Complexity 0 = Easy
        //Complexity 1 = Normal
        //Complexity 2 = Hard

        if (type == 0) //Plus Setting Random Generate
        {
            if (complexity == 0)
            {
                a = Random.Range(1, 10);
                b = Random.Range(1, 10);
                ComplexityImage.fillAmount = 0.33f;
            }
            else if (complexity == 1)
            {
                a = Random.Range(10, 50);
                b = Random.Range(10, 50);
                ComplexityImage.fillAmount = 0.66f;
            }
            else if (complexity == 2)
            {
                a = Random.Range(20, 80);
                b = Random.Range(20, 80);
                ComplexityImage.fillAmount = 1f;
            }

            PlusSystem();
        }
        else if (type == 1) //Minus Setting Random Generate
        {
            if (complexity == 0)
            {
                a = Random.Range(1, 10);
                b = Random.Range(1, 10);
                ComplexityImage.fillAmount = 0.33f;
            }
            else if (complexity == 1)
            {
                a = Random.Range(10, 50);
                b = Random.Range(10, 50);
                ComplexityImage.fillAmount = 0.66f;
            }
            else if (complexity == 2)
            {
                a = Random.Range(20, 80);
                b = Random.Range(20, 80);
                ComplexityImage.fillAmount = 1f;
            }

            MinusSystem();
        }
        else if (type == 2) //Devide Setting Random Generate
        {
            if (complexity == 0)
            {
                a = Random.Range(1, 10);
                b = Random.Range(1, 10);
                ComplexityImage.fillAmount = 0.33f;
            }
            else if (complexity == 1)
            {
                a = Random.Range(2, 50);
                b = Random.Range(8, 50);
                ComplexityImage.fillAmount = 0.66f;
            }
            else if (complexity == 2)
            {
                a = Random.Range(3, 100);
                b = Random.Range(3, 100);
                ComplexityImage.fillAmount = 1f;
            }

            DivideSystem();
        }
        else if (type == 3) //Multiply Setting Random Generate
        {
            if (complexity == 0)
            {
                a = Random.Range(1, 10);
                b = Random.Range(1, 10);
                ComplexityImage.fillAmount = 0.33f;
            }
            else if (complexity == 1)
            {
                a = Random.Range(10, 15);
                b = Random.Range(4, 10);
                ComplexityImage.fillAmount = 0.66f;
            }
            else if (complexity == 2)
            {
                a = Random.Range(10, 30);
                b = Random.Range(5, 15);
                ComplexityImage.fillAmount = 1f;
            }

            MultiplySystem();
        }
    }

    public void CheckNumbers()
    {
        if (uiSystem.numbersInputText.text != "")
        {
            int v = int.Parse(uiSystem.numbersInputText.text);
            if (v != 0)
            {
                rand = (short) Random.Range(0, 4);
                compl = (short) Random.Range(0, 3);

                if (v == x || v == 666)
                {
                    //CORRECT ANSWER
                    if (v == 666)
                    {
                        SoundManager.PlaySound(
                            SoundManager.easternEgg666Sound[Random.Range(0, SoundManager.easternEgg666Sound.Length)]);
                        CorrectAnswer();
                        StartCoroutine("ScreamerImageShow");
                    }
                    else
                    {
                        CorrectAnswer();
                    }
                }
                else
                {
                    //INCORRECT ANSWER

                    IncorrectAnswer();
                }

                ClearInputField();
            }
        }
        else
        {
            Debug.Log("Type some number");
        }
    }

    void CorrectAnswer()
    {
        if (multiX <= 20)
        {
            multiX++;

            if (multiX >= 11 && multiX < 16)
            {
                _UiParticleSystem_Down.PlayOnAFew();
                SoundManager.PlaySound(SoundManager.multiXCorrectSound_lvl1);
                SetScore(Random.Range(20, 40));
            }
            else if (multiX >= 16 && multiX <= 21)
            {
                _UiParticleSystem_Down.PlayOnAFew();
                _UiParticleSystem_Top.PlayOnAFew();
                SoundManager.PlaySound(SoundManager.multiXCorrectSound_lvl2);
                SetScore(Random.Range(40, 60));
            }
        }
        
        //Score Value
        if (!isChest)
        {
            if (compl == 0)
            {
                SetScore((int) (Random.Range(20, 40) + (multiX * 2f) + (timeLeft * 10)));
            }
            else if (compl == 1)
            {
                SetScore((int) (Random.Range(50, 90) + (multiX * 3f) + (timeLeft * 10)));
            }
            else if (compl == 2)
            {
                SetScore((int) (Random.Range(100, 150) + (multiX * 4f) + (timeLeft * 10)));

                randomSkullDrop();
            }
            else
            {
                SetScore(50);
            }
        }
        else
        {
            SetScore((int) (Random.Range(500, 1200) + (multiX * 2f) + (timeLeft * 10)));
        }
        
        SoundManager.PlaySound(SoundManager.correctSound);

        //HEAL
        if (multiX >= 13 && health < 3)
        {
            Heal();
        }

        //Correct Text Show
        
        correctText.text = "Верный";
        correctText.color = Color.green;
        correctTextAnimator.SetTrigger("isCorrect");
        
        if (levelNow < 100)
        {
            levelNow++; 
        }

        //Update Time on Answer Questions
        
        timeLeft = 1f;
        if (TimeLeftChange > 2f)
        {
            TimeLeftChange -= 0.1f;
        }
        
        //Check Health

        if (enemyHealth >= 2)
        {
            GenerateNumber(rand, compl);
            enemyHealth--;
            enemyHealthImage.fillAmount -= 0.5f;
            
            if (!isHuman)
            {
                playerAnimator.SetTrigger("isAttack");
                SoundManager.PlaySound(SoundManager.bladeAttackSound);
            }
            else if (isHuman)
            {
                playerAnimator.SetTrigger("isSaveHuman");
            }
        }
        else
        {
            GenerateNumber(rand, compl);
            enemyHealthImage.enabled = false;
            
            enemyHealth = 2;
            enemyHealthImage.fillAmount = 1f;
            
            StartCoroutine("ChangeSkinEnemy_Anim");
            
            //Check Human or Not
            if (!isHuman)
            {
                bladeImage.enabled = true;
                signImage.enabled = false;
                bladeAnimator.SetTrigger("isAttack");
                playerAnimator.SetTrigger("isAttack");
                SoundManager.PlaySound(SoundManager.bladeAttackSound);
            }
            else if (isHuman)
            {
                playerAnimator.SetTrigger("isSaveHuman");
            }
            
            uiSystem.numbersOutputText.enabled = false;

            toMathPanelAnimator.SetBool("isOpen", false);
            toHellPanelAnimator.SetBool("isOpen", true);
        }

        multiXText.text = "x" + multiX;

        /*ButtonsMath.SetActive(false);*/
        /*ButtonsHellParadise.SetActive(true);*/
        

        isChest = false;
    }

    void IncorrectAnswer()
    {
        multiX = 1;

        correctText.text = "Неверный";
        correctText.color = Color.red;
        correctTextAnimator.SetTrigger("isCorrect");

        Damage();
        GenerateNumber(rand, compl);

        multiXText.text = "x" + multiX;
    }

    private int SetScore(int count)
    {
        if (score + count > 0)
        {
            return score += count;
        }
        else
        {
            return 0;
        }
    }

    private void randomSkullDrop()
    {
        short rand = (short)Random.Range(0, 3);
        
        if (rand == 0)
        {
            skullDropAnimator.SetTrigger("isDrop");
            skull++;
        }
    }

    public void ToHellButton()
    {
        if (isHuman && !isChest)
        {
            Damage();
        }
        else
        {
            enemyHealthImage.enabled = true;
            enemyHealthImage.sprite = enemyHealthSprite[0];
            
            SetScore(Random.Range(20, 50));

            signImage.sprite = signChooseList[1];
            signImage.enabled = true;

            uiSystem.numbersOutputText.enabled = true;

            SoundManager.ToHellButtonSound(true);

            /*ButtonsMath.SetActive(true);
            ButtonsHellParadise.SetActive(false);*/

            toMathPanelAnimator.SetBool("isOpen", true);
            toHellPanelAnimator.SetBool("isOpen", false);
        }
    }

    public void ToParadiseButton()
    {
        if (!isHuman && !isChest)
        {
            Damage();
        }
        else
        {
            enemyHealthImage.enabled = true;
            enemyHealthImage.sprite = enemyHealthSprite[1];
            
            SetScore(Random.Range(20, 50));
            
            signImage.sprite = signChooseList[0];
            signImage.enabled = true;

            uiSystem.numbersOutputText.enabled = true;

            SoundManager.ToHellButtonSound(false);

            /*ButtonsMath.SetActive(true);
            ButtonsHellParadise.SetActive(false);*/

            toMathPanelAnimator.SetBool("isOpen", true);
            toHellPanelAnimator.SetBool("isOpen", false);
        }
    }

    public void ToChestButton()
    {
        if (isChest)
        {
            SetScore(Random.Range(20, 50));
            
            signImage.sprite = signChooseList[2];
            signImage.enabled = true;

            uiSystem.numbersOutputText.enabled = true;

            SoundManager.ToHellButtonSound(false);

            toMathPanelAnimator.SetBool("isOpen", true);
            toHellPanelAnimator.SetBool("isOpen", false);
        }
    }

    void PlusSystem()
    {
        x = a + b;
        uiSystem.numbersOutputText.text = a + " + " + b + " = " + " ?";
    }

    void MinusSystem()
    {
        if (a > b && a - b != 0)
        {
            x = a - b;
            uiSystem.numbersOutputText.text = a + " - " + b + " = " + " ?";
        }
        else
        {
            GenerateNumber(1, 1);
        }
    }

    void DivideSystem()
    {
        if (a > b && a % b == 0)
        {
            x = a / b;
            uiSystem.numbersOutputText.text = a + " / " + b + " = " + " ?";
        }
        else
        {
            GenerateNumber(2, 1);
        }
    }

    void MultiplySystem()
    {
        x = a * b;
        uiSystem.numbersOutputText.text = a + " * " + b + " = " + " ?";
    }

    public void ClearInputFieldButton()
    {
        if (uiSystem.numbersInputText.text != "")
        {
            uiSystem.numbersInputText.text = "";
            SoundManager.PlaySound(SoundManager.clearTextSound);
        }
    }
    
    private void ClearInputField()
    {
        uiSystem.numbersInputText.text = "";
    }

    void Damage()
    {
        if (health > 1)
        {
            health--;
            SetScore(Random.Range(-10, -30));
            uiSystem.healthUI.fillAmount -= 0.34f;
            playerAnimator.SetTrigger("isDamaged");
            healthUIAnimator.SetTrigger("isDamaged");
            SoundManager.PlaySound(SoundManager.incorrectSound);
        }
        else
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        
        saveLoad.allSouls = score;
        saveLoad.nowSkulls = skull;
        saveLoad.soulsRecord = score;
        saveLoad.isSound = SoundManager.isSound;

        uiSystem.levelProgressImage.fillAmount = levelNow / 100f;

        if (saveLoad.allSkulls >= 5 || saveLoad.nowSkulls >= 5)
        {
            uiSystem.continueEndButton.interactable = true;
        }
        else
        {
            uiSystem.continueEndButton.interactable = false;
        }

        endPanel.SetActive(true);
        deadPanelAnimator.SetBool("isDead", true);

        infoPanel.SetActive(false);
        
        uiSystem.death_SoulsRecordText.text = saveLoad.allSkulls.ToString();
        uiSystem.death_AllSoulsText.text = score.ToString();

        SoundManager.PlaySound(SoundManager.gameOverRu[Random.Range(0, SoundManager.gameOverRu.Length)]);
        
        saveLoad.SaveFile();
    }

    public void continueButton()
    {
    }

    public void endButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    void Heal()
    {
        health++;
        uiSystem.healthUI.fillAmount += 0.34f;
        SoundManager.PlaySound(SoundManager.healSound);
    }

    void TimerProgressBar()
    {
        if (!isDead)
        {
            if (Time.timeScale > 0)
            {
                timeLeft -= Time.deltaTime / TimeLeftChange;

                if (timeLeft <= 0.8f && timeLeft >= 0.7f && needHelp)
                {
                    if (isHuman)
                    {
                        needHelpHellPanelAnimator[0].SetTrigger("needHelp");
                    }
                    else if (!isHuman)
                    {
                        needHelpHellPanelAnimator[1].SetTrigger("needHelp");
                    }
                }
                else
                if (timeLeft <= 0.2f && !heartWasBeat)
                {
                    SoundManager.PlaySound(SoundManager.heartBeatSound);
                    heartWasBeat = true;
                }
                else
                if (timeLeft < 0)
                {
                    timeLeft = 1f;
                    heartWasBeat = false;
                    Damage();
                }

                uiSystem.progressBar.fillAmount = timeLeft;  
            }
        }
    }

    IEnumerator ChangeSkinEnemy_Anim()
    {
        enemyRockAnimator.SetTrigger("isAccept");
        yield return new WaitForSeconds(0.4f);
        signImage.enabled = false;
        bladeImage.enabled = false;
        randomSkinEnemy();
    }

    IEnumerator ScreamerImageShow()
    {
        screamerImage.enabled = true;
        yield return new WaitForSeconds(0.2f);
        screamerImage.enabled = false;
    }

    void randomSkinEnemy()
    {
        int rand = Random.Range(0, enemySpriteList.Length);

        if (rand != 22)
        {
            if (rand >= 0 && rand < 13)
            {
                isHuman = true;
            }
            else
            {
                isHuman = false;
            }
            
            uiSystem.openChestCardButton.enabled = false;
            uiSystem.toHellCardButtonImage.enabled = true;
            uiSystem.toParadiseCardButtonImage.enabled = true;
        }
        else
        {
            isChest = true;

            uiSystem.openChestCardButton.enabled = true;
            uiSystem.toHellCardButtonImage.enabled = false;
            uiSystem.toParadiseCardButtonImage.enabled = false;
        }

        enemySprite.sprite = enemySpriteList[rand];
    }

    public void PauseTimeScale(int scaleCount)
    {
        Time.timeScale = scaleCount;
    }

    public void UnpauseClick()
    {
        GenerateNumber((short) Random.Range(0, 3), (short) Random.Range(0, 2));
        Damage();
    }
}