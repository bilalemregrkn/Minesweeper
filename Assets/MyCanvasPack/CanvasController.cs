using UnityEngine;

namespace MyCanvasPack
{
	[RequireComponent(typeof(Canvas))]
	public abstract class CanvasController : MonoBehaviour
	{
		public virtual CanvasType MyCanvasType() => CanvasType.Home;
		private Canvas _canvas;
		
		private void Awake()
		{
			_canvas = GetComponent<Canvas>();
		}

		public virtual void Open()
		{
			_canvas.enabled = true;
		}

		public virtual void Close()
		{
			_canvas.enabled = false;
		}
	}
}