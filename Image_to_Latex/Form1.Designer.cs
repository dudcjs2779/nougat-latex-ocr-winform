namespace Image_to_Latex
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnPasteImg = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            btnGenerate = new Button();
            button1 = new Button();
            button2 = new Button();
            pictureBox1 = new PictureBox();
            textBox1 = new TextBox();
            label1 = new Label();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnPasteImg
            // 
            btnPasteImg.Font = new Font("Malgun Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnPasteImg.ForeColor = Color.Black;
            btnPasteImg.Location = new Point(3, 3);
            btnPasteImg.Name = "btnPasteImg";
            btnPasteImg.Size = new Size(375, 40);
            btnPasteImg.TabIndex = 0;
            btnPasteImg.Text = "paste image from clipboard";
            btnPasteImg.UseVisualStyleBackColor = true;
            btnPasteImg.Click += btnPasteImg_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.Controls.Add(btnPasteImg);
            flowLayoutPanel1.Controls.Add(btnGenerate);
            flowLayoutPanel1.Controls.Add(button1);
            flowLayoutPanel1.Controls.Add(button2);
            flowLayoutPanel1.Controls.Add(pictureBox1);
            flowLayoutPanel1.Controls.Add(textBox1);
            flowLayoutPanel1.Controls.Add(label1);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(838, 493);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // btnGenerate
            // 
            btnGenerate.Font = new Font("Malgun Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnGenerate.Location = new Point(384, 3);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(420, 40);
            btnGenerate.TabIndex = 3;
            btnGenerate.Text = "Generate Latex";
            btnGenerate.UseVisualStyleBackColor = true;
            btnGenerate.Click += btnGenerate_Click;
            // 
            // button1
            // 
            button1.Location = new Point(3, 49);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 5;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Visible = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(103, 49);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 6;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            button2.Visible = false;
            button2.Click += button2_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(3, 84);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(802, 244);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(4, 335);
            textBox1.Margin = new Padding(4);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Size = new Size(800, 80);
            textBox1.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Malgun Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.HotTrack;
            label1.Location = new Point(3, 419);
            label1.Name = "label1";
            label1.Size = new Size(46, 28);
            label1.TabIndex = 4;
            label1.Text = "text";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(838, 493);
            Controls.Add(flowLayoutPanel1);
            KeyPreview = true;
            Name = "Form1";
            Text = "Image to Latex";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            KeyDown += Form1_KeyDown;
            Resize += Form1_Resize;
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnPasteImg;
        private FlowLayoutPanel flowLayoutPanel1;
        private PictureBox pictureBox1;
        private TextBox textBox1;
        private Button btnGenerate;
        private Label label1;
        private Button button1;
        private Button button2;
    }
}