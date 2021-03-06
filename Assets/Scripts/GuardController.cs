using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardController : MonoBehaviour
{
    public float lookDist;
    public Transform[] navPoints;
    AudioSource Audio;
    public AudioClip BuzzingClip;
    public AudioClip oofClip;
    public ParticleSystem Dizzytime;
    private int destPoint = 0;
    private bool pursuing;
    private bool guardStunned;
    private bool playerHiding;
    private Vector3 currentLocation;
    private GameObject player;
    private NavMeshAgent navAgent;

    // https://docs.unity3d.com/Manual/nav-AgentPatrol.html
    // https://docs.unity3d.com/ScriptReference/Physics.Raycast.html

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.autoBraking = false;
        pursuing = false;
        player = GameObject.FindGameObjectWithTag("Player");
        GotoNextPoint();
        Audio = GetComponent<AudioSource>();
        Dizzytime = GetComponentInChildren<ParticleSystem>();
        Dizzytime.Stop();
    }
    void GotoNextPoint()
    {
        if (navPoints.Length == 0)
        {
            return;
        }
        navAgent.destination = navPoints[destPoint].position;
        destPoint = (destPoint + 1) % navPoints.Length;
    }
    void Update()
    {
        Debug.Log("Guard is going to: " + navAgent.destination);
        playerHiding = player.GetComponent<PlayerController>().hiding;
        Audio.PlayOneShot(BuzzingClip);

        if (!navAgent.pathPending && navAgent.remainingDistance < 0.5f && pursuing == false)
        {
            GotoNextPoint();
            
        }
        else if (pursuing == true)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            navAgent.destination = player.transform.position;
            playerHiding = player.GetComponent<PlayerController>().hiding;
            Debug.Log("Chasing Player for Realsies");
        }
        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, lookDist, LayerMask.GetMask("NPC and PC")))
        {
            Debug.Log("Player Found!");
            pursuing = true;
        }
        if (playerHiding == true)
        {
            pursuing = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
        if (guardStunned == true)
        {
            gameObject.transform.position = currentLocation;
        }
    }
    void OnCollisionEnter(Collision Hit)
    {
        if (Hit.gameObject.tag == "Projectile")
        {
            currentLocation = gameObject.transform.position;
            StartCoroutine("Stunned");
            Destroy(Hit.gameObject);
            Audio.PlayOneShot(oofClip);
            Dizzytime.Play();
        }
    }
    IEnumerator Stunned()
    {
        guardStunned = true;
        yield return new WaitForSecondsRealtime(5);
        guardStunned = false;
        pursuing = false;
        navAgent.ResetPath();
        GotoNextPoint();
        Dizzytime.Stop();
    }
    public IEnumerator HitReset()
    {
        Debug.Log("Hit is being reset");
        guardStunned = true;
        yield return new WaitForSecondsRealtime(1);
        guardStunned = false;
        navAgent.ResetPath();
        GotoNextPoint();
        pursuing = false;
        Dizzytime.Stop();
    }
}
