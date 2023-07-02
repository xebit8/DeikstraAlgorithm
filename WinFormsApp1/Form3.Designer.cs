namespace WinFormsApp1
{
    partial class Form3
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
            this.ValueTB = new System.Windows.Forms.TextBox();
            this.CloseButton = new System.Windows.Forms.Button();
            this.ValueTBLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ValueTB
            // 
            this.ValueTB.Location = new System.Drawing.Point(51, 36);
            this.ValueTB.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ValueTB.Name = "ValueTB";
            this.ValueTB.Text = "1";
            this.ValueTB.Size = new System.Drawing.Size(70, 23);
            this.ValueTB.TabIndex = 0;
            this.ValueTB.TextChanged += new System.EventHandler(this.ValueTV_TextChanged);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(189, 36);
            this.CloseButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(88, 23);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.Text = "OK";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // ValueTBLabel
            // 
            this.ValueTBLabel.AutoSize = true;
            this.ValueTBLabel.Location = new System.Drawing.Point(11, 9);
            this.ValueTBLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ValueTBLabel.Name = "ValueTBLabel";
            this.ValueTBLabel.Size = new System.Drawing.Size(159, 15);
            this.ValueTBLabel.TabIndex = 2;
            this.ValueTBLabel.Text = "Вес (Нагруженность) ребра";
            // 
            // EdgeValueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 87);
            this.Controls.Add(this.ValueTBLabel);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.ValueTB);
            this.ActiveControl = this.CloseButton;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EdgeValueForm";
            this.Text = "Выберите вес для дуги";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox ValueTB;
        private Button CloseButton;
        private Label ValueTBLabel;
    }
}