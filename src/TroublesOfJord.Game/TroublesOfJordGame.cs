﻿using System.IO;
using TroublesOfJord.Compiling;
using TroublesOfJord.Storage;
using TroublesOfJord.UI;
using TroublesOfJord.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra;
using Myra.Graphics2D.UI;
using TroublesOfJord.Core;
using XNAssets;

namespace TroublesOfJord
{
	public class TroublesOfJordGame : Game
	{
		private const string DataPath = "data";
		private readonly GraphicsDeviceManager _graphics;

		public static TroublesOfJordGame Instance { get; private set; }

		public int? StartGameIndex;

		public TroublesOfJordGame()
		{
			Instance = this;

			_graphics = new GraphicsDeviceManager(this)
			{
				PreferredBackBufferWidth = 1440,
				PreferredBackBufferHeight = 900
			};

			IsMouseVisible = true;
			Window.AllowUserResizing = true;
			Window.Title = "Troubles of Jord";

			TJ.GameLogHandler = message =>
			{
				var asGameView = Desktop.Widgets[0] as GameView;
				if (asGameView == null)
				{
					return;
				}

				asGameView.LogView.Log(message);
			};
		}

		protected override void LoadContent()
		{
			base.LoadContent();

			MyraEnvironment.Game = this;

/*			MyraEnvironment.DisableClipping = true;
			MyraEnvironment.DrawFocusedWidgetFrame = true;
			MyraEnvironment.DrawWidgetsFrames = true;*/

			CompilerParams.Verbose = true;

			var dataPath = Path.Combine(Files.ExecutableFolder, DataPath);
			TJ.AssetManager = new AssetManager(GraphicsDevice, new FileAssetResolver(dataPath));

			var compiler = new Compiler();
			TJ.Module = compiler.Process(Path.Combine(Files.ExecutableFolder, DataPath));

			if (StartGameIndex == null)
			{
				SwitchToMainMenu();
			} else
			{
				Play(StartGameIndex.Value);
			}
		}

		private T SwitchTo<T>() where T : Widget, new()
		{
			Desktop.Widgets.Clear();
			var widget = new T();
			Desktop.Widgets.Add(widget);

			return widget;
		}

		public void SwitchToMainMenu()
		{
			SwitchTo<MainMenu>();
		}

		public void Play(int slotIndex)
		{
			if (TJ.Player != null)
			{
				TJ.Player.Stats.Life.Changed -= Life_Changed;
			}

			TJ.Session = new GameSession(slotIndex);

			TJ.Player.Stats.Life.Changed += Life_Changed;

			SwitchTo<GameView>();

			var gameView = (GameView)Desktop.Widgets[0];
			Desktop.FocusedKeyboardWidget = gameView;

			UpdateStats();
		}

		private void UpdateStats()
		{
			var gameView = (GameView)Desktop.Widgets[0];

			var life = TJ.Player.Stats.Life;
			gameView._labelHp.Text = string.Format("H: {0}/{1}", life.CurrentHP, life.MaximumHP);
			gameView._labelMana.Text = string.Format("M: {0}/{1}", life.CurrentMana, life.MaximumMana);
			gameView._labelStamina.Text = string.Format("S: {0}/{1}", life.CurrentStamina, life.MaximumStamina);
		}

		private void Life_Changed(object sender, System.EventArgs e)
		{
			UpdateStats();
		}

		protected override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (TJ.Session != null)
			{
				TJ.Session.OnTimer();
			}
		}

		protected override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			if (_graphics.PreferredBackBufferWidth != Window.ClientBounds.Width ||
				_graphics.PreferredBackBufferHeight != Window.ClientBounds.Height)
			{
				_graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
				_graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
				_graphics.ApplyChanges();
			}

			GraphicsDevice.Clear(Color.Black);

			Desktop.Render();
		}

		protected override void EndRun()
		{
			base.EndRun();

			// Save current game
			if (TJ.Session != null)
			{
				TJ.Session.Slot.CharacterData = new CharacterData(TJ.Session.Character);
				TJ.Session.Slot.Save();
			}
		}
	}
}