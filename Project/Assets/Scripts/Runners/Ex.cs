using UnityEngine;
using System.Collections;

public class Ex : People 
{
	public float m_LateralSpeed;

	protected override void UpdateVirtual ()
	{
		if(m_Player != null)
		{
			Vector3 newPosition = transform.localPosition;
			newPosition.x = Mathf.Lerp (newPosition.x, m_Player.transform.localPosition.x, m_LateralSpeed * Time.deltaTime);
			transform.localPosition = newPosition;
		}
	}
}
