using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;

namespace TX.Framework.WindowUI.Controls
{
    [ToolboxItem(false)]
    public class MultiselectComboBoxItem : TXCheckBox
    {
        private MultiselectComboBox _CheckBoxComboBox;
        private object _ComboBoxItem;

        public MultiselectComboBoxItem(MultiselectComboBox owner, object comboBoxItem)
            : base()
        {
            DoubleBuffered = true;
            _CheckBoxComboBox = owner;
            _ComboBoxItem = comboBoxItem;
            base.CornerRadius = 0;
            if (_CheckBoxComboBox.DataSource != null)
                AddBindings();
            else
                Text = comboBoxItem.ToString();
        }

        public object ComboBoxItem
        {
            get { return _ComboBoxItem; }
        }

        public void AddBindings()
        {
            DataBindings.Add("Text", _ComboBoxItem, _CheckBoxComboBox.DisplayMemberSingleItem);
            DataBindings.Add("Checked", _ComboBoxItem, _CheckBoxComboBox.ValueMember, false, DataSourceUpdateMode.OnPropertyChanged, false, null, null);
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            if (_CheckBoxComboBox.DataSource != null)
            {
                PropertyInfo PI = ComboBoxItem.GetType().GetProperty(_CheckBoxComboBox.ValueMember);
                PI.SetValue(ComboBoxItem, Checked, null);
            }
            base.OnCheckedChanged(e);
            if (_CheckBoxComboBox.DataSource != null)
            {
                string OldDisplayMember = _CheckBoxComboBox.DisplayMember;
                _CheckBoxComboBox.DisplayMember = null;
                _CheckBoxComboBox.DisplayMember = OldDisplayMember;
            }
        }

        internal void ApplyProperties(CheckBoxProperties properties)
        {
            this.Appearance = properties.Appearance;
            this.AutoCheck = properties.AutoCheck;
            this.AutoEllipsis = properties.AutoEllipsis;
            this.AutoSize = properties.AutoSize;
            this.CheckAlign = properties.CheckAlign;
            this.FlatAppearance.BorderColor = properties.FlatAppearanceBorderColor;
            this.FlatAppearance.BorderSize = properties.FlatAppearanceBorderSize;
            this.FlatAppearance.CheckedBackColor = properties.FlatAppearanceCheckedBackColor;
            this.FlatAppearance.MouseDownBackColor = properties.FlatAppearanceMouseDownBackColor;
            this.FlatAppearance.MouseOverBackColor = properties.FlatAppearanceMouseOverBackColor;
            this.FlatStyle = properties.FlatStyle;
            this.ForeColor = properties.ForeColor;
            this.RightToLeft = properties.RightToLeft;
            this.TextAlign = properties.TextAlign;
            this.ThreeState = properties.ThreeState;
        }

    }
}
