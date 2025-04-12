using Newtonsoft.Json;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using App.Scripts.Scenes.Gameplay.Features.Map.Items;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Configs;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scenes.Gameplay.Features.LevelCreation.Editor
{
	public class LevelCreationEditorWindow : OdinEditorWindow
	{
		#region Create/Size
		[HorizontalGroup("Create/Size")]
		[SerializeField] private int height = 10;

		[HorizontalGroup("Create/Size")]
		[SerializeField] private int width = 10;

		[Space(8)]
		[SerializeField] private TilesDatabase _tilesDatabase;

		[VerticalGroup("Create")]
		[ShowIf("@_tilesDatabase != null")]
		[Button(ButtonSizes.Small)]
		public void CreateMatrix()
		{
			blocksMatrix = new string[width, height];
		}
		#endregion

		#region Matrix

		[FoldoutGroup("Chunk")]
		[ValueDropdown(nameof(GetIds))]
		[SerializeField] private string value = string.Empty;

		[FoldoutGroup("Chunk")]
		[ShowIf("@blocksMatrix != null")]
		[TableMatrix(DrawElementMethod = nameof(DrawCustomElement))]
		[SerializeField] private string[,] blocksMatrix;
		
		private string DrawCustomElement(Rect rect, string value)
		{
			if (Event.current.button == 0 && (Event.current.type == EventType.MouseDrag || Event.current.type == EventType.MouseDown)
				&& rect.Contains(Event.current.mousePosition))
			{
				value = this.value;
				GUI.changed = true;
				Event.current.Use();
			}


			if (Event.current.button == 1 && (Event.current.type == EventType.MouseDrag || Event.current.type == EventType.MouseDown)
				&& rect.Contains(Event.current.mousePosition))
			{
				value = string.Empty;
				GUI.changed = true;
				Event.current.Use();
			}

			if (!string.IsNullOrEmpty(value) && _tilesDatabase.Configs.TryGetValue(value, out var config))
			{
				GUI.DrawTexture(rect, config.TileSprite.texture);
			}

			return value;
		}

		public IEnumerable<string> GetIds()
		{
			if (_tilesDatabase == null)
			{
				return null;
			}
			return _tilesDatabase.Configs.Keys.ToList();
		}
		#endregion

		#region SaveLoad
		[Space(8)]
		[SerializeField] private ChunkFilling _chunkFilling;
		[HorizontalGroup("SaveLoad")]
		[Button(ButtonSizes.Small)]
		public void Save()
		{
			_chunkFilling.Tiles.Clear();
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					string element = blocksMatrix[i, j];
					if (!string.IsNullOrEmpty(element))
					{
						_chunkFilling.Tiles.Add(new ()
						{
							Position = new Vector2Int(height -1-i, width-1-j),
							TileId = element
						});
					}
				}
			}
		}

		[HorizontalGroup("SaveLoad")]
		[ShowIf("@_tilesDatabase != null")]
		[Button(ButtonSizes.Small)]
		public void Load()
		{
		}
		#endregion

		[MenuItem("My Tools/Level Creation")]
		private static void OpenWindow()
		{
			GetWindow<LevelCreationEditorWindow>().Show();
		}
	}
}