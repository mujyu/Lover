// UTAGE: Unity Text Adventure Game Engine (c) Ryohei Tokimurausing UnityEngine;

namespace Utage
{

	/// <summary>
	/// コマンド：シナリオラベル
	/// </summary>
	internal class AdvCommandScenarioLabel : AdvCommand
	{
		public AdvCommandScenarioLabel(StringGridRow row)
			: base(row)
		{
			this.ScenarioLabel = ParseScenarioLabel(AdvColumnName.Command);
			this.Type = ParseCellOptional<ScenarioLabelType>(AdvColumnName.Arg1, ScenarioLabelType.None);
		}


		public override void DoCommand(AdvEngine engine)
		{
		}

		public enum ScenarioLabelType
		{
			None,
			SavePoint,
		};
		public string ScenarioLabel { get; protected set; }

		public ScenarioLabelType Type { get; protected set; }
		public string Title
		{
			get
			{
				string title = ParseCellOptional<string>(AdvColumnName.Arg2, "");
				if (string.IsNullOrEmpty(title)) return "";

				//現在の設定言語にローカライズされたテキストを取得
				string columnName = AdvColumnName.Arg2.QuickToString();
				if (LanguageManager.Instance != null)
				{
					if (this.RowData.Grid.ContainsColumn(LanguageManager.Instance.CurrentLanguage))
					{
						columnName = LanguageManager.Instance.CurrentLanguage;
					}
				}
				if (this.RowData.IsEmptyCell(columnName))
				{   //指定の言語が空なら、デフォルトのArg2列を
					return this.RowData.ParseCellOptional<string>(AdvColumnName.Arg2.QuickToString(), "");
				}
				else
				{   //指定の言語を
					return this.RowData.ParseCellOptional<string>(columnName, "");
				}
			}
		}
	}
}