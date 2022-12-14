using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour    
{
       Vector3 respawnPoint = new Vector3 (7.23f,2.67f,7.37f);

     public AudioClip jump;
     public AudioClip backgroundMusic;

     public AudioSource sfxPlayer;
     public AudioSource musicPlayer;
     

     public float maxSpeed = 3.0f;
     public float rotation = 0.0f;
     public float camRotation = 0.0f;
     GameObject cam;
     Rigidbody myRigidbody;

     bool isOnGround;
     public GameObject  groundChecker;
     public LayerMask groundLayer;
     public float jumpforce= 300.0f;

     float rotationSpeed = 2.0f;
     float camRotationSpeed = 1.5f;
    
     void Start()
    {
        cam = GameObject.Find("Main Camera");
        myRigidbody = GetComponent<Rigidbody>();
        musicPlayer.clip = backgroundMusic;
        musicPlayer.loop = true;
        musicPlayer.Play();
        sfxPlayer.PlayOneShot(jump); 
    }

    // Update is called once per frame
    void Update()
    {
       isOnGround = Physics.CheckSphere(groundChecker.transform.position,0.1f,groundLayer);

       if (isOnGround==true && Input.GetKeyDown(KeyCode.Space))
       {
         myRigidbody.AddForce(transform.up * jumpforce);
       }
        Vector3 newVelocity = transform.forward * Input.GetAxis("Vertical") * maxSpeed;
        myRigidbody.velocity = new Vector3(newVelocity.x, myRigidbody.velocity.y, newVelocity.z);

        rotation = rotation + Input.GetAxis("Mouse X") * rotationSpeed;
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, rotation, 0.0f));

        camRotation = camRotation - Input.GetAxis("Mouse Y") * camRotationSpeed;
        cam.transform.localRotation = Quaternion.Euler(new Vector3(camRotation, 0.0f, 0.0f));

       
          
        
    }

     private void OnTriggerEnter(Collider other)
     {
            if (other.tag == "Death box")
    {
          transform.position = respawnPoint;
    }
     }
}
