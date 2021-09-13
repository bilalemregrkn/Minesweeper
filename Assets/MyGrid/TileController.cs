 using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyGrid
{
	public class TileController : MonoBehaviour
	{
		public TileController up;
		public TileController down;
		public TileController right;
		public TileController left;

		public Vector2 coordinate;

		public Unit myUnit;

		public TileController GetNeighbour(Direction direction)
		{
			switch (direction)
			{
				case Direction.Up:
					return up;
				case Direction.Down:
					return down;
				case Direction.Left:
					return left;
				case Direction.Right:
					return right;
				case Direction.UpRight:
					return up != null ? up.right : null;
				case Direction.UpLeft:
					return up != null ? up.left : null;
				case Direction.DownRight:
					return down != null ? down.right : null;
				case Direction.DownLeft:
					return down != null ? down.left : null;
				default:
					return null;
			}
		}

		public List<TileController> GetAllNeighbour()
		{
			int direction = Enum.GetNames(typeof(Direction)).Length;
			var list = new List<TileController>();
			for (int i = 0; i < direction; i++)
			{
				var tile = GetNeighbour((Direction) i);
				

				if (tile != null)
				{
					if(!tile.gameObject.activeInHierarchy) continue;
					list.Add(tile);
				}
					
			}

			return list;
		}
	}
}