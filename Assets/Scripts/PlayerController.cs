// Made by Fisher Hensly for use in DIG-4715 Group 10's Project 2.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 10f;
    public float force = 10f;
    public bool hiding;
    public GameObject projectilePrefab;
    public Camera mainCamera;
    public int ammo;
    public int lives;
    public Text Rocktext;
    public Text Livetext;
    AudioSource  Audio;
    public AudioClip HurtClip;
    public AudioClip ThrowClip;
    private Vector3 moveDirect;
    private Vector3 lookDirect;
    private Vector3 spawnLoc;
    private GameObject guardCont;

    void Start()
    {
        hiding = false;
        Cursor.lockState = CursorLockMode.Locked;
        spawnLoc = gameObject.transform.position;
        guardCont = GameObject.FindGameObjectWithTag("Enemy");
        Audio = GetComponent<AudioSource>();
    }
    void Update()
    {
        float x = Input.GetAxis ("Horizontal");
        float z = Input.GetAxis ("Vertical");

        moveDirect = transform.right * x + transform.forward * z;

        Rocktext.text = "Rocks: " + ammo;
        Livetext.text = "Lives: " + lives;

        if (lives == 0)
        {
            SceneManager.LoadScene("Lose");
            PauseMenu.InGame = false;
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (ammo > 0)
            {
                // The Camera has the X cord while Player has Y cord.
                lookDirect = new Vector3(mainCamera.transform.rotation.x, gameObject.transform.rotation.y, 0.5f);
                GameObject projectileShot = Instantiate(projectilePrefab, gameObject.transform.position + gameObject.transform.forward, Quaternion.Euler(lookDirect));
                ProjectileController projectileActive = projectileShot.GetComponent<ProjectileController>();
                projectileActive.Launch(force, gameObject);
                ammo--;
                Debug.Log("Ammocount: " + ammo);
                Audio.PlayOneShot(ThrowClip);
            }
        }
    }
    void FixedUpdate()
    {
        controller.Move(moveDirect * speed * Time.deltaTime);
    }
    void OnTriggerEnter(Collider Entered)
    {
        if (Entered.gameObject.tag == "HidingSpot")
        {
            hiding = true;
            Debug.Log ("Hiding: " + hiding);
        }
        if (Entered.gameObject.tag == "Ammunition")
        {
            ammo++;
            Destroy(Entered.gameObject);
            Debug.Log("Ammocount: " + ammo);
        }
        if (Entered.gameObject.tag == "Objective")
        {
            Debug.Log("You Win!"); // This is placeholder code.
            SceneManager.LoadScene("Win");
            PauseMenu.InGame = false;
        }
    }
    void OnTriggerExit(Collider Exited)
    {
        if (Exited.gameObject.tag == "HidingSpot")
        {
            hiding = false;
            Debug.Log ("Hiding: " + hiding);
        }
    }
    void OnCollisionEnter(Collision Struck)
    {
        if (Struck.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit is an Enemy");
            gameObject.GetComponent<CharacterController>().enabled = false;
            gameObject.transform.position = spawnLoc;
            gameObject.GetComponent<CharacterController>().enabled = true;
            guardCont.GetComponent<GuardController>().StartCoroutine("HitReset");
            lives--;
            Audio.PlayOneShot(HurtClip);
        }
    }
}