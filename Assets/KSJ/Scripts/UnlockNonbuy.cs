using UnityEngine;

public class UnlockNonbuy : MonoBehaviour
{
	public string objectName; // 객체 이름

	private void Start()
	{
		//초기화
		ResetUnlockStatus();
		// 이전에 저장된 상태를 불러와서 비활성화된 상태면 비활성화 시킴
		if (PlayerPrefs.GetInt(objectName + "_unlocked", 0) == 1)
		{
			gameObject.SetActive(false);
		}
		else
		{
			InvokeRepeating("Unlock", 0f, 1f);
		}
	}

	public void Unlock()
	{
		if (objectName == "magiccatskin")
		{
			if (DataManager.Instance.magicCatSkin == 1)
			{
				// 오브젝트를 비활성화하고 상태를 저장
				gameObject.SetActive(false);
				PlayerPrefs.SetInt(objectName + "_unlocked", 1);
				PlayerPrefs.Save();
			}
		}
		else if(objectName == "magiccatwallpaper")
		{
			if (DataManager.Instance.magicCatWallpaper == 1)
			{
				// 오브젝트를 비활성화하고 상태를 저장
				gameObject.SetActive(false);
				PlayerPrefs.SetInt(objectName + "_unlocked", 1);
				PlayerPrefs.Save();
			}
		}
		else if (objectName == "magiccatbackeffect")
		{
			if (DataManager.Instance.magicCatEffect == 1)
			{
				// 오브젝트를 비활성화하고 상태를 저장
				gameObject.SetActive(false);
				PlayerPrefs.SetInt(objectName + "_unlocked", 1);
				PlayerPrefs.Save();
			}
		}


		else if(objectName == "butterflycatskin")
		{

			if (DataManager.Instance.butterflyCatSkin == 1)
			{
				// 오브젝트를 비활성화하고 상태를 저장
				gameObject.SetActive(false);
				PlayerPrefs.SetInt(objectName + "_unlocked", 1);
				PlayerPrefs.Save();
			}
		
		}
		else if (objectName == "butterflycatwallpaper")
		{

			if (DataManager.Instance.butterflyCatWallpaper == 1)
			{
				// 오브젝트를 비활성화하고 상태를 저장
				gameObject.SetActive(false);
				PlayerPrefs.SetInt(objectName + "_unlocked", 1);
				PlayerPrefs.Save();
			}

		}
		else if (objectName == "butterflycateffect")
		{

			if (DataManager.Instance.butterflyCatEffect == 1)
			{
				// 오브젝트를 비활성화하고 상태를 저장
				gameObject.SetActive(false);
				PlayerPrefs.SetInt(objectName + "_unlocked", 1);
				PlayerPrefs.Save();
			}

		}

	}


	public void ResetUnlockStatus()
	{
		// 저장된 비활성화 상태를 리셋
		PlayerPrefs.DeleteKey(objectName + "_unlocked");
		PlayerPrefs.Save();

		// 오브젝트를 다시 활성화
		gameObject.SetActive(true);
	}
}