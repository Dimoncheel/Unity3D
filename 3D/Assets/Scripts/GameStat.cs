using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStat : MonoBehaviour
{
    #region Clock / GameTime
    private static TMPro.TextMeshProUGUI Clock;
    
    private static float _gameTime;
    private static string point1="";
    private static string point2="";
    private static string point3="";
    public static float GameTime
    {
        get => _gameTime;
        set
        {
            _gameTime = value;
            UpdateTime();
        }
    }

    public static string Point1
    {
        get => point1;
        set
        {
            point1 = value;
            
        }
    }
    public static string Point2
    {
        get => point2;
        set
        {
            point2 = value;

        }
    }
    public static string Point3
    {
        get => point3;
        set
        {
            point3 = value;

        }
    }

    private static void UpdateTime()
    {
        int t = (int)_gameTime;
        GameStat.Clock.text = $"{t / 3600 % 24:00}:{t / 60 % 60:00}:{t % 60:00}.{(int)((_gameTime - t) * 10):0}";
    }
    #endregion

    #region Checkpoint1
    private static UnityEngine.UI.Image ImageCheckpoint1;
    private static float _checkpoint1Fill;
    public static float Checkpoint1Fill
    {
        get => _checkpoint1Fill;
        set
        {
            _checkpoint1Fill = value;
            UpdateCheckpoint1();
        }
    }
    private static void UpdateCheckpoint1()
    {
        if (Checkpoint1Fill >= 0 && Checkpoint1Fill <= 1)
        {
            ImageCheckpoint1.fillAmount = Checkpoint1Fill;
            ImageCheckpoint1.color = new Color(
                1 - Checkpoint1Fill,   
                Checkpoint1Fill,      
                0.1f                  
            );
        }
        else
            Debug.LogError("UpdateCheckpoint1: fillAmount = " + Checkpoint1Fill);
    }

    public static void PassCheckpoint1(bool status)
    {
      
        Checkpoint1Fill = 0.96f;
        ImageCheckpoint1.color = status ? Color.green : Color.red;
        int t = (int)_gameTime;
        Point1 = $"\nFirst check point:{t / 3600 % 24:00}:{t / 60 % 60:00}:{t % 60:00}.{(int)((_gameTime - t) * 10):0}";
        
    }
    #endregion

    #region Checkpoint2
    private static UnityEngine.UI.Image ImageCheckpoint2;
    private static float _checkpoint2Fill;
    public static float Checkpoint2Fill
    {
        get => _checkpoint2Fill;
        set
        {
            _checkpoint2Fill = value;
            UpdateCheckpoint2();
        }
    }
    private static void UpdateCheckpoint2()
    {
        if (Checkpoint2Fill >= 0 && Checkpoint2Fill <= 1)
        {
            ImageCheckpoint2.fillAmount = Checkpoint2Fill;
            ImageCheckpoint2.color = new Color(
                1 - Checkpoint2Fill,  
                Checkpoint2Fill,       
                0.1f                  
            );
        }
        else
            Debug.LogError("UpdateCheckpoint2: fillAmount = " + Checkpoint2Fill);
    }
    
    public static void PassCheckpoint2(bool status)
    {
        Checkpoint2Fill = 0.96f;
        ImageCheckpoint2.color = status ? Color.green : Color.red;
        int t = (int)_gameTime;
        Point2= $"\nSecond check point:{t / 3600 % 24:00}:{t / 60 % 60:00}:{t % 60:00}.{(int)((_gameTime - t) * 10):0}";
    }
    #endregion

    #region Checkpoint3
    private static UnityEngine.UI.Image ImageCheckpoint3;
    private static float _checkpoint3Fill;
    public static float Checkpoint3Fill
    {
        get => _checkpoint3Fill;
        set
        {
            _checkpoint3Fill = value;
            UpdateCheckpoint3();
        }
    }
    private static void UpdateCheckpoint3()
    {
        if (Checkpoint3Fill >= 0 && Checkpoint3Fill <= 1)
        {
            ImageCheckpoint3.fillAmount = Checkpoint3Fill;
            ImageCheckpoint3.color = new Color(
                1 - Checkpoint3Fill,   
                Checkpoint3Fill,       
                0.1f                   
            );
        }
        else
            Debug.LogError("UpdateCheckpoint3: fillAmount = " + Checkpoint3Fill);
    }
   
    public static void PassCheckpoint3(bool status)
    {
        Checkpoint3Fill = 0.96f;
        ImageCheckpoint3.color = status ? Color.green : Color.red;
        int t = (int)_gameTime;
        Point3= $"\nThird check point:{t / 3600 % 24:00}:{t / 60 % 60:00}:{t % 60:00}.{(int)((_gameTime - t) * 10):0}";
        GameMenu.Show(messageText: "You Win",buttonText:"Exit");
        
    }
    #endregion

    void Start()
    {
        GameStat.Clock =
            GameObject.Find("Clock")
            .GetComponent<TMPro.TextMeshProUGUI>();

        
        GameStat.ImageCheckpoint1 =
            GameObject.Find(nameof(GameStat.ImageCheckpoint1))
            .GetComponent<UnityEngine.UI.Image>();

        GameStat.ImageCheckpoint2 =
           GameObject.Find(nameof(GameStat.ImageCheckpoint2))
           .GetComponent<UnityEngine.UI.Image>();
        GameStat.ImageCheckpoint3 =
          GameObject.Find(nameof(GameStat.ImageCheckpoint3))
          .GetComponent<UnityEngine.UI.Image>();

    }

    void LateUpdate()
    {
        GameStat.GameTime += Time.deltaTime;
    }

}
