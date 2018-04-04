﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalNew : MonoBehaviour 
{
	[Tooltip("The player game object.")]
	[SerializeField] private GameObject player;

	[Tooltip("This is what the player will teleport too. Can use any empty game object.")]
	[SerializeField] private GameObject portalBuddy;

	[Tooltip("Glitchy effect script that is attached to the player camera.")]
	[SerializeField] private GlitchyEffect glitchyEffectScript;

	[Tooltip("Angle that the player will face when teleported. Rotates on Y axis.")]
	[Range(0.0f, 180.0f)] [SerializeField] private float lookAngle;

	private CustomRigidbodyFPSController playerController;
	public Camera playerCamera;
	private bool overThreshold;
    private AudioSource portalAudio;
    private bool hasPlayedAudio = false;

	void Start()
	{
		playerController = player.GetComponent<CustomRigidbodyFPSController> ();
        portalAudio = GetComponent<AudioSource>();
		//playerCamera = player.GetComponentInChildren<Camera> ();
	}

    private void Update()
    {
        if (!portalAudio.isPlaying && hasPlayedAudio)
        {
            this.gameObject.SetActive(false);
        }
    }

    void OnEnable()
	{
		Scope.ScopedIn += Teleport;
	}

	void OnDisable()
	{
		Scope.ScopedIn -= Teleport;
	}

	void Teleport(int i)
	{
		overThreshold = glitchyEffectScript.OverThreshold;

		if (overThreshold) 
		{
			player.transform.position = portalBuddy.transform.position;
			//player.transform.eulerAngles = new Vector3 (player.transform.rotation.x, lookAngle, player.transform.rotation.z);
			//playerCamera.transform.eulerAngles = new Vector3 (playerCamera.transform.rotation.x, lookAngle, playerCamera.transform.rotation.z);
			glitchyEffectScript.OverThreshold = false;
            if(!portalAudio.isPlaying && !hasPlayedAudio)
            {
                portalAudio.Play();
                hasPlayedAudio = true;
            }
		}
	}

	/*IEnumerator rotatePlayer()
	{
		playerController.enabled = false;
		player.transform.eulerAngles = new Vector3 (player.transform.rotation.x, lookAngle, player.transform.rotation.z);
		yield return new WaitForSeconds (1);
		playerController.enabled = true;
	}*/
}
