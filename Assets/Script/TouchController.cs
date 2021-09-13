using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	[SerializeField] private Unit myUnit;

	private bool IsLeftClick => Input.GetMouseButtonDown(0);
	private bool IsRightClick => Input.GetMouseButtonDown(1);

	public void OnPointerDown(PointerEventData eventData)
	{
		if(GameManager.Instance.IsGameOver) return;
		
		if (IsLeftClick)
			myUnit.Open();
		else if (IsRightClick)
			myUnit.ToggleFlag();
		
		
	}

	public void OnPointerUp(PointerEventData eventData)
	{
	}
}