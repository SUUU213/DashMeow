using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class ResultUI : MonoBehaviour
{
	[Header("Scene")]
	[SerializeField] private string gameModeSceneToLoad;
	[Space]
	[Header("UI")]
	[SerializeField] private GameObject resultUI;
	[SerializeField] private TextMeshProUGUI totalScoreText;
	[SerializeField] private TextMeshProUGUI rewardSushiText;
	[SerializeField] private TextMeshProUGUI DesirePieceiText;

	private void Start()
    {
		GameManager.OnGameEndChanged += DisplayResultUI;
		resultUI.SetActive(false);
	}

	private void OnDestroy()
	{
		GameManager.OnGameEndChanged -= DisplayResultUI;
	}

	public void DisplayResultUI()
	{
		Time.timeScale = 0f;
		int totalScore = GameManager.Score.GetTotalScore();
		totalScoreText.text = totalScore.ToString();
		resultUI.SetActive(true);
	}

	public void UpdateRewardSushiText(int reward)
	{
		rewardSushiText.text = "초밥 : " + reward;
	}

	public void UpdateDesirePieceiText(int rewardValue)
	{
		DesirePieceiText.text = "깨진 염원 조각 : " + rewardValue;
	}

	public void RestartButton()
	{	
		Time.timeScale = 1f;
		GameManager.Score.Reset();
		GameManager.Instance.ResetAllDebuffs();
		UnitySceneManager.LoadScene(UnitySceneManager.GetActiveScene().buildIndex);
	}

	public void QuitButton()
	{
		Time.timeScale = 1f;
		GameManager.Score.Reset();
		GameManager.Instance.ResetAllDebuffs();
		UnitySceneManager.LoadScene(gameModeSceneToLoad);
	}
}