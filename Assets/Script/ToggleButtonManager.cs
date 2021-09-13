using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ToggleButtonManager : MonoBehaviour
{
	private List<ToggleButton> _listToggleButton;
	
	public int CurrentIndex { get; set; }

	private void Start()
	{
		_listToggleButton = GetComponentsInChildren<ToggleButton>().ToList();
		
		_listToggleButton[1].OnClick_Button();
	}

	public void Selected(int index)
	{
		CurrentIndex = index;
		for (int i = 0; i < _listToggleButton.Count; i++)
		{
			bool active = i == index;
			_listToggleButton[i].UpdateDisplay(active);
		}
	}
}