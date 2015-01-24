using UnityEngine;
using System.Collections;

public class Ex : People 
{
	public float m_LateralSpeed;

	protected override void UpdateVirtual ()
	{
		float minDistance = Mathf.Infinity;
		Player playerToUse = null;

		foreach(Player player in m_Players)
		{
			if(player.gameObject.activeSelf)
			{
				float distance = Vector3.Distance (transform.localPosition, player.transform.localPosition);

				if(distance < minDistance)
				{
					playerToUse = player;
					minDistance = distance;
				}
			}
		}		

		if(playerToUse != null)
		{
			Vector3 newPosition = transform.localPosition;
			newPosition.x = Mathf.Lerp (newPosition.x, playerToUse.transform.localPosition.x, m_LateralSpeed * Time.deltaTime);
			transform.localPosition = newPosition;
		}
	}
}
