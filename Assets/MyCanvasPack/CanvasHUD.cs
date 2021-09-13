using System.Collections;
using TMPro;
using UnityEngine;

namespace MyCanvasPack
{
	public class CanvasHUD : CanvasController
	{
		public override CanvasType MyCanvasType() => CanvasType.HUD;

		[SerializeField] private TextMeshProUGUI textTime;


		public override void Open()
		{
			base.Open();

			StartCoroutine(StartTimer());
		}

		public override void Close()
		{
			base.Close();
			StopAllCoroutines();
		}

		IEnumerator StartTimer()
		{
			GameManager.Instance.Score = 0;
			while (true)
			{
				GameManager.Instance.Score += Time.deltaTime;
				yield return null;
				textTime.text = ((int) GameManager.Instance.Score).ToString();
			}
			// ReSharper disable once IteratorNeverReturns
		}
	}
}