using UnityEngine;

public class HorseBERSERKBOSSButton : MonoBehaviour
{
	private const float updateCycle = 1f;
	private float lastUpdate = 0f;
	private GameObject berserkBossButton;

	private void Awake()
	{
		berserkBossButton = GameObject.Find("HorseBERSERKBOSSButton");
		berserkBossButton.SetActive(false);
		UpdateBERSERKBOSSButton();
	}

	private void Update()
	{
		lastUpdate += Time.deltaTime;

		if (lastUpdate >= updateCycle)
		{
			UpdateBERSERKBOSSButton();
			lastUpdate = 0f;
		}
	}

	private void UpdateBERSERKBOSSButton()
	{
		var currentZodiacSign = GameManager.BerserkSystem.GetCurZodiacSign();
		Debug.Log($"Current Zodiac Sign: {currentZodiacSign}");
		ActivateBERSERKBOSSButton(currentZodiacSign);
	}

	private void ActivateBERSERKBOSSButton(BerserkSystemManager.ZodiacSign zodiacSign)
	{
		switch (zodiacSign)
		{
			case BerserkSystemManager.ZodiacSign.HORSE:
				berserkBossButton.SetActive(true);
				Debug.Log("HorseERSERKBOSSButton activated.");
				break;
			default:
				berserkBossButton.SetActive(false);
				Debug.Log("HorseERSERKBOSSButton deactivated.");
				break;
		}
	}
}