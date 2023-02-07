using System;
using UnityEngine;
using System.Collections;
using System.Text;
using GameServices;
using Model;
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

	async void GetSteamTicket()
	{
		if (SteamManager.Initialized)
		{
			//SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, 4);
			Byte[] ticket = new byte[1024];
			uint sessionTicketSize;
			HAuthTicket ticketRequest = SteamUser.GetAuthSessionTicket(ticket, 1024, out sessionTicketSize);
			Array.Resize(ref ticket, (int)sessionTicketSize);
			
			string hex = BitConverter.ToString(ticket).Replace("-", "");
			Player player = await GameService.LoginPlayer(hex);
			
			Debug.Log(player.Gamertag);
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
