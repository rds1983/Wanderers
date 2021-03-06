/* Generated by MyraPad at 08.11.2019 0:00:17 */
using Myra.Graphics2D.UI;

#if !XENKO
using Microsoft.Xna.Framework;
#else
using Xenko.Core.Mathematics;
#endif

namespace Jord.UI
{
	partial class MainMenu: Grid
	{
		private void BuildUI()
		{
			var label1 = new Label();
			label1.Text = "Troubles of Jord";
			label1.TextColor = new Color
			{
				B = 64,
				G = 128,
				R = 255,
				A = 255,
			};
			label1.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Center;

			_playItem = new MenuItem();
			_playItem.Id = "_playItem";
			_playItem.Text = "Play";

			_quitMenuItem = new MenuItem();
			_quitMenuItem.Id = "_quitMenuItem";
			_quitMenuItem.Text = "Quit";

			_mainMenu = new VerticalMenu();
			_mainMenu.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Center;
			_mainMenu.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Center;
			_mainMenu.Id = "_mainMenu";
			_mainMenu.Width = 200;
			_mainMenu.Items.Add(_playItem);
			_mainMenu.Items.Add(_quitMenuItem);

			_textVersion = new Label();
			_textVersion.Text = "Version 1.0";
			_textVersion.TextColor = Color.Lime;
			_textVersion.Id = "_textVersion";
			_textVersion.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Right;
			_textVersion.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Bottom;

			
			Id = "Root";
			Widgets.Add(label1);
			Widgets.Add(_mainMenu);
			Widgets.Add(_textVersion);
		}

		
		public MenuItem _playItem;
		public MenuItem _quitMenuItem;
		public VerticalMenu _mainMenu;
		public Label _textVersion;
	}
}