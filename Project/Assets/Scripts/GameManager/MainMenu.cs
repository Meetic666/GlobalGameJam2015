using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour 
{
	public List<Rect> m_TextAreas;
	public List<string> m_Texts;

	int m_SelectedIndex = 0;

	float m_PreviousVertical;

	void Update()
	{
		float vertical = Input.GetAxis ("Vertical");

		if(vertical != 0.0f && (m_PreviousVertical == 0.0f || Mathf.Sign (vertical) == - Mathf.Sign (m_PreviousVertical)))
		{
			m_SelectedIndex += (int) Mathf.Sign (vertical);

			if(m_SelectedIndex < 0)
			{
				m_SelectedIndex = m_TextAreas.Count - 1;
			}
			else if(m_SelectedIndex >= m_TextAreas.Count)
			{
				m_SelectedIndex = 0;
			}
		}

		bool jump = false;

		for(int i = 1; i <= 4; i++)
		{
			if(Input.GetButtonDown("Jump" + i))
			{
				jump = true;
			}
		}

		if(jump)
		{
			HandleSelection ();
		}

		m_PreviousVertical = vertical;
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

			Color previousColor = GUI.contentColor;

			if(i == m_SelectedIndex)
			{
				GUI.contentColor = Color.yellow;
			}

			if(GUI.Button (textArea, m_Texts[i]))
			{
				m_SelectedIndex = i;

				HandleSelection();
			}

			GUI.contentColor = previousColor;
		}
	}

	void HandleSelection()
	{
		if(m_Texts[m_SelectedIndex] == "Quit")
		{
			Application.Quit();
		}
		else if(m_Texts[m_SelectedIndex] == "Play")
		{
			Application.LoadLevel ("PlayerConfirmation");
		}
	}
}
