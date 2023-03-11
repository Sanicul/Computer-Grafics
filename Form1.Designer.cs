
namespace BezierSurname
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
            this.Struct = new System.Windows.Forms.Button();
            this.Clear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Struct
            // 
            this.Struct.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Struct.Font = new System.Drawing.Font("Comic Sans MS", 10F);
            this.Struct.Location = new System.Drawing.Point(12, 12);
            this.Struct.Name = "Struct";
            this.Struct.Size = new System.Drawing.Size(144, 33);
            this.Struct.TabIndex = 0;
            this.Struct.Text = "Hide Structure";
            this.Struct.UseVisualStyleBackColor = true;
            this.Struct.Click += new System.EventHandler(this.Struct_Click);
            // 
            // Clear
            // 
            this.Clear.Font = new System.Drawing.Font("Comic Sans MS", 10F);
            this.Clear.Location = new System.Drawing.Point(12, 51);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(144, 29);
            this.Clear.TabIndex = 1;
            this.Clear.Text = "Clear";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1854, 930);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.Struct);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Struct;
        private System.Windows.Forms.Button Clear;
    }
}

