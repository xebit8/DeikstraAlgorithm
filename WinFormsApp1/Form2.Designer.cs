namespace DeikstraAlgorithm
{
    partial class Form2
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
            this.NameTB = new System.Windows.Forms.TextBox();
            this.CloseButton = new System.Windows.Forms.Button();
            this.NameTBLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // NameTB
            // 
            this.NameTB.Location = new System.Drawing.Point(56, 50);
            this.NameTB.Name = "NameTB";
            this.NameTB.Size = new System.Drawing.Size(244, 35);
            this.NameTB.TabIndex = 0;
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(375, 48);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(131, 40);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.Text = "OK";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // NameTBLabel
            // 
            this.NameTBLabel.AutoSize = true;
            this.NameTBLabel.Location = new System.Drawing.Point(21, 9);
            this.NameTBLabel.Name = "NameTBLabel";
            this.NameTBLabel.Size = new System.Drawing.Size(313, 30);
            this.NameTBLabel.TabIndex = 2;
            this.NameTBLabel.Text = "Имя вершины (необязательно)";
            // 
            // NodeNameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 131);
            this.Controls.Add(this.NameTBLabel);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.NameTB);
            this.ActiveControl = this.CloseButton;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NodeNameForm";
            this.Text = "Выберите имя для вершины";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox NameTB;
        private Button CloseButton;
        private Label NameTBLabel;
    }
}