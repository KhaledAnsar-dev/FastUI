namespace FastUI.FastUILibrary.Core.Components
{
    partial class FuiTextBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            myPanel = new FastUI.Modules.Panels.FuiPanel();
            innerControl = new TextBox();
            myPanel.SuspendLayout();
            SuspendLayout();
            // 
            // myPanel
            // 
            myPanel.BackColor = Color.Transparent;
            myPanel.BorderColor = Color.Black;
            myPanel.BorderWidth = 1F;
            myPanel.ControlHeight = 100;
            myPanel.Controls.Add(innerControl);
            myPanel.ControlWidth = 200;
            myPanel.CornerRadius = 10F;
            myPanel.Dock = DockStyle.Fill;
            myPanel.FillColor = Color.White;
            myPanel.FocusedBorderColor = Color.Black;
            myPanel.HoverBorderColor = Color.Gray;
            myPanel.HoverFillColor = Color.White;
            myPanel.Location = new Point(0, 0);
            myPanel.Name = "myPanel";
            myPanel.Size = new Size(200, 100);
            myPanel.TabIndex = 0;
            // 
            // innerControl
            // 
            innerControl.BorderStyle = BorderStyle.None;
            innerControl.Location = new Point(13, 12);
            innerControl.Multiline = true;
            innerControl.Name = "innerControl";
            innerControl.Size = new Size(123, 30);
            innerControl.TabIndex = 1;

            // 
            // FuiTextBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(myPanel);
            Name = "FuiTextBox";
            Size = new Size(200, 100);
            myPanel.ResumeLayout(false);
            myPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private FastUI.Modules.Panels.FuiPanel myPanel;
        private TextBox innerControl;
    }
}
