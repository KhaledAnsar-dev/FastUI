namespace FastUI.FastUILibrary.Modules.Selection.FastComboBoxUI
{
    partial class FastComboBox
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
            label = new Label();
            comboBox = new Guna.UI2.WinForms.Guna2ComboBox();
            fakeFocus = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            SuspendLayout();
            // 
            // label
            // 
            label.AutoSize = true;
            label.BackColor = Color.FromArgb(242, 242, 242);
            label.Cursor = Cursors.Hand;
            label.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label.ForeColor = Color.FromArgb(0, 102, 102);
            label.Location = new Point(11, 6);
            label.Name = "label";
            label.Size = new Size(55, 21);
            label.TabIndex = 14;
            label.Text = "Select";
            label.Visible = false;
            label.Click += label_Click;
            // 
            // comboBox
            // 
            comboBox.BackColor = Color.Transparent;
            comboBox.BorderColor = Color.FromArgb(181, 188, 188);
            comboBox.BorderRadius = 16;
            comboBox.Cursor = Cursors.Hand;
            comboBox.CustomizableEdges = customizableEdges1;
            comboBox.Dock = DockStyle.Fill;
            comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.FillColor = Color.FromArgb(242, 242, 242);
            comboBox.FocusedColor = Color.FromArgb(0, 138, 128);
            comboBox.FocusedState.BorderColor = Color.FromArgb(0, 138, 128);
            comboBox.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            comboBox.ForeColor = Color.FromArgb(0, 102, 102);
            comboBox.HoverState.FillColor = Color.FromArgb(235, 235, 235);
            comboBox.ImeMode = ImeMode.Close;
            comboBox.IntegralHeight = false;
            comboBox.ItemHeight = 28;
            comboBox.Items.AddRange(new object[] { "amir", "anes", "bachir", "khaled", "razan" });
            comboBox.ItemsAppearance.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox.ItemsAppearance.ForeColor = Color.FromArgb(72, 72, 72);
            comboBox.ItemsAppearance.SelectedForeColor = Color.FromArgb(0, 102, 102);
            comboBox.Location = new Point(0, 0);
            comboBox.Name = "comboBox";
            comboBox.ShadowDecoration.CustomizableEdges = customizableEdges2;
            comboBox.Size = new Size(141, 34);
            comboBox.TabIndex = 13;
            comboBox.TextOffset = new Point(8, 0);
            comboBox.DropDown += comboBox_DropDown;
            comboBox.SelectedIndexChanged += comboBox_SelectedIndexChanged;
            comboBox.SizeChanged += comboBox_SizeChanged;
            comboBox.MouseEnter += comboBox_MouseEnter;
            comboBox.MouseLeave += comboBox_MouseLeave;
            // 
            // fakeFocus
            // 
            fakeFocus.CustomizableEdges = customizableEdges3;
            fakeFocus.Location = new Point(10, 10);
            fakeFocus.Name = "fakeFocus";
            fakeFocus.ShadowDecoration.CustomizableEdges = customizableEdges4;
            fakeFocus.Size = new Size(1, 1);
            fakeFocus.TabIndex = 15;
            // 
            // FastComboBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label);
            Controls.Add(comboBox);
            Controls.Add(fakeFocus);
            Name = "FastComboBox";
            Size = new Size(141, 34);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label;
        private Guna.UI2.WinForms.Guna2ComboBox comboBox;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel fakeFocus;
    }
}
