using System;
using UnityEngine;

namespace ActionEventBased
{
	public class Player : MonoBehaviour
	{
		private int currentHealth;
		[SerializeField] private float footStepRate = 3.0f;

		private bool isMoving;

		[SerializeField] private int maxHealth = 100;
		[SerializeField] private float movementSpeed = 10.0f;

		private float timeBetweenFootSteps;
		private float timeSinceLastFootStep;

		private int CurrentHealth
		{
			get => currentHealth;
			set
			{
				currentHealth = value;
				HealthChanged?.Invoke(currentHealth);
			}
		}

		public event Action<int> HealthChanged;
		public event Action FootStepPerformed;

		private void Awake()
		{
			timeBetweenFootSteps = 1.0f / footStepRate;
		}

		private void Start()
		{
			CurrentHealth =
				maxHealth; // This invokes the event on startup as well, automatically updating the UI. We did not have to do anything special. Be careful with the order -> if this is called first (Awake?), and only then the UI subscribed to the event. So, there are no listeners at this point.
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
				FootStepPerformed?.Invoke();
			}

			timeSinceLastFootStep += Time.deltaTime;
		}

		private void Attack()
		{
			if (Input.GetButtonDown("Fire1")) CurrentHealth -= 5;
		}
	}
}