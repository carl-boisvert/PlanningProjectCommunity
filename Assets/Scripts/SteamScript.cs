using System;
using UnityEngine;
using System.Collections;
using Steamworks;
using UnityEngine.UI;

public class SteamScript : MonoBehaviour
{
	[SerializeField] private Button _setDataButton;
	protected Callback<LobbyCreated_t> _lobbyCreatedCallback;
	protected Callback<LobbyDataUpdate_t> _lobbyDataUpdateCallback;

	private ulong _lobbyId;

	private void OnEnable()
	{
		_setDataButton.onClick.AddListener(OnSetDataClicked);
		_lobbyCreatedCallback = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
		_lobbyDataUpdateCallback = Callback<LobbyDataUpdate_t>.Create(OnLobbyDataUpdated);
	}

	private void OnDisable()
	{
		_setDataButton.onClick.RemoveAllListeners();
		_lobbyCreatedCallback.Dispose();
		_lobbyDataUpdateCallback.Dispose();
	}

	void Start()
	{
		if (SteamManager.Initialized)
		{
			string name = SteamFriends.GetPersonaName();
			SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, 4);
			Debug.Log(name);
		}
	}

	void OnLobbyCreated(LobbyCreated_t lobbyCreated)
	{
		if (lobbyCreated.m_eResult == EResult.k_EResultOK)
		{
			Debug.Log($"Lobby was Created");
			_lobbyId = lobbyCreated.m_ulSteamIDLobby;
		}
	}

	private void OnSetDataClicked()
	{
		if (SteamMatchmaking.SetLobbyData(new CSteamID(_lobbyId), "data", "DataInLobby"))
		{
			Debug.Log($"Lobby data value was set!");
		}
	}

	private void OnLobbyDataUpdated(LobbyDataUpdate_t param)
	{
		string value = SteamMatchmaking.GetLobbyData(new CSteamID(_lobbyId), "data");
		Debug.Log($"Lobby data value for data {value}");
	}
}
