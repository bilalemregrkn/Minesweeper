using System.Collections;
using App.Helpers;
using MyGrid;
using TMPro;
using UnityEngine;

public class Unit : MonoBehaviour
{
	public UnitState UnitState => _unitState;
	private UnitState _unitState;

	[SerializeField] private SpriteRenderer mySpriteRenderer;

	[SerializeField] private SpriteRenderer myMask;

	[SerializeField] private SpriteRenderer myFlag;
	[SerializeField] private SpriteRenderer myBomb;


	[SerializeField] private TextMeshPro myText;
	[SerializeField] private TileController myTile;

	private int _mineCount;
	private bool _open;

	public void Prepare(UnitState state)
	{
		_unitState = state;

		mySpriteRenderer.color = _unitState == UnitState.Mine ? Color.red : Color.white;
		myBomb.enabled = _unitState == UnitState.Mine;
	}

	public void PrepareText()
	{
		if (_unitState == UnitState.Mine)
		{
			myText.text = "";
		}
		else
		{
			var list = myTile.GetAllNeighbour();

			int count = 0;
			foreach (var item in list)
			{
				if (item.myUnit.UnitState == UnitState.Mine)
					count++;
			}

			var result = count > 0 ? count.ToString() : "";
			myText.text = result;
			_mineCount = count;
		}
	}

	public void Open()
	{
		if (!CanOpen()) return;

		_open = true;
		IEnumerator Do()
		{
			yield return ColorChange(myMask, myMask.color.With(a: 0), .2f);
			myMask.enabled = false;
		}

		StartCoroutine(Do());
		
		if (_unitState == UnitState.Mine)
		{
			GameManager.Instance.GameOver();
			return;
		}

		if (_mineCount == 0)
		{
			var list = myTile.GetAllNeighbour();
			foreach (var item in list)
			{
				item.myUnit.Open();
			}
		}

		if (GameManager.Instance.IsWin())
		{
		}
	}

	IEnumerator ColorChange(SpriteRenderer current, Color target, float time)
	{
		float passed = 0;
		var initColor = current.color;
		while (passed < time)
		{
			passed += Time.deltaTime;
			var normalized = passed / time;
			current.color = Color.Lerp(initColor, target, normalized);
			yield return null;
		}
	}

	public void ToggleFlag()
	{
		AudioManager.Instance.Play(ClipType.Flag);
		myFlag.enabled = !myFlag.enabled;
	}

	public bool CanOpen()
	{
		return !_open && !myFlag.enabled;
	}
}