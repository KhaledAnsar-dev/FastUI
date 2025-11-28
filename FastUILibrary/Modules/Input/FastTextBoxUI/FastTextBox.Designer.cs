namespace FastUI.Modules.Input.FastTextBoxUI
{
    partial class FastTextBox
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            fakeFocus = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            textBox = new Guna.UI2.WinForms.Guna2TextBox();
            SuspendLayout();
            // 
            // fakeFocus
            // 
            fakeFocus.CustomizableEdges = customizableEdges1;
            fakeFocus.Location = new Point(10, 10);
            fakeFocus.Name = "fakeFocus";
            fakeFocus.ShadowDecoration.CustomizableEdges = customizableEdges2;
            fakeFocus.Size = new Size(1, 1);
            fakeFocus.TabIndex = 11;
            // 
            // textBox
            // 
            textBox.BackColor = Color.Transparent;
            textBox.BorderColor = Color.FromArgb(218, 221, 221);
            textBox.BorderRadius = 16;
            textBox.CustomizableEdges = customizableEdges3;
            textBox.DefaultText = "";
            textBox.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            textBox.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            textBox.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            textBox.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            textBox.FocusedState.BorderColor = Color.FromArgb(0, 138, 128);
            textBox.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox.ForeColor = Color.Black;
            textBox.HoverState.BorderColor = Color.FromArgb(181, 188, 188);
            textBox.HoverState.FillColor = Color.FromArgb(250, 250, 251);
            textBox.Location = new Point(0, 0);
            textBox.Margin = new Padding(3, 4, 3, 4);
            textBox.Name = "textBox";
            textBox.PlaceholderForeColor = Color.FromArgb(183, 183, 184);
            textBox.PlaceholderText = "Input";
            textBox.SelectedText = "";
            textBox.ShadowDecoration.CustomizableEdges = customizableEdges4;
            textBox.Size = new Size(141, 34);
            textBox.TabIndex = 12;
            textBox.TextOffset = new Point(5, 0);
            textBox.Click += textBox_Click;
            textBox.KeyPress += textBox_KeyPress;
            textBox.MouseLeave += textBox_MouseLeave;
            // 
            // FastTextBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(textBox);
            Controls.Add(fakeFocus);
            Name = "FastTextBox";
            Size = new Size(141, 34);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2CustomGradientPanel fakeFocus;
        private Guna.UI2.WinForms.Guna2TextBox textBox;
    }
}
