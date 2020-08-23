namespace Alice
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
            this.Read = new System.Windows.Forms.Button();
            this.textBoxMost = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.import = new System.Windows.Forms.Button();
            this.adressLable = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Read
            // 
            this.Read.Location = new System.Drawing.Point(177, 122);
            this.Read.Name = "Read";
            this.Read.Size = new System.Drawing.Size(138, 23);
            this.Read.TabIndex = 0;
            this.Read.Text = "Read and Process";
            this.Read.UseVisualStyleBackColor = true;
            this.Read.Click += new System.EventHandler(this.Read_Click);
            // 
            // textBoxMost
            // 
            this.textBoxMost.Location = new System.Drawing.Point(210, 41);
            this.textBoxMost.Name = "textBoxMost";
            this.textBoxMost.Size = new System.Drawing.Size(49, 20);
            this.textBoxMost.TabIndex = 1;
            this.textBoxMost.Text = "10";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Number of most frequency words";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.SupportMultiDottedExtensions = true;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.SupportMultiDottedExtensions = true;
            // 
            // import
            // 
            this.import.Location = new System.Drawing.Point(41, 122);
            this.import.Name = "import";
            this.import.Size = new System.Drawing.Size(105, 23);
            this.import.TabIndex = 3;
            this.import.Text = "import text file";
            this.import.UseVisualStyleBackColor = true;
            this.import.Click += new System.EventHandler(this.import_Click);
            // 
            // adressLable
            // 
            this.adressLable.AutoSize = true;
            this.adressLable.ForeColor = System.Drawing.SystemColors.GrayText;
            this.adressLable.Location = new System.Drawing.Point(92, 204);
            this.adressLable.Name = "adressLable";
            this.adressLable.Size = new System.Drawing.Size(59, 13);
            this.adressLable.TabIndex = 17;
            this.adressLable.Text = "C:\\Alice.txt";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 203);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Adress : ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 247);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.adressLable);
            this.Controls.Add(this.import);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxMost);
            this.Controls.Add(this.Read);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Read;
        private System.Windows.Forms.TextBox textBoxMost;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button import;
        private System.Windows.Forms.Label adressLable;
        private System.Windows.Forms.Label label2;
    }
}

