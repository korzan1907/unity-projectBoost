using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{

    
    [SerializeField] float mainThrust=500;
    [SerializeField] float rotationThrust=50;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem leftBoosterParticles;

    [SerializeField] ParticleSystem rightBoosterParticles;

    [SerializeField] ParticleSystem mainBoosterParticles;

    Rigidbody rb;
    AudioSource audioSource;
    
    // bool isAlive;
    
    // Start is called before the first frame update
    void Start()
    {
        // isAlive=true;
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() {
        if(Input.GetKey(KeyCode.Space))
        {
            // Debug.Log("Pressed space - Thrusting");
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }

    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime); //rb.AddRelativeForce(0,1,0); // (1,1,1) add relative force 1 on x, 1 on y and 1 on z
        if (!audioSource.isPlaying)
        {
            // audioSource.Play();
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainBoosterParticles.isPlaying)
        {
            mainBoosterParticles.Play();
        }
    }
    private void StopThrusting()
    {
        audioSource.Stop();
        mainBoosterParticles.Stop();
    }

    void ProcessRotation(){
        if(Input.GetKey(KeyCode.A))
        {
            // Debug.Log("Pressed - Left");
            RotateLeft();

        }
        else if(Input.GetKey(KeyCode.D))
        {
            // Debug.Log("Pressed - Right");
            RotateRight();
        }
        else
        {
            StopRotating();
        }

    }


    private void StopRotating()
    {
        leftBoosterParticles.Stop();
        rightBoosterParticles.Stop();
    }

    private void RotateRight()
    {
        if (!rightBoosterParticles.isPlaying)
        {
            rightBoosterParticles.Play();
        }
        ApplyRotation(-rotationThrust);
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if (!leftBoosterParticles.isPlaying)
        {
            leftBoosterParticles.Play();
        }
    }

    private void ApplyRotation(float rotationThisFrame){
        rb.freezeRotation = true; //freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward*Time.deltaTime*rotationThisFrame);
        rb.freezeRotation = false;
    }
}
