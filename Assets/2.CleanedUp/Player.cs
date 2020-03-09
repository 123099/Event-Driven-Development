using UnityEngine;

namespace _2.CleanedUp
{
    [RequireComponent(typeof(AudioSource))]
    public class Player : MonoBehaviour
    {
        private AudioSource audioSource;

        [SerializeField] private ParticleSystem footStepParticleEffect;
        [SerializeField] private float footStepRate = 3.0f;

        [SerializeField] private AudioClip[] footStepSounds;

        private bool isMoving;

        [SerializeField] private int maxHealth = 100;
        [SerializeField] private float movementSpeed = 10.0f;

        private float timeBetweenFootSteps;
        private float timeSinceLastFootStep;

        public int CurrentHealth { get; private set; }
        public bool HasDealtDamage { get; private set; }
        public float DamageDealt { get; private set; }

        private void Awake()
        {
            timeBetweenFootSteps = 1.0f / footStepRate;

            audioSource = GetComponent<AudioSource>();
            CurrentHealth = maxHealth;
        }

        // How much of this code actually relates to player logic, and how much to extra effects?
        // How many things is this single COMPONENT trying to accomplish?
        // This is cleaner now, but it still tried to do too much and relies on too much. Because of that, it is impossible to test the player without having all the other settings!
        private void Update()
        {
            Move();
            ProgressFootSteps();
            Attack();
        }

        private void Move()
        {
            var movementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));

            if (Mathf.Abs(movementInput.sqrMagnitude) < 0.1f)
            {
                isMoving = false;
                timeSinceLastFootStep = timeBetweenFootSteps;
                return;
            }

            isMoving = true;
            transform.Translate(Time.deltaTime * movementSpeed * movementInput.normalized);
        }

        private void ProgressFootSteps()
        {
            if (!isMoving) return;

            if (timeSinceLastFootStep >= timeBetweenFootSteps)
            {
                timeSinceLastFootStep -= timeBetweenFootSteps;

                audioSource.PlayOneShot(footStepSounds[Random.Range(0, footStepSounds.Length)]);
                Instantiate(footStepParticleEffect, transform.position, transform.rotation);
            }

            timeSinceLastFootStep += Time.deltaTime;
        }

        private void Attack()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                const int damage = 5;
                DamageDealt = damage;
                HasDealtDamage = true;
                CurrentHealth -= 5;
            }
            else
            {
                DamageDealt = 0;
                HasDealtDamage = false;
            }
        }
    }
}