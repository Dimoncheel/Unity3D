using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint3 : MonoBehaviour
{
    public static bool isActivated;

    [SerializeField]
    private UnityEngine.UI.Image countdown;
    private const float startTime = 40;
    private float countdownTime;

    void Start()
    {
        CheckPoint3.isActivated = false;
        countdownTime = startTime;
    }

    void Update()
    {
        if (CheckPoint2.isActivated)
        {
            countdownTime -= Time.deltaTime;   

            if (countdownTime < 0)            
            {
                GameStat.PassCheckpoint3(false);
                this.gameObject.SetActive(false);
            }
            else
            {
                GameStat.Checkpoint3Fill =
                countdown.fillAmount =          
                    countdownTime / startTime;   
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.SetActive(false);
        GameStat.PassCheckpoint3(true);
    }
}
