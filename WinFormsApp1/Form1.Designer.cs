namespace DeikstraAlgorithm
{
    partial class Form1
    {

        private System.ComponentModel.IContainer components = null;

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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AddButton = new System.Windows.Forms.Button();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.StartDAButton = new System.Windows.Forms.Button();
            this.Canvas = new System.Windows.Forms.PictureBox();
            this.ClearButton = new System.Windows.Forms.Button();
            this.MoveButton = new System.Windows.Forms.Button();
            this.ChangeThemeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).BeginInit();
            this.SuspendLayout();
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(100, 0);
            this.AddButton.Margin = new System.Windows.Forms.Padding(1);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(200, 50);
            this.AddButton.TabIndex = 0;
            this.AddButton.Text = "Добавить";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // RemoveButton
            // 
            this.RemoveButton.Location = new System.Drawing.Point(300, 0);
            this.RemoveButton.Margin = new System.Windows.Forms.Padding(1);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(200, 50);
            this.RemoveButton.TabIndex = 1;
            this.RemoveButton.Text = "Удалить";
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(500, 0);
            this.ConnectButton.Margin = new System.Windows.Forms.Padding(1);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(200, 50);
            this.ConnectButton.TabIndex = 2;
            this.ConnectButton.Text = "Соединить";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // StartDAButton
            // 
            this.StartDAButton.Location = new System.Drawing.Point(700, 0);
            this.StartDAButton.Margin = new System.Windows.Forms.Padding(1);
            this.StartDAButton.Name = "StartDAButton";
            this.StartDAButton.Size = new System.Drawing.Size(200, 50);
            this.StartDAButton.TabIndex = 3;
            this.StartDAButton.Text = "Начать";
            this.StartDAButton.UseVisualStyleBackColor = true;
            this.StartDAButton.Click += new System.EventHandler(this.StartDAButton_Click);
            // 
            // Canvas
            // 
            this.Canvas.BackColor = System.Drawing.Color.White;
            this.Canvas.Location = new System.Drawing.Point(0, 50);
            this.Canvas.Margin = new System.Windows.Forms.Padding(1);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(1100, 450);
            this.Canvas.TabIndex = 4;
            this.Canvas.TabStop = false;
            this.Canvas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Canvas_Click);
            this.Canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseDown);
            this.Canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseMove);
            this.Canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseUp);
            this.Canvas.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Canvas_DoubleMouseClick);
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(900, 0);
            this.ClearButton.Margin = new System.Windows.Forms.Padding(1);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(100, 50);
            this.ClearButton.TabIndex = 4;
            this.ClearButton.Text = "Очистить";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // MoveButton
            // 
            this.MoveButton.Location = new System.Drawing.Point(0, 0);
            this.MoveButton.Margin = new System.Windows.Forms.Padding(2);
            this.MoveButton.Name = "MoveButton";
            this.MoveButton.Size = new System.Drawing.Size(100, 50);
            this.MoveButton.TabIndex = 5;
            this.MoveButton.Text = "Переместить";
            this.MoveButton.UseVisualStyleBackColor = true;
            this.MoveButton.Click += new System.EventHandler(this.MoveButton_Click);
            // 
            // ChangeThemeButton
            // 
            this.ChangeThemeButton.Location = new System.Drawing.Point(1000, 0);
            this.ChangeThemeButton.Name = "ChangeThemeButton";
            this.ChangeThemeButton.Size = new System.Drawing.Size(100, 50);
            this.ChangeThemeButton.TabIndex = 6;
            this.ChangeThemeButton.Text = "Сменить тему";
            this.ChangeThemeButton.UseVisualStyleBackColor = true;
            this.ChangeThemeButton.Click += new System.EventHandler(this.ChangeThemeButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 500);
            this.Controls.Add(this.ChangeThemeButton);
            this.Controls.Add(this.MoveButton);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.Canvas);
            this.Controls.Add(this.StartDAButton);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.RemoveButton);
            this.Controls.Add(this.AddButton);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Расчет кратчайшего пути по графу местности на основе алгоритма Дейкстры";
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Button AddButton;
        private Button RemoveButton;
        private Button ConnectButton;
        private Button StartDAButton;
        private PictureBox Canvas;
        private Button ClearButton;
        private Button MoveButton;
        private Button ChangeThemeButton;
    }
}