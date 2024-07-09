using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JellyCoinUI : MonoBehaviour
{
	[SerializeField] private TMP_Text jellyCoinUI;

	private void OnEnable()
	{
		GameManager.Score.OnScoreChanged += UpdateScoreText;
	}

	private void OnDisable()
	{
		GameManager.Score.OnScoreChanged -= UpdateScoreText;
	}

	private void UpdateScoreText(int value)
	{
		jellyCoinUI.text = "JellyCoin : " + value.ToString();
	}
}