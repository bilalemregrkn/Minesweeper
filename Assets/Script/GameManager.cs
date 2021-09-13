using System;
using System.Collections;
using System.Collections.Generic;
using App.Helpers;
using MyCanvasPack;
using MyGrid;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }
	public float Score { get; set; }
	public bool IsGameOver => _isGameOver;


	[SerializeField] private GameObject parentGrid;


	private readonly List<Unit> _listUnit = new List<Unit>();
	private bool _isGameOver;

	private readonly Vector2[] _size = new[]
	{
		new Vector2(4, 4),
		new Vector2(4, 6),
		new Vector2(4, 8)
	};

	private readonly int[] _difficulty = {20, 40, 60};

	private void Awake()
	{
		Instance = this;
	}

	private void PrepareGame(Vector2 size, int mineAmount)
	{
		//Fill List
		foreach (var item in GridManager.Instance.ListGridController)
		{
			if (size.y < 8)
			{
				//en alt ve en üstü kapat
				if (item.coordinate.y == 0 || item.coordinate.y == 7)
					item.gameObject.SetActive(false);
			}

			if (size.y < 6)
			{
				//en alttan bir üst ve en üsstün bir alt kapat
				if (item.coordinate.y == 1 || item.coordinate.y == 6)
					item.gameObject.SetActive(false);
			}

			if (item.gameObject.activeInHierarchy)
			{
				var unit = item.GetComponent<Unit>();
				_listUnit.Add(unit);
			}
		}

		//Set Mine
		var list = new List<Unit>(_listUnit);
		for (int i = 0; i < mineAmount; i++)
		{
			var index = Random.Range(0, list.Count);
			var unit = list[index];
			unit.Prepare(UnitState.Mine);
			list.RemoveAt(index);
		}

		foreach (var item in _listUnit)
		{
			item.PrepareText();
		}
	}

	public void StartGame(int sizeIndex, int difficultyIndex)
	{
		CanvasManager.Instance.Open(CanvasType.HUD);

		var size = _size[sizeIndex];
		var percent = _difficulty[difficultyIndex];
		var amount = (int) (size.x * size.y).ToPercent(percent);
		PrepareGame(size, amount);
		parentGrid.SetActive(true);
	}

	public void GameOver()
	{
		if (_isGameOver) return;
		_isGameOver = true;

		AudioManager.Instance.Play(ClipType.Bomb);
		IEnumerator Do()
		{
			foreach (var item in _listUnit)
			{
				if (item.UnitState == UnitState.Mine)
				{
					item.Open();
					yield return new WaitForSeconds(.2f);
				}
			}

			CanvasManager.Instance.Open(CanvasType.GameOver);
		}

		StartCoroutine(Do());
	}

	public bool IsWin()
	{
		return false;
	}
}