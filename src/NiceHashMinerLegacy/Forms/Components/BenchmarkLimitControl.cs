﻿using System;
using System.Windows.Forms;
using NiceHashMinerLegacy.Common.Enums;

namespace NiceHashMiner.Forms.Components
{
    public partial class BenchmarkLimitControl : UserControl
    {
        public string GroupName
        {
            get => groupBox1.Text;
            set
            {
                if (value != null) groupBox1.Text = value;
            }
        }

        // int array reference property
        private int[] _timeLimits;

        public int[] TimeLimits
        {
            get => _timeLimits;
            set
            {
                if (value != null)
                {
                    _timeLimits = value;
                    for (var indexKey = 0; indexKey < _timeLimits.Length; ++indexKey)
                    {
                        _textBoxes[indexKey].Text = _timeLimits[indexKey].ToString();
                    }
                }
            }
        }

        // texbox references
        private readonly TextBox[] _textBoxes;

        public BenchmarkLimitControl()
        {
            InitializeComponent();
            textBoxQuick.KeyPress += TextBoxKeyPressEvents.TextBoxIntsOnly_KeyPress;
            textBoxStandard.KeyPress += TextBoxKeyPressEvents.TextBoxIntsOnly_KeyPress;
            textBoxPrecise.KeyPress += TextBoxKeyPressEvents.TextBoxIntsOnly_KeyPress;
            _textBoxes = new[] {textBoxQuick, textBoxStandard, textBoxPrecise};
            //InitLocale();
        }

        public void SetToolTip(ref ToolTip toolTip, string groupTypeName)
        {
            // TODO old implementation has textBox tooltips that don't work
            toolTip.SetToolTip(labelQuick,
                string.Format(Translations.Tr("Amount of time (in seconds) for {0} benchmarking the {1}"),
                    Translations.Tr("Quick"), groupTypeName) + ".");
            toolTip.SetToolTip(labelStandard,
                string.Format(Translations.Tr("Amount of time (in seconds) for {0} benchmarking the {1}"),
                    Translations.Tr("Standard"), groupTypeName) + ".");
            toolTip.SetToolTip(labelPrecise,
                string.Format(Translations.Tr("Amount of time (in seconds) for {0} benchmarking the {1}"),
                    Translations.Tr("Standard"), groupTypeName) + ".");
        }

        public void InitLocale()
        {
            labelQuick.Text = Translations.Tr("Quick") + ":";
            labelStandard.Text = Translations.Tr("Standard") + ":";
            labelPrecise.Text = Translations.Tr("Precise") + ":";
        }

        // TODO replace  TextChanged Events with TextBox exit events

        #region Events

        private void TextBoxQuick_TextChanged(object sender, EventArgs e)
        {
            SetTimeLimit(BenchmarkPerformanceType.Quick, textBoxQuick.Text);
        }

        private void TextBoxStandard_TextChanged(object sender, EventArgs e)
        {
            SetTimeLimit(BenchmarkPerformanceType.Standard, textBoxStandard.Text);
        }

        private void TextBoxPrecise_TextChanged(object sender, EventArgs e)
        {
            SetTimeLimit(BenchmarkPerformanceType.Precise, textBoxPrecise.Text);
        }

        #endregion // Events

        private void SetTimeLimit(BenchmarkPerformanceType type, string numString)
        {
            if (_timeLimits == null) return;
            if (int.TryParse(numString, out var value))
            {
                _timeLimits[(int) type] = value;
            }
        }
    }
}
