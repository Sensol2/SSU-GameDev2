using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider hpSlider;
    public Slider mpSlider;
	Player player;

	private void Start()
	{
		player = GameManager.instance.player;
	}
	private void Update()
	{
		hpSlider.value = player.hp;
		mpSlider.value = player.mp;
	}
}
