using UnityEngine;

namespace MyCanvasPack
{
	public class CanvasHome : CanvasController
	{
		public override CanvasType MyCanvasType() => CanvasType.Home;

		[SerializeField] private ToggleButtonManager size;
		[SerializeField] private ToggleButtonManager difficulty;
		
		public void OnClick_PlayButton()
		{
			GameManager.Instance.StartGame(size.CurrentIndex,difficulty.CurrentIndex);
		}
		
		
	}
}