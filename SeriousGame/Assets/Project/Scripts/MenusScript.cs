using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenusScript : MonoBehaviour {

    public GameObject mainPanel;
    public GameObject settingsPanel;
    public GameObject difficultyPanel;
    public GameObject levelsNormalPanel;
    public GameObject levelsHardPanel;
    public GameObject pauseMenuPanel;
    public GameObject infosMenuPanel;
    public Button pauseButton;
    public Text testText;
    public Slider sliderVolume;

    public Button NormalDifficultyButton;
    public Button HardDifficultyButton;

    public Sprite pauseButtonON;
    public Sprite pauseButtonOFF;

    private GameObject currentPanel;
    private GameObject lastPanel;
    private int soundEnabled;
    private float volume;
    private string choosenDifficulty;
    private Color disabledColor;
    private Color enabledColor;
    private bool gamePaused;
    private bool infosDisplayed;
    private string initialMode;

	// Use this for initialization
	void Start () {
        soundEnabled = PlayerPrefs.GetInt("SoundEnabled", 1);
        choosenDifficulty = PlayerPrefs.GetString("ChoosenDifficulty", "Normal");
        volume = PlayerPrefs.GetFloat("Volume", 1.0f);

        AudioListener.volume = volume;

        if (soundEnabled == 0)
        {
            AudioListener.pause = true;
        }

        initialMode = PlayerPrefs.GetString("InitialMode", "LoadMainPanel");
        
        if(initialMode.Contains("Normal"))
        {
            currentPanel = levelsNormalPanel;
            mainPanel.SetActive(false);
            currentPanel.SetActive(true);
        }
        else if (initialMode.Contains("Hard"))
        {
            currentPanel = levelsHardPanel;
            mainPanel.SetActive(false);
            currentPanel.SetActive(true);
        }
        else
        {
            currentPanel = mainPanel;
            currentPanel.SetActive(true);
        }

        disabledColor = Color.grey;
        enabledColor = Color.white;
        gamePaused = false;
        infosDisplayed = false;
	}

    public void ChooseDifficulty()
    {
        lastPanel = currentPanel;
        currentPanel.SetActive(false);
        difficultyPanel.SetActive(true);
        currentPanel = difficultyPanel;
    }

    public void ChangeSettings()
    {
        lastPanel = currentPanel;
        currentPanel.SetActive(false);
        settingsPanel.SetActive(true);
        currentPanel = settingsPanel;
    }

    public void Back(GameObject last)
    {
        currentPanel.SetActive(false);
        last.SetActive(true);
        currentPanel = last;
    }

    public void ValidateSettings()
    {
        volume = sliderVolume.value / 100;
        PlayerPrefs.SetFloat("Volume", volume);

        currentPanel.SetActive(false);
        lastPanel.SetActive(true);
        currentPanel = lastPanel;
    }

    public void ValidateDifficulty()
    {
        currentPanel.SetActive(false);
        lastPanel = currentPanel;
        if (choosenDifficulty.Equals("Normal"))
        {
            currentPanel = levelsNormalPanel;
        }
        else
        {
            currentPanel = levelsHardPanel;
        }
        currentPanel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
        PlayerPrefs.DeleteAll();
    }

    public void ToggleSound()
    {
        if (soundEnabled != 0)
        {
            soundEnabled = 0;
            PlayerPrefs.SetInt("SoundEnabled", 0);
            AudioListener.pause = true;
        }
        else
        {
            soundEnabled = 1;
            PlayerPrefs.SetInt("SoundEnabled", 1);
            AudioListener.pause = false;
        }
    }

    public void ToggleDifficulty(Button clickedButton)
    {
        choosenDifficulty = clickedButton.name;
        PlayerPrefs.SetString("ChoosenDifficulty", choosenDifficulty);
        if (clickedButton.name.Equals(NormalDifficultyButton.name))
        {
            HardDifficultyButton.image.color = disabledColor;
            NormalDifficultyButton.image.color = enabledColor;
        }
        else
        {
            HardDifficultyButton.image.color = enabledColor;
            NormalDifficultyButton.image.color = disabledColor;
        }
    }

    public void MainToHome()
    {
        currentPanel.SetActive(false);
        currentPanel = mainPanel;
        lastPanel = mainPanel;
        currentPanel.SetActive(true);
    }

    public void GameToHome()
    {
        PlayerPrefs.SetString("InitialMode", "TitleScreenMode");
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void GameToLevelSelection()
    {
        if (choosenDifficulty.Equals("Normal"))
        {
            PlayerPrefs.SetString("InitialMode", "LoadNormalDifficultyPanel");
        }
        else
        {
            PlayerPrefs.SetString("InitialMode", "LoadHardDifficultyPanel");
        }
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void Pause()
    {
        if(!gamePaused)
        {
            pauseButton.image.sprite = pauseButtonOFF;
            Time.timeScale = 0.0f;
            pauseMenuPanel.SetActive(true);
            gamePaused = true;
        }
        else
        {
            pauseButton.image.sprite = pauseButtonON;
            Time.timeScale = 1.0f;
            pauseMenuPanel.SetActive(false);
            gamePaused = false;
        } 
    }

    public void DisplayInfos()
    {
        pauseButton.interactable = false;
        if (!gamePaused)
        {
            pauseButton.image.sprite = pauseButtonOFF;
            Time.timeScale = 0.0f;
            gamePaused = true;
        }

        testText.text = PlayerPrefs.GetString("LastInfo", "erreur");

        infosMenuPanel.SetActive(true);
    }

    public void HideInfos()
    {
        if (gamePaused)
        {
            pauseButton.interactable = true;
            Pause();
        }

        infosMenuPanel.SetActive(false);

        // Si fin de niveau (victoire ou défaite), on retourne à l'écran de sélection des niveaux
        if (PlayerPrefs.GetInt("GameOver", 0) == 1)
        {
            GameToLevelSelection();
        }
    }

    public void LoadInfo(string infoID)
    {
        PlayerPrefs.SetString("LastInfo", PlayerPrefs.GetString(infoID, "erreur"));
        DisplayInfos();
    }

    public void LoadLevel(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }

    /*void OnApplicationExit()
    {
        PlayerPrefs.DeleteKey("InitialMode");
    }*/
}
