﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour
{
	public int score = 0;					// The player's score.


	private PlayerControl playerControl;	// Reference to the player control script.
	private int previousScore = 0;			// The score in the previous frame.

	void Update ()
	{
		// Set the score text.
		GetComponent<Text>().text = "Score: " + score;

		// Set the previous score to this frame's score.
		previousScore = score;
	}

}
