using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
	float speed = 2f;
	float lowerXValue = -24f;
	float upperXValue = 48f;
	public GameManager gameManager;

	void Update()
	{

		if (!gameManager.GetComponent<GameManager>().bossBattle)
        {
			transform.Translate(-speed * Time.deltaTime, 0, 0);

			if (transform.position.x <= lowerXValue)
			{
				transform.Translate(upperXValue, 0f, 0f);
			}
		}
	}

}