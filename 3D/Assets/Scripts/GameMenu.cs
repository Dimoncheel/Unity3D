using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    public static bool SoundsEnabled { get; private set; }
    public static float SoundsVolume { get; private set; }

    private const string preferencesFilename = "Assets/Files/prefer.txt";

    private static GameObject MenuContent;
    private static TMPro.TextMeshProUGUI MenuMessage;
    private static TMPro.TextMeshProUGUI MenuButtonTitle;
    public static TMPro.TextMeshProUGUI MenuStatistics;

    private AudioSource backgroundMusic;

    private bool musicEnabled;
    private float musicVolume;

    void Start()
    {
        MenuContent = GameObject.Find("MenuContent");
        MenuMessage = GameObject.Find("MenuMessage")
                            .GetComponent<TMPro.TextMeshProUGUI>();
        MenuButtonTitle = GameObject.Find("MenuButtonTitle")
                            .GetComponent<TMPro.TextMeshProUGUI>();
        MenuStatistics = GameObject.Find("GameStatistics")
                            .GetComponent<TMPro.TextMeshProUGUI>();

        backgroundMusic = this.GetComponent<AudioSource>();

        var SoundsToggle = GameObject.Find("SoundsToggle")
                            .GetComponent<UnityEngine.UI.Toggle>();
        var SoundsSlider = GameObject.Find("SoundsVolume")
                            .GetComponent<UnityEngine.UI.Slider>();
        var MusicToggle = GameObject.Find("MusicToggle")
                            .GetComponent<UnityEngine.UI.Toggle>();
        var MusicSlider = GameObject.Find("MusicVolume")
                            .GetComponent<UnityEngine.UI.Slider>();
        if (this.LoadPreferences())
        {
            SoundsToggle.isOn = SoundsEnabled;
            SoundsSlider.value = SoundsVolume;
            MusicToggle.isOn = musicEnabled;
            MusicSlider.value = musicVolume;
        }
        else
        {
            SoundsEnabled = SoundsToggle.isOn;
            SoundsVolume = SoundsSlider.value;
            musicEnabled = MusicToggle.isOn;
            musicVolume = MusicSlider.value;
        }
        SoundsEnabled = SoundsToggle.isOn;
        SoundsVolume = SoundsSlider.value;
        musicEnabled = MusicToggle.isOn;
        musicVolume = MusicSlider.value;
        this.UpdateMusicState();

        if (MenuContent.activeInHierarchy)
            GameMenu.Show(MenuMessage.text, MenuButtonTitle.text);
    }

    void LateUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (MenuContent.activeInHierarchy) GameMenu.Hide();
            else GameMenu.Show();
        }
    }

    private void OnDestroy()
    {
        SavePreferences();
        
    }

    #region Event handlers
    public void MenuButtonClick()
    {
        if(CheckPoint3.isActivated)
        {
            Application.Quit();
        }
        else
        {
            GameMenu.Hide();
        }
        
    }
    public void MusicToggleChanged(bool isChecked)
    {
        musicEnabled = isChecked;
        UpdateMusicState();
    }
    public void MusicVolumeChanged(float value)
    {
        musicVolume = value;
        UpdateMusicState();
    }
    public void SoundsToggleChanged(bool isChecked)
    {
        GameMenu.SoundsEnabled = isChecked;
    }
    public void SoundsVolumeChanged(float value)
    {
        GameMenu.SoundsVolume = value;
    }
    #endregion

    private void UpdateMusicState()
    {
        backgroundMusic.volume = musicVolume;
        if (musicEnabled)
        {
            if (!backgroundMusic.isPlaying) backgroundMusic.Play();
        }
        else if (backgroundMusic.isPlaying) backgroundMusic.Stop();
    }

    public static void Show(                   
        string messageText = "Game paused",    
        string buttonText = "Resume")          
    {
        MenuContent.SetActive(true);

       
        MenuMessage.text = messageText;
        MenuButtonTitle.text = buttonText;

       
        MenuStatistics.text = $"Game Statistics:\n Time in game: {GameStat.GameTime:F1} s "+GameStat.Point1+GameStat.Point2+GameStat.Point3;

        Time.timeScale = 0f;
    }
    public static void Hide()    
    {
        MenuContent.SetActive(false);
        Time.timeScale = 1f;
    }

    private void SavePreferences()
    {
        System.IO.File.WriteAllText(preferencesFilename,
            $"{musicEnabled};{musicVolume};{SoundsEnabled};{SoundsVolume}"
        );
    }
    private bool LoadPreferences()
    {
        if (System.IO.File.Exists(preferencesFilename))
        {
            try
            {
                string[] data = System.IO.File.ReadAllText(preferencesFilename).Split(";");
                musicEnabled = Convert.ToBoolean(data[0]);
                musicVolume = Convert.ToSingle(data[1]);
                SoundsEnabled = Convert.ToBoolean(data[2]);
                SoundsVolume = Convert.ToSingle(data[3]);
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }
        return false;
    }
}
