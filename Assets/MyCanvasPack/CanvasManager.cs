using System.Collections.Generic;
using UnityEngine;

namespace MyCanvasPack
{
	public enum CanvasType
	{
		Home, HUD,
		GameOver
	}

	public class CanvasManager : Singleton<CanvasManager>
	{
		[SerializeField] private List<CanvasController> listCanvas;
		[SerializeField] private CanvasType startCanvas;
		
		private CanvasController _current;
		private readonly Stack<CanvasController> _history = new Stack<CanvasController>();

		private void Start()
		{
			Open(startCanvas);
		}

		public void Open(CanvasType canvasType)
		{
			if (_current)
			{
				_current.Close();
				_history.Push(_current);
			}

			var canvas = listCanvas.Find(x => x.MyCanvasType() == canvasType);
			if (!canvas) return;
			_current = canvas;
			_current.Open();
		}

		public void Back()
		{
			if (_history.Count == 0) 
				return;

			_current.Close();

			var canvas = _history.Pop();
			_current = canvas;
			_current.Open();
		}
	}
}