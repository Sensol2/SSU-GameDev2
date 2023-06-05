using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
	[SerializeField]
	public int sceneID;
	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.CompareTag("Player"))
		{
			SceneChanger.instance.MoveToSceneByIndex(sceneID);
		}
	}
}
