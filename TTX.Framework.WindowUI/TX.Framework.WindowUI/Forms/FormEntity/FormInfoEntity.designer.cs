using TX.Framework.WindowUI.Controls;
namespace TX.Framework.WindowUI.Forms
{
    partial class FormInfoEntity
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelWorkArea = new TX.Framework.WindowUI.Controls.TXPanel();
            this.panelControlArea = new System.Windows.Forms.Panel();
            this.btnCancel = new TX.Framework.WindowUI.Controls.TXButton();
            this.btnOK = new TX.Framework.WindowUI.Controls.TXButton();
            this.panelControlArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelWorkArea
            // 
            this.panelWorkArea.BackColor = System.Drawing.Color.Transparent;
            this.panelWorkArea.BorderColor = System.Drawing.Color.LightGray;
            this.panelWorkArea.CornerRadius = 5;
            this.panelWorkArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelWorkArea.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panelWorkArea.Location = new System.Drawing.Point(6, 30);
            this.panelWorkArea.MinimumSize = new System.Drawing.Size(27, 27);
            this.panelWorkArea.Name = "panelWorkArea";
            this.panelWorkArea.Size = new System.Drawing.Size(592, 311);
            this.panelWorkArea.TabIndex = 0;
            // 
            // panelControlArea
            // 
            this.panelControlArea.BackColor = System.Drawing.Color.Transparent;
            this.panelControlArea.Controls.Add(this.btnCancel);
            this.panelControlArea.Controls.Add(this.btnOK);
            this.panelControlArea.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControlArea.Location = new System.Drawing.Point(6, 341);
            this.panelControlArea.Margin = new System.Windows.Forms.Padding(0);
            this.panelControlArea.Name = "panelControlArea";
            this.panelControlArea.Size = new System.Drawing.Size(592, 30);
            this.panelControlArea.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = null;
            this.btnCancel.Location = new System.Drawing.Point(256, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取 消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.OnBtnCancelClick);
            // 
            // btnOK
            // 
            this.btnOK.Image = null;
            this.btnOK.Location = new System.Drawing.Point(159, 1);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 25);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确 定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.OnBtnOkClick);
            // 
            // FormInfoEntity
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(604, 374);
            this.Controls.Add(this.panelWorkArea);
            this.Controls.Add(this.panelControlArea);
            this.CornerRadius = 1;
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "FormInfoEntity";
            this.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.Text = "FormEntity";
            this.panelControlArea.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected TXButton btnCancel;
        protected TXButton btnOK;
        protected TXPanel panelWorkArea;
        protected System.Windows.Forms.Panel panelControlArea;
    }
}