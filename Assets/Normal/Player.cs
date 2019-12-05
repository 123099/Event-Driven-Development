﻿using UnityEngine;
using UnityEngine.UI;

namespace Normal
{
    [RequireComponent(typeof(AudioSource))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 10.0f;
        [SerializeField] private float footStepRate = 3.0f;

        [SerializeField] private AudioClip[] footStepSounds = null;

        [SerializeField] private ParticleSystem footStepParticleEffect = null;

        [SerializeField] private Text playerHealthUiText = null;

        private float timeBetweenFootSteps = 0.0f;
        private float timeSinceLastFootStep = 0.0f;

        private AudioSource audioSource = null;

        public float Health { get; private set; } = 100.0f;

        private void Awake()
        {
            timeBetweenFootSteps = 1.0f / footStepRate;

            audioSource = GetComponent<AudioSource>();
        }

        // How much of this code actually relates to player logic, and how much to extra effects?
        // How many things is this single COMPONENT trying to accomplish?
        private void Update()
        {
            var movementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));

            if (movementInput.sqrMagnitude == 0.0f)
            {
                timeSinceLastFootStep = timeBetweenFootSteps;
                return;
            }

            transform.Translate(Time.deltaTime * movementSpeed * movementInput.normalized);

            if (timeSinceLastFootStep >= timeBetweenFootSteps)
            {
                timeSinceLastFootStep -= timeBetweenFootSteps;

                audioSource.PlayOneShot(footStepSounds[UnityEngine.Random.Range(0, footStepSounds.Length)]);
                Instantiate(footStepParticleEffect, transform.position, transform.rotation);
            }

            timeSinceLastFootStep += Time.deltaTime;

            // Add this here. But it doesn't work because of the return above.
            // Fix this by moving it up, but this still requires modifying the player and suddenly adding UI into the mix.
            // Another way is polling for the health from a player UI script.
            if (playerHealthUiText != null)
            {
                playerHealthUiText.text = Health.ToString();
            }

            if (Input.GetButtonDown("Fire1"))
            {
                Health -= 5.0f;
            }
        }
    }
}