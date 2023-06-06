using Com.LuisPedroFonseca.ProCamera2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	ProCamera2DShake proCamera2DShake;
	private void Start()
	{
		proCamera2DShake = Camera.main.GetComponent<ProCamera2DShake>();
		proCamera2DShake.ProCamera2D.AddCameraTarget(GameObject.Find("Player").transform);
	}
}
