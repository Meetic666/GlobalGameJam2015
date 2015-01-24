using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour 
{
	public List<Rect> m_TextAreas;
	public List<string> m_Texts;
	
	void OnGUI()
	{
		for(int i = 0; i < m_TextAreas.Count; i++)
		{
			Rect textArea = new Rect(0,0,0,0);
			textArea.x = m_TextAreas[i].x * camera.pixelWidth;
			textArea.y = m_TextAreas[i].y * camera.pixelHeight;
			textArea.width = m_TextAreas[i].width * camera.pixelWidth;
			textArea.height = m_TextAreas[i].height * camera.pixelHeight;
			
			if(GUI.Button (textArea, m_Texts[i]))
			{
				if(m_Texts[i] == "Quit")
				{
					Application.Quit();
				}
				else if(m_Texts[i] == "Play")
				{
					Application.LoadLevel ("PlayerConfirmation");
				}
			}
		}
	}
}
