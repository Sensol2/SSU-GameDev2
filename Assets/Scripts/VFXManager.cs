using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    static public VFXManager instance;
	GameObject hitEffect;
	private void Awake()
	{
		instance = this;
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
