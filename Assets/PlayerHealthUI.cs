using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PlayerHealthUI : MonoBehaviour
{
	[SerializeField] private Player player = null;

	private Text healthUIText = null;

	private void Awake()
	{
		healthUIText = GetComponent<Text>();
	}

	private void Update()
	{
		// Okay, but this is polling the value and updating the UI EVERY SINGLE FRAME, while the health doesn't change that often.
		healthUIText.text = player.Health.ToString();
	}
}
