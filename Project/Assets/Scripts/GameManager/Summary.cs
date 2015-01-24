using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class DickishnessComment
{
	public float m_DickishnessThreshold;
	public string m_Message;
}

public class Summary : MonoBehaviour 
{
	public DickishnessComment[] m_Comments;

	List<string> m_CommentToUse = new List<string>(4);

	public List<Rect> m_DickishnessTextAreaPercentages;

	void Start()
	{
		for(int i = 0; i < GameData.Instance.DickPercentages.Count; i++)
		{
			string commentUsed = "";

			foreach(DickishnessComment comment in m_Comments)
			{
				if(GameData.Instance.DickPercentages[i] >= comment.m_DickishnessThreshold)
				{
					commentUsed = comment.m_Message;
				}
			}

			m_CommentToUse.Add ("You've survived " + GameData.Instance.Scores[i]  + " seconds.\nYou've killed "
				+ GameData.Instance.DeathTolls[i] + " people.\n" + commentUsed + "\n\n" + (int)(Random.value * 100) + "% of the players killed more people than you.\n"
					+ (int)(Random.value * 100) + "% of the players survived longer than you.\n");
		}
	}

	void Update()
	{
		for(int i = 1; i<= 4; i++)
		{
			if(Input.GetButtonDown("Jump" + i))
			{
				Application.LoadLevel ("MainMenu");
			}
		}
	}

	void OnGUI()
	{
		for(int i = 0; i < m_DickishnessTextAreaPercentages.Count; i++)
		{
			if(GameData.Instance.PlayersConfirmed[i])
			{
				Rect textArea = new Rect(0,0,0,0);
				textArea.x = m_DickishnessTextAreaPercentages[i].x * camera.pixelWidth;
				textArea.y = m_DickishnessTextAreaPercentages[i].y * camera.pixelHeight;
				textArea.width = m_DickishnessTextAreaPercentages[i].width * camera.pixelWidth;
				textArea.height = m_DickishnessTextAreaPercentages[i].height * camera.pixelHeight;

				GUI.TextArea (textArea, m_CommentToUse[i]);
			}
		}
	}
}
