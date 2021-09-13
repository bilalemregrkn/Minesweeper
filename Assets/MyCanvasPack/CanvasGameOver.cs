using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyCanvasPack
{
	public class CanvasGameOver : CanvasController
	{
		public override CanvasType MyCanvasType() => CanvasType.GameOver;
		[SerializeField] private TextMeshProUGUI textTime;

		public override void Open()
		{
			base.Open();
			textTime.text = ((int)GameManager.Instance.Score).ToString();
		}

		public void OnClick_TryAgain()
		{
			SceneManager.LoadScene(0);
		}
	}
}