using UnityEngine;
using System.Collections;

[System.Serializable]
public class DickishnessComment
{
	public float m_DickishnessThreshold;
	public string m_Message;
}

public class Summary : MonoBehaviour 
{
	public DickishnessComment[] m_Comments;

	string m_CommentToUse;

	public Rect m_DickishnessTextAreaPercentages;

	void Start()
	{
		string commentUsed = "";

		foreach(DickishnessComment comment in m_Comments)
		{
			if(GameData.Instance.DickPercentage >= comment.m_DickishnessThreshold)
			{
				commentUsed = comment.m_Message;
			}
		}

		m_CommentToUse = "You've survived " + GameData.Instance.Score  + " seconds.\nYou've killed "
			+ GameData.Instance.DeathToll + " people.\n" + commentUsed + "\n\n" + (int)(Random.value * 100) + "% of the players killed more people than you.\n"
				+ (int)(Random.value * 100) + "% of the players survived longer than you.\n";
	}

	void OnGUI()
	{
		Rect textArea = new Rect(0,0,0,0);
		textArea.x = m_DickishnessTextAreaPercentages.x * camera.pixelWidth;
		textArea.y = m_DickishnessTextAreaPercentages.y * camera.pixelHeight;
		textArea.width = m_DickishnessTextAreaPercentages.width * camera.pixelWidth;
		textArea.height = m_DickishnessTextAreaPercentages.height * camera.pixelHeight;

		GUI.TextArea (textArea, m_CommentToUse);
	}
}
