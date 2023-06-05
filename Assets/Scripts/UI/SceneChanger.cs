using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	static public SceneChanger instance;
	public SceneTransition sceneTransition;
	private void Awake()
	{
		if (instance == null)
			instance = this;
	}

	private void Start()
	{
		var obj = GameObject.Find("BlackScreen");
		sceneTransition = obj.GetComponent<SceneTransition>();
	}

	public void MoveToSceneByIndex(int idx)
	{
		StartCoroutine(LoadSceneCoroutine(idx));
	}

	private IEnumerator LoadSceneCoroutine(int idx)
	{
		// Fade out
		sceneTransition.FadeOut();

		// Wait for the fade out to complete
		yield return new WaitForSeconds(sceneTransition.transitionTime);

		// Load the new scene
		SceneManager.LoadScene(idx);

		// Fade in
		sceneTransition.FadeIn();
	}
}
