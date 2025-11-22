namespace FastUI.Controls.Buttons
{
    partial class FastButton
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
            button = new Guna.UI2.WinForms.Guna2Button();
            SuspendLayout();
            // 
            // button
            // 
            button.BackColor = Color.Transparent;
            button.BorderRadius = 16;
            button.CustomizableEdges = customizableEdges1;
            button.DisabledState.BorderColor = Color.DarkGray;
            button.DisabledState.CustomBorderColor = Color.DarkGray;
            button.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            button.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            button.Dock = DockStyle.Fill;
            button.FillColor = Color.FromArgb(255, 188, 1);
            button.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button.ForeColor = Color.White;
            button.HoverState.FillColor = Color.FromArgb(255, 200, 40);
            button.Location = new Point(0, 0);
            button.Name = "button";
            button.ShadowDecoration.CustomizableEdges = customizableEdges2;
            button.Size = new Size(94, 34);
            button.TabIndex = 3;
            button.Text = "Valider";
            button.TextOffset = new Point(0, -1);
            // 
            // FastButton
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(button);
            Name = "FastButton";
            Size = new Size(94, 34);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button button;
    }
}
