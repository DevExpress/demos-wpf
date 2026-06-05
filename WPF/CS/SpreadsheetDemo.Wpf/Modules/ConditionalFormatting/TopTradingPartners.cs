using System.Linq;
using System.Drawing;
using DevExpress.Spreadsheet;

namespace DevExpress.XtraSpreadsheet.Demos {
    public static class TopTradingPartners {
        const string defaultTableName = "Table";

        public static void ApplyTopImportsConditionalFormatting(Worksheet sheet) {
            CellRange importsColumnRange = GetTableColumnRange(sheet, "Imports");
            if(importsColumnRange == null)
                return;

            ConditionalFormattingCollection conditionalFormattings = sheet.ConditionalFormattings;
            
            RankConditionalFormatting cfRule2 = conditionalFormattings.AddRankConditionalFormatting(importsColumnRange, ConditionalFormattingRankCondition.TopByRank, 5);
            
            
            cfRule2.Formatting.Fill.BackgroundColor = Color.FromArgb(250, 191, 143);
        }
        public static void ApplyImportsYearlyChangeConditionalFormatting(Worksheet sheet) {
            CellRange imports1YChgColumnRange = GetTableColumnRange(sheet, "Imports 1Y Chg");
            if(imports1YChgColumnRange == null)
                return;

            ConditionalFormattingCollection conditionalFormattings = sheet.ConditionalFormattings;

            
            ExpressionConditionalFormatting cfRule =
            conditionalFormattings.AddExpressionConditionalFormatting(imports1YChgColumnRange, ConditionalFormattingExpressionCondition.GreaterThan, "0");
            
            
            cfRule.Formatting.Font.Color = Color.FromArgb(52, 150, 151);

            
            ExpressionConditionalFormatting cfRule2 =
            conditionalFormattings.AddExpressionConditionalFormatting(imports1YChgColumnRange, ConditionalFormattingExpressionCondition.LessThan, "0");
            
            
            cfRule2.Formatting.Font.Color = Color.FromArgb(227, 108, 9);
        }
        public static void ApplyTopExportsConditionalFormatting(Worksheet sheet) {
            CellRange exportsColumnRange = GetTableColumnRange(sheet, "Exports");
            if(exportsColumnRange == null)
                return;

            ConditionalFormattingCollection conditionalFormattings = sheet.ConditionalFormattings;

            
            RankConditionalFormatting cfRule1 = conditionalFormattings.AddRankConditionalFormatting(exportsColumnRange, ConditionalFormattingRankCondition.TopByRank, 5);
            
            
            cfRule1.Formatting.Fill.BackgroundColor = Color.FromArgb(141, 215, 217);
        }
        public static void ApplyExportsYearlyChangeConditionalFormatting(Worksheet sheet) {
            CellRange exports1YChgColumnRange = GetTableColumnRange(sheet, "Exports 1Y Chg");
            if(exports1YChgColumnRange == null)
                return;

            ConditionalFormattingCollection conditionalFormattings = sheet.ConditionalFormattings;

            
            ExpressionConditionalFormatting cfRule =
            conditionalFormattings.AddExpressionConditionalFormatting(exports1YChgColumnRange, ConditionalFormattingExpressionCondition.GreaterThan, "0");
            
            
            cfRule.Formatting.Font.Color = Color.FromArgb(52, 150, 151);

            
            ExpressionConditionalFormatting cfRule2 =
            conditionalFormattings.AddExpressionConditionalFormatting(exports1YChgColumnRange, ConditionalFormattingExpressionCondition.LessThan, "0");
            
            
            cfRule2.Formatting.Font.Color = Color.FromArgb(227, 108, 9);
        }


        public static void ApplyAsiaCountriesConditionalFormatting(Worksheet sheet) {
            CellRange countryColumnRange = GetTableColumnRange(sheet, "Country");
            if(countryColumnRange == null)
                return;

            ConditionalFormattingCollection conditionalFormattings = sheet.ConditionalFormattings;
            
            FormulaExpressionConditionalFormatting cfRule = conditionalFormattings.AddFormulaExpressionConditionalFormatting(countryColumnRange, "=$B6=\"Asia\"");
            
            
            cfRule.Formatting.Fill.BackgroundColor = Color.FromArgb(255, 94, 202, 199);
        }

        public static void ApplyBalanceChangeConditionalFormatting(Worksheet sheet) {
            CellRange balance1YChgColumnRange = GetTableColumnRange(sheet, "Balance 1Y Chg");
            if(balance1YChgColumnRange == null)
                return;

            ConditionalFormattingCollection conditionalFormattings = sheet.ConditionalFormattings;
            
            ConditionalFormattingValue lowBound1 = sheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.Auto);
            
            ConditionalFormattingValue highBound1 = sheet.ConditionalFormattings.CreateValue(ConditionalFormattingValueType.Auto);
            
            DataBarConditionalFormatting cfRule1 = conditionalFormattings.AddDataBarConditionalFormatting(balance1YChgColumnRange, lowBound1, highBound1, Color.FromArgb(87, 200, 197));
            cfRule1.GradientFill = false;
            
            cfRule1.NegativeBarColor = Color.FromArgb(247, 150, 70);
            
            cfRule1.AxisPosition = ConditionalFormattingDataBarAxisPosition.Middle;
            
            cfRule1.AxisColor = Color.Black;
        }
        public static void ApplyBalanceTrendConditionalFormatting(Worksheet sheet) {
            CellRange balanceColumnRange = GetTableColumnRange(sheet, "Balance");
            if(balanceColumnRange == null)
                return;

            ConditionalFormattingCollection conditionalFormattings = sheet.ConditionalFormattings;
            
            ConditionalFormattingIconSetValue minPoint = sheet.ConditionalFormattings.CreateIconSetValue(ConditionalFormattingValueType.Formula, "=MIN($F$6:$F$22)", ConditionalFormattingValueOperator.GreaterOrEqual);
            
            ConditionalFormattingIconSetValue midPoint = sheet.ConditionalFormattings.CreateIconSetValue(ConditionalFormattingValueType.Number, "0", ConditionalFormattingValueOperator.GreaterOrEqual);
            
            ConditionalFormattingIconSetValue maxPoint = sheet.ConditionalFormattings.CreateIconSetValue(ConditionalFormattingValueType.Number, "0.0001", ConditionalFormattingValueOperator.GreaterOrEqual);
            
            IconSetConditionalFormatting cfRule = conditionalFormattings.AddIconSetConditionalFormatting(balanceColumnRange, IconSetType.ArrowsGray3, new ConditionalFormattingIconSetValue[] { minPoint, midPoint, maxPoint });
        }
        static CellRange GetTableColumnRange(Worksheet sheet, string columnName) {
            Table table = sheet.Tables.FirstOrDefault(t => t.Name == defaultTableName);
            if(table == null)
                return null;
            TableColumn column = table.Columns.FirstOrDefault(c => c.Name == columnName);
            if(column == null)
                return null;
            return column.DataRange;
        }
    }
}
