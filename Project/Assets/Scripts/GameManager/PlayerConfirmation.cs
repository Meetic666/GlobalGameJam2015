using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerConfirmation : MonoBehaviour 
{
	public List<Rect> m_TextAreas;

	void Start()
	{
		GameData.Instance.ResetPlayersConfirmed();
	}

	void Update()
	{
		for(int i = 1; i <= 4; i++)
		{
			bool confirm = Input.GetButtonDown("Jump" + i);
			bool back = Input.GetButtonDown("Back" + i);

			if(!GameData.Instance.PlayersJoined[i-1])
			{
				if(confirm)
				{
					GameData.Instance.JoinPlayer(i, true);
				}
			}
			else
			{
				if(!GameData.Instance.PlayersConfirmed[i-1])
				{
					if(confirm)
					{
						GameData.Instance.ConfirmPlayer(i, true);
					}
					else if(back)
					{
						GameData.Instance.JoinPlayer (i, false);
					}
				}
				else
				{
					if(back)
					{
						GameData.Instance.ConfirmPlayer (i, false);
					}
				}
			}
		}		
		
		if(GameData.Instance.NumberOfPlayersJoined > 0 && GameData.Instance.NumberOfPlayersJoined == GameData.Instance.NumberOfPlayersConfirmed)
		{
			string levelToLoad = "MultiPlayer";

			Application.LoadLevel (levelToLoad);
		}
	}
	
	void OnGUI()
	{
		for(int i = 0; i < m_TextAreas.Count; i++)
		{
			Rect textArea = new Rect(0,0,0,0);
			textArea.x = m_TextAreas[i].x * camera.pixelWidth;
			textArea.y = m_TextAreas[i].y * camera.pixelHeight;
			textArea.width = m_TextAreas[i].width * camera.pixelWidth;
			textArea.height = m_TextAreas[i].height * camera.pixelHeight;

			string text = "Press Space or A to join";

			if(GameData.Instance.PlayersJoined[i])
			{
				text = "Press space or A to go to lobby \nEscape or B to leave game";
			}

			if(GameData.Instance.PlayersConfirmed[i])
			{
				text = "Waiting for players \nEscape or B to leave lobby";
			}

			GUI.TextArea (textArea, text);
		}
	}
}
