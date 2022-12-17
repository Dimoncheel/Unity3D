using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint2 : MonoBehaviour
{
    public static bool isActivated;

    [SerializeField]
    private UnityEngine.UI.Image countdown;
    private const float startTime = 30;
    private float countdownTime;
    GameObject gate2;

    void Start()
    {
        CheckPoint2.isActivated = false;
        countdownTime = startTime;
        gate2 = GameObject.Find("Gates2");
    }

    void Update()
    {
        if (CheckPoint1.isActivated)
        {
            countdownTime -= Time.deltaTime;  

            if (countdownTime < 0)             
            {
                GameStat.PassCheckpoint2(false);
                this.gameObject.SetActive(false);
            }
            else
            {
                GameStat.Checkpoint2Fill =
                countdown.fillAmount =          
                    countdownTime / startTime;   
            }
        } 
    }
    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.SetActive(false);
        CheckPoint2.isActivated = true;
        gate2.SetActive(false);
        GameStat.PassCheckpoint2(true);
    }
}
