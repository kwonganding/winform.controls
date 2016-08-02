namespace TX.Framework.WindowUI.Forms
{
    partial class FormListEntity
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
            this.panelPager = new System.Windows.Forms.Panel();
            this.panelSearchCondition = new System.Windows.Forms.Panel();
            this.panelToolBar = new System.Windows.Forms.Panel();
            this.panelWorkArea = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelPager
            // 
            this.panelPager.BackColor = System.Drawing.Color.Transparent;
            this.panelPager.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelPager.Location = new System.Drawing.Point(6, 395);
            this.panelPager.Name = "panelPager";
            this.panelPager.Size = new System.Drawing.Size(768, 26);
            this.panelPager.TabIndex = 0;
            // 
            // panelSearchCondition
            // 
            this.panelSearchCondition.BackColor = System.Drawing.Color.Transparent;
            this.panelSearchCondition.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelSearchCondition.Location = new System.Drawing.Point(6, 365);
            this.panelSearchCondition.Name = "panelSearchCondition";
            this.panelSearchCondition.Size = new System.Drawing.Size(768, 30);
            this.panelSearchCondition.TabIndex = 1;
            // 
            // panelToolBar
            // 
            this.panelToolBar.BackColor = System.Drawing.Color.Transparent;
            this.panelToolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelToolBar.Location = new System.Drawing.Point(6, 30);
            this.panelToolBar.Name = "panelToolBar";
            this.panelToolBar.Size = new System.Drawing.Size(768, 28);
            this.panelToolBar.TabIndex = 2;
            // 
            // panelWorkArea
            // 
            this.panelWorkArea.BackColor = System.Drawing.Color.Transparent;
            this.panelWorkArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelWorkArea.Location = new System.Drawing.Point(6, 58);
            this.panelWorkArea.Name = "panelWorkArea";
            this.panelWorkArea.Size = new System.Drawing.Size(768, 307);
            this.panelWorkArea.TabIndex = 3;
            // 
            // FormListEntity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 427);
            this.Controls.Add(this.panelWorkArea);
            this.Controls.Add(this.panelToolBar);
            this.Controls.Add(this.panelSearchCondition);
            this.Controls.Add(this.panelPager);
            this.CornerRadius = 0;
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "FormListEntity";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "FormListEntity";
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel panelPager;
        protected System.Windows.Forms.Panel panelSearchCondition;
        protected System.Windows.Forms.Panel panelToolBar;
        protected System.Windows.Forms.Panel panelWorkArea;

    }
}