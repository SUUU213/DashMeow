using UnityEngine;
using BackEnd;
using UnityEngine.Events;

public class BackendGameData
{
	// 유저 정보를 불러오는데 성공했을 때 호출할 메서드 등록
	[System.Serializable]
	public class GameDataLoadEvent : UnityEvent { }
	public GameDataLoadEvent onGameDataLoadEvent = new GameDataLoadEvent();

	private static BackendGameData instance = null;
	public static BackendGameData Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new BackendGameData();
			}

			return instance;
		}
	}

	private UserGameData userGameData = new UserGameData();
	public UserGameData UserGameData => userGameData;

	private string gameDataRowInDate = string.Empty;

	/// <summary>
	/// 뒤끝 콘솔 테이블에 새로운 유저 정보 추가
	/// </summary>
	public void GameDataInsert()
	{
		// 유저 정보를 초기값으로 설정
		userGameData.Reset();

		// 테이블에 추가할 데이터로 가공
		Param param = new Param()
		{
			{"INFINITBestscore", userGameData.INFINITBestscore},
			{"cannedFood", userGameData.cannedFood },
			{"silverKey", userGameData.silverKey },
			{"goldKey", userGameData.goldKey },
			{"money", userGameData.money },
			{"brokenBlue", userGameData.brokenBlue },
			{"brokenRed", userGameData.brokenRed},
			{"brokenGreen", userGameData.brokenGreen }
		};

		// 첫 번째 매개변수는 뒤끝 콘솔의 "게임 정보 관리" 탭에 생성한 테이블 이름
		Backend.GameData.Insert("USER_DATA", param, callback =>
		{
			// 게임 정보 추가에 성공했을 때
			if (callback.IsSuccess())
			{
				// 게임 정보의 고유값
				gameDataRowInDate = callback.GetInDate();

				Debug.Log($"게임 정보 데이터 삽입에 성공했습니다. : {callback}");
			}
			// 실패했을 때
			else
			{
				Debug.LogError($"게임 정보 데이터 삽입에 실패했습니다. : {callback}");
			}
		});
	}

	/// <summary>
	/// 뒤끝 콘솔 테이블에서 유저 정보를 불러올 때 호출
	/// </summary>
	public void GameDataLoad()
	{
		Backend.GameData.GetMyData("USER_DATA", new Where(), callback =>
		{
			// 게임 정보 불러오기에 성공했을 때
			if (callback.IsSuccess())
			{
				Debug.Log($"게임 정보 데이터 불러오기에 성공했습니다. : {callback}");

				// JSON 데이터 파싱 성공
				try
				{
					LitJson.JsonData gameDataJson = callback.FlattenRows();

					// 받아온 데이터의 개수가 0이면 데이터가 없는 것
					if (gameDataJson.Count <= 0)
					{
						Debug.LogWarning("데이터가 존재하지 않습니다.");
					}
					else
					{
						// 불러온 게임 정보의 고유값
						gameDataRowInDate = gameDataJson[0]["inDate"].ToString();
						// 불러온 게임 정보를 userData 변수에 저장
						userGameData.INFINITBestscore = int.Parse(gameDataJson[0]["INFINITBestscore"].ToString());
						userGameData.cannedFood = int.Parse(gameDataJson[1]["cannedFood"].ToString());
						userGameData.silverKey = int.Parse(gameDataJson[2]["silverKey"].ToString());
						userGameData.goldKey = int.Parse(gameDataJson[3]["goldKey"].ToString());
						userGameData.money = int.Parse(gameDataJson[4]["money"].ToString());
						userGameData.brokenBlue = int.Parse(gameDataJson[5]["brokenBlue"].ToString());
						userGameData.brokenRed = int.Parse(gameDataJson[6]["brokenRed"].ToString());
						userGameData.brokenGreen = int.Parse(gameDataJson[7]["brokenGreen"].ToString());

						onGameDataLoadEvent?.Invoke();
					}
				}
				// JSON 데이터 파싱 실패
				catch (System.Exception e)
				{
					// 유저 정보를 초기값으로 설정
					userGameData.Reset();
					// try-catch 에러 출력
					Debug.LogError(e);
				}
			}
			// 실패했을 때
			else
			{
				Debug.LogError($"게임 정보 데이터 불러오기에 실패했습니다. : {callback}");
			}
		});
	}

	/// <summary>
	/// 뒤끝 콘솔 테이블에 있는 유저 데이터 갱신
	/// </summary>
	public void GameDataUpdate(UnityAction action = null)
	{
		if (userGameData == null)
		{
			Debug.LogError("서버에서 다운받거나 새로 삽입한 데이터가 존재하지 않습니다." +
						   "Insert 혹은 Load를 통해 데이터를 생성해주세요.");
			return;
		}

		Param param = new Param()
		{
			{"INFINITBestscore", userGameData.INFINITBestscore},
			{"cannedFood", userGameData.cannedFood },
			{"silverKey", userGameData.silverKey },
			{"goldKey", userGameData.goldKey },
			{"money", userGameData.money },
			{"brokenBlue", userGameData.brokenBlue },
			{"brokenRed", userGameData.brokenRed},
			{"brokenGreen", userGameData.brokenGreen }

		};

		// 게임 정보의 고유값(gameDataRowInDate)이 없으면 에러 메시지 출력
		if (string.IsNullOrEmpty(gameDataRowInDate))
		{
			Debug.LogError($"유저의 inDate 정보가 없어 게임 정보 데이터 수정에 실패했습니다.");
		}
		// 게임 정보의 고유값이 있으면 테이블에 저장되어 있는 값 중 inDate 컬럼의 값과
		// 소유하는 유저의 owner_inDate가 일치하는 row를 검색하여 수정하는 UpdateV2() 호출
		else
		{
			Debug.Log($"{gameDataRowInDate}의 게임 정보 데이터 수정을 요청합니다.");

			Backend.GameData.UpdateV2("USER_DATA", gameDataRowInDate, Backend.UserInDate, param, callback =>
			{
				if (callback.IsSuccess())
				{
					Debug.Log($"게임 정보 데이터 수정에 성공했습니다. : {callback}");

					action?.Invoke();

					onGameDataLoadEvent?.Invoke();
				}
				else
				{
					Debug.LogError($"게임 정보 데이터 수정에 실패했습니다. : {callback}");
				}
			});
		}
	}
}
