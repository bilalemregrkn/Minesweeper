using App.Helpers;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
	private Image _image;
	private ToggleButtonManager _toggleButtonManager;
	private int _index;

	private void Awake()
	{
		_image = GetComponent<Image>();
		_toggleButtonManager = transform.parent.GetComponent<ToggleButtonManager>();
		_index = transform.GetSiblingIndex();
	}

	public void OnClick_Button()
	{
		_toggleButtonManager.Selected(_index);
	}
	
	public void UpdateDisplay(bool active)
	{
		_image.color = !active ? Color.white.With(a:.5f) : Color.white;
	}
}