/* Generated by MyraPad at 11/15/2020 11:30:34 AM */
using Myra;
using Myra.Graphics2D;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;
using Myra.Graphics2D.Brushes;

#if !STRIDE
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#else
using Stride.Core.Mathematics;
#endif

namespace TroublesOfJord.MapEditor.UI
{
	partial class ChangeSizeDialog: Dialog
	{
		private void BuildUI()
		{
			var label1 = new Label();
			label1.Text = "Current Size:";

			_labelCurrentSize = new Label();
			_labelCurrentSize.Text = "64x64";
			_labelCurrentSize.GridColumn = 1;
			_labelCurrentSize.Id = "_labelCurrentSize";

			var label2 = new Label();
			label2.Text = "Action:";
			label2.GridRow = 1;

			var listItem1 = new ListItem();
			listItem1.Text = "Expand";

			var listItem2 = new ListItem();
			listItem2.Text = "Cut";

			_comboAction = new ComboBox();
			_comboAction.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;
			_comboAction.GridColumn = 1;
			_comboAction.GridRow = 1;
			_comboAction.Id = "_comboAction";
			_comboAction.Items.Add(listItem1);
			_comboAction.Items.Add(listItem2);

			var label3 = new Label();
			label3.Text = "Direction:";
			label3.GridRow = 2;

			var listItem3 = new ListItem();
			listItem3.Text = "Left";

			var listItem4 = new ListItem();
			listItem4.Text = "Top";

			var listItem5 = new ListItem();
			listItem5.Text = "Right";

			var listItem6 = new ListItem();
			listItem6.Text = "Bottom";

			_comboDirection = new ComboBox();
			_comboDirection.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;
			_comboDirection.GridColumn = 1;
			_comboDirection.GridRow = 2;
			_comboDirection.Id = "_comboDirection";
			_comboDirection.Items.Add(listItem3);
			_comboDirection.Items.Add(listItem4);
			_comboDirection.Items.Add(listItem5);
			_comboDirection.Items.Add(listItem6);

			var label4 = new Label();
			label4.Text = "Amount:";
			label4.GridRow = 3;

			_numericAmount = new SpinButton();
			_numericAmount.Minimum = 1;
			_numericAmount.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;
			_numericAmount.Value = 4;
			_numericAmount.Integer = true;
			_numericAmount.GridColumn = 1;
			_numericAmount.GridRow = 3;
			_numericAmount.Id = "_numericAmount";

			_labelFill = new Label();
			_labelFill.Text = "Fill:";
			_labelFill.GridRow = 4;
			_labelFill.Id = "_labelFill";

			_comboFill = new ComboBox();
			_comboFill.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;
			_comboFill.GridColumn = 1;
			_comboFill.GridRow = 4;
			_comboFill.Id = "_comboFill";

			var grid1 = new Grid();
			grid1.ColumnSpacing = 8;
			grid1.RowSpacing = 8;
			grid1.DefaultRowProportion = new Proportion
			{
				Type = Myra.Graphics2D.UI.ProportionType.Auto,
			};
			grid1.ColumnsProportions.Add(new Proportion
			{
				Type = Myra.Graphics2D.UI.ProportionType.Auto,
			});
			grid1.ColumnsProportions.Add(new Proportion
			{
				Type = Myra.Graphics2D.UI.ProportionType.Fill,
			});
			grid1.Widgets.Add(label1);
			grid1.Widgets.Add(_labelCurrentSize);
			grid1.Widgets.Add(label2);
			grid1.Widgets.Add(_comboAction);
			grid1.Widgets.Add(label3);
			grid1.Widgets.Add(_comboDirection);
			grid1.Widgets.Add(label4);
			grid1.Widgets.Add(_numericAmount);
			grid1.Widgets.Add(_labelFill);
			grid1.Widgets.Add(_comboFill);

			
			Title = "Change Size";
			Left = 547;
			Top = 160;
			Width = 250;
			Height = 220;
			Content = grid1;
		}

		
		public Label _labelCurrentSize;
		public ComboBox _comboAction;
		public ComboBox _comboDirection;
		public SpinButton _numericAmount;
		public Label _labelFill;
		public ComboBox _comboFill;
	}
}