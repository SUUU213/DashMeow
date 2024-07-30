using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSSScenePig : MonoBehaviour
{
	private DebuffSystem debuffSystem;

	private void Start()
    {
		debuffSystem = FindObjectOfType<DebuffSystem>();

		debuffSystem.OnPigDebuffChanged.Invoke();
		Debug.Log("OnPigDebuffChanged.");
	}
}