﻿using EventManagerBased.Events;
using UnityEngine;
using UnityEngine.Events;

namespace EventManagerBased
{
	public class FootSteps : MonoBehaviour
	{
		[SerializeField] private float rate = 3.0f;
		
		private float timeBetweenFootSteps;
		private float timeSinceLastFootStep;

		private void Awake()
		{
			timeBetweenFootSteps = 1.0f / rate;
		}

		// When the foot steps are enabled once again, make it so that the foot step is performed instantly
		private void OnEnable()
		{
			timeSinceLastFootStep = timeBetweenFootSteps;
		}

		// This will not run when the foot steps are disabled
		private void Update()
		{
			if (timeSinceLastFootStep >= timeBetweenFootSteps)
			{
				timeSinceLastFootStep -= timeBetweenFootSteps;
				EventManager.RaiseEvent(new FootStepPerformedGameEvent());
			}

			timeSinceLastFootStep += Time.deltaTime;
		}

		private void OnValidate()
		{
			rate = Mathf.Clamp(rate, 0.01f, rate);
		}
	}
}