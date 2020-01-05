using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiningItem : MonoBehaviour {

	public float shiningTime = 1f;
	public float width = 0.2f;

	SpriteRenderer sr;
	bool isShining = false;

	// Use this for initialization
	void Start () {

		sr = GetComponent<SpriteRenderer> ();
    }

	void Update () {
		if (!isShining) {
			isShining = true;
			StartCoroutine (Shine ());
		}
	}

	IEnumerator Shine () {
		float currentTime = 0;
		float speed = 1f / shiningTime;

		sr.material.SetFloat ("_Width", width);

		while (currentTime <= shiningTime) {
			currentTime += Time.deltaTime;
			float value = Mathf.Lerp (0, 1, speed * currentTime);
			sr.material.SetFloat ("_TimeController", value);
			yield return null;
		}
		yield return new WaitForSeconds (1f);
		sr.material.SetFloat ("_Width", 0);
		isShining = false;
	}

/*
	Rect GetUVs(Sprite sprite)	{
		Rect uv = sprite.rect;
		uv.x /= sprite.texture.width;
		uv.width /= sprite.texture.width;
		uv.y /= sprite.texture.height;
		uv.height /= sprite.texture.height;
		return uv;
	}
*/


}
