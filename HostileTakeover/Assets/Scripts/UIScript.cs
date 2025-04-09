using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    public GameObject player;
    public GameController gameController;
    public EnemyController enemyController;
    public SettingsScript playerCharacter;
    public GameObject[] i_healthBars;
    public GameObject i_healthLow;
    public GameObject settingsPanel;
    public GameObject pauseMenu;


    public Slider healthSlider;
    [SerializeField] public Image[] cockpitImage;
    [SerializeField] public Image[] captainImage;
    [SerializeField] public Image[] bodyImage;
    [SerializeField] public AudioClip[] voiceLines1;
    [SerializeField] public AudioClip[] voiceLines2;
    [SerializeField] public AudioClip[] voiceLines3;
    [SerializeField] public AudioClip[] voiceLines4;
    public AudioSource voiceSource;
    public Text killText;
    public Text Timer;
    public float kills;
    public float timeCounter;
    public float camerAspect;
    public bool isPaused;
    public GameObject gameOver;

    void Awake()
    {
        DisableCharacterImages();
    }

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        timeCounter = 0;
        i_healthBars = GameObject.FindGameObjectsWithTag("HealthBars");
        settingsPanel.SetActive(false);
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        camerAspect = Camera.main.aspect;
        Debug.Log(camerAspect);

        SelectHealthBar();
        DisplayKills();
        DisplayTimer();
        PauseMenuEsc();
        if(isPaused == false)
        {
            timeCounter += Time.deltaTime;
        }
    }

    void DisableCharacterImages()
    {
        for (int i = 0; i < cockpitImage.Length; i++)
        {
            cockpitImage[i].gameObject.SetActive(false);
            captainImage[i].gameObject.SetActive(false);
            bodyImage[i].gameObject.SetActive(false);
        }
    }

    public void DisplayPilot(int enemy, int voice)
    {
        // Set Active to image
        Debug.Log(enemy);
        captainImage[enemy].gameObject.SetActive(true);
        cockpitImage[enemy].gameObject.SetActive(true);
        bodyImage[enemy].gameObject.SetActive(true);

        // Play Audio
        if (enemy == 0)
            voiceSource.clip = voiceLines1[voice];
        if (enemy == 1)
            voiceSource.clip = voiceLines2[voice];
        if (enemy == 2)
            voiceSource.clip = voiceLines3[voice];
        if (enemy == 3)
            voiceSource.clip = voiceLines4[voice];

        voiceSource.Play();

        // Close image
        StartCoroutine(WaitForVoiceLine(enemy));
    }

    IEnumerator WaitForVoiceLine(int enemy)
    {
        yield return new WaitForSeconds(3);
        cockpitImage[enemy].gameObject.SetActive(false);
        captainImage[enemy].gameObject.SetActive(false); 
        bodyImage[enemy].gameObject.SetActive(false);
    }

    public void GameOverScreen()
    {
        Cursor.visible = true;
        gameOver.SetActive(true);
    }

    void PauseMenuEsc()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Pause the game somehow?
            if(!pauseMenu.activeSelf && !settingsPanel.activeSelf)
            {
                isPaused = true;
                pauseMenu.SetActive(true);
            }
            else if (pauseMenu.activeSelf && settingsPanel.activeSelf)
            {
                isPaused = false;
                pauseMenu.SetActive(false); 
                settingsPanel.SetActive(false);
            }
            else if (pauseMenu.activeSelf && !settingsPanel.activeSelf)
            {
                isPaused = false;
                pauseMenu.SetActive(false);
            }
        }
    }

    public void ClosePauseMenu()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
    }

    public void CloseSettingsMenu()
    {
        settingsPanel.SetActive(false);
    }

    public void OpenSettingsMenu()
    {
        if (settingsPanel.activeSelf == true)
            settingsPanel.SetActive(false);
        else
            settingsPanel.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void DisplayKills()
    {
        kills = gameController.GetComponent<GameController>().kills;
        killText.text = kills.ToString();
    }

    void DisplayTimer()
    {
        float mins = Mathf.FloorToInt(timeCounter / 60);
        float secs = Mathf.FloorToInt(timeCounter % 60);
        Timer.text = string.Format("{00:00}:{01:00}", mins, secs);
    }

    void SelectHealthBar()
    {
        if(player != null)
        {
            float currHealth = player.GetComponent<Health>().currentHealth;
            ChangeHealthBar(currHealth);
        }
        else
        {
            ChangeHealthBar(0);
        }
    }

    void ChangeHealthBar(float currHealth)
    {
        switch (currHealth)
        {
            case 0:
                for (int i = 0; i < i_healthBars.Length; i++)
                {
                    if (i_healthBars[i].name.Contains("4"))
                    {
                        i_healthBars[i].SetActive(true);
                    }
                    else
                        i_healthBars[i].SetActive(false);
                }
                healthSlider.value = 0f;
                break;
            case 1:
                for (int i = 0; i < i_healthBars.Length; i++)
                {
                    if (i_healthBars[i].name.Contains("3"))
                    {
                        i_healthBars[i].SetActive(true);
                    }
                    else
                        i_healthBars[i].SetActive(false);
                }
                healthSlider.value = 0.25f;
                // Enable Health Low Overlay
                i_healthLow.SetActive(true);
                break;
            case 2:
                for (int i = 0; i < i_healthBars.Length; i++)
                {
                    if (i_healthBars[i].name.Contains("2"))
                    {
                        i_healthBars[i].SetActive(true);
                    }
                    else
                        i_healthBars[i].SetActive(false);
                }
                healthSlider.value = 0.5f;
                break;
            case 3:
                for (int i = 0; i < i_healthBars.Length; i++)
                {
                    if (i_healthBars[i].name.Contains("1"))
                    {
                        i_healthBars[i].SetActive(true);
                    }
                    else
                        i_healthBars[i].SetActive(false);
                }
                healthSlider.value = 0.75f;
                break;
            case 4:
                for (int i = 0; i < i_healthBars.Length; i++)
                {
                    if (i_healthBars[i].name.Contains("0"))
                    {
                        i_healthBars[i].SetActive(true);
                    }
                    else
                        i_healthBars[i].SetActive(false);
                }
                break;
            default:
                break;
        }
    }
}
