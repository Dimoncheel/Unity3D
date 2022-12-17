using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private Rigidbody rb;
    private AudioSource hitWallSound;
    private AudioSource hitGateSound;

    private Vector3 jump = Vector3.up * 100;
    private Vector3 forceDirection;
    private const float FORCE_APML = 2;


    void Start()
    {
        rb = this.GetComponent<Rigidbody>();

        AudioSource[] audioSources =             
            this.GetComponents<AudioSource>();    
        hitWallSound = audioSources[0];
        hitGateSound = audioSources[1];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(jump);
        }
        float fx = Input.GetAxis("Horizontal");
        float fy = Input.GetAxis("Vertical");       
       

        forceDirection = cam.transform.forward;     
        
        forceDirection.y = 0;     
        
        forceDirection = forceDirection.normalized * fy;
       

        
       
        forceDirection += cam.transform.right * fx;
       

        rb.AddForce(forceDirection * FORCE_APML);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (GameMenu.SoundsEnabled)
        {
            AudioSource sound = other.gameObject.tag switch
            {
                "Wall" => hitWallSound,
                "Gate" => hitGateSound,
                _ => null
            };
            if (sound != null)
            {
                sound.volume = GameMenu.SoundsVolume;
                sound.Play();
            }
        }
    }
}
