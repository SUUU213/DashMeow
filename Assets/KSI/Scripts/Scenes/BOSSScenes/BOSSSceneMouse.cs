using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSSSceneMouse : MonoBehaviour
{
	private DebuffSystem debuffSystem;

	private void Start()
    {
		debuffSystem = FindObjectOfType<DebuffSystem>();

		debuffSystem.OnMouseDebuffChanged.Invoke();
		Debug.Log("OnMouseDebuffChanged.");
	}
}