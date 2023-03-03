
namespace KEKESGRAPHICAPPLICATION
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.MoveFigure = new System.Windows.Forms.Button();
            this.MovePolygon = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Mongolian Baiti", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button1.Location = new System.Drawing.Point(14, 451);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(131, 44);
            this.button1.TabIndex = 0;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MoveFigure
            // 
            this.MoveFigure.Location = new System.Drawing.Point(14, 399);
            this.MoveFigure.Name = "MoveFigure";
            this.MoveFigure.Size = new System.Drawing.Size(131, 46);
            this.MoveFigure.TabIndex = 1;
            this.MoveFigure.Text = "MoveFigure";
            this.MoveFigure.UseVisualStyleBackColor = true;
            this.MoveFigure.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MoveFigure_MouseClick);
            // 
            // MovePolygon
            // 
            this.MovePolygon.Location = new System.Drawing.Point(14, 341);
            this.MovePolygon.Name = "MovePolygon";
            this.MovePolygon.Size = new System.Drawing.Size(131, 52);
            this.MovePolygon.TabIndex = 2;
            this.MovePolygon.Text = "MovePolygon";
            this.MovePolygon.UseVisualStyleBackColor = true;
            this.MovePolygon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MovePolygon_MouseClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1007, 558);
            this.Controls.Add(this.MovePolygon);
            this.Controls.Add(this.MoveFigure);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button MoveFigure;
        private System.Windows.Forms.Button MovePolygon;
    }
}

