namespace Lab1
{
    partial class Form1
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.set = new System.Windows.Forms.Button();
            this.displayBox = new System.Windows.Forms.PictureBox();
            this.kmeans = new System.Windows.Forms.Button();
            this.clear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.displayBox)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(110, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(110, 45);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Кол переменных";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Кол кластеров";
            // 
            // set
            // 
            this.set.Location = new System.Drawing.Point(57, 83);
            this.set.Name = "set";
            this.set.Size = new System.Drawing.Size(75, 23);
            this.set.TabIndex = 4;
            this.set.Text = "Установить начальные значения";
            this.set.UseVisualStyleBackColor = true;
            this.set.Click += new System.EventHandler(this.set_Click);
            // 
            // displayBox
            // 
            this.displayBox.BackColor = System.Drawing.Color.White;
            this.displayBox.Location = new System.Drawing.Point(225, 15);
            this.displayBox.Name = "displayBox";
            this.displayBox.Size = new System.Drawing.Size(546, 423);
            this.displayBox.TabIndex = 5;
            this.displayBox.TabStop = false;
            // 
            // kmeans
            // 
            this.kmeans.Location = new System.Drawing.Point(57, 126);
            this.kmeans.Name = "kmeans";
            this.kmeans.Size = new System.Drawing.Size(75, 23);
            this.kmeans.TabIndex = 6;
            this.kmeans.Text = "К-средних";
            this.kmeans.UseVisualStyleBackColor = true;
            this.kmeans.Click += new System.EventHandler(this.kmeans_Click);
            // 
            // clear
            // 
            this.clear.Location = new System.Drawing.Point(57, 168);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(75, 23);
            this.clear.TabIndex = 7;
            this.clear.Text = "очистить";
            this.clear.UseVisualStyleBackColor = true;
            this.clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.kmeans);
            this.Controls.Add(this.displayBox);
            this.Controls.Add(this.set);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Lab1_kmeans_Piskarev";
            ((System.ComponentModel.ISupportInitialize)(this.displayBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button set;
        private System.Windows.Forms.PictureBox displayBox;
        private System.Windows.Forms.Button kmeans;
        private System.Windows.Forms.Button clear;
    }
}

