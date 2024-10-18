namespace Computer_Graphics_LR1
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
            components = new System.ComponentModel.Container();
            circularTrackBar1 = new CircularTrackBar();
            cTB1 = new CircularTrackBar();
            cTB2 = new CircularTrackBar();
            cTB3 = new CircularTrackBar();
            tB_coords = new ToggleButton();
            coords_lb = new Label();
            tB_dots = new ToggleButton();
            dots_lb = new Label();
            tB_letter = new ToggleButton();
            tB_freecamera = new ToggleButton();
            letter_lb = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            tB_anim = new ToggleButton();
            anim_lb = new Label();
            trackBar1 = new TrackBar();
            trackBar2 = new TrackBar();
            trackBar3 = new TrackBar();
            trackBar4 = new TrackBar();
            trackBar5 = new TrackBar();
            trackBar6 = new TrackBar();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar6).BeginInit();
            SuspendLayout();
            // 
            // circularTrackBar1
            // 
            circularTrackBar1.BackColor = Color.LightGray;
            circularTrackBar1.CircleColor = Color.White;
            circularTrackBar1.Location = new Point(-33, -11);
            circularTrackBar1.Maximum = 60;
            circularTrackBar1.Name = "circularTrackBar1";
            circularTrackBar1.Size = new Size(29, 29);
            circularTrackBar1.SliderColor = Color.Blue;
            circularTrackBar1.TabIndex = 0;
            circularTrackBar1.Text = "circularTrackBar1";
            circularTrackBar1.TextColor = Color.Blue;
            circularTrackBar1.TrackColor = Color.LightGray;
            circularTrackBar1.Value = 1;
            // 
            // cTB1
            // 
            cTB1.BackColor = Color.LightGray;
            cTB1.CircleColor = Color.White;
            cTB1.Location = new Point(1033, 528);
            cTB1.Maximum = 360;
            cTB1.Name = "cTB1";
            cTB1.Size = new Size(121, 121);
            cTB1.SliderColor = Color.Blue;
            cTB1.TabIndex = 1;
            cTB1.Text = "circularTrackBar2";
            cTB1.TextColor = Color.Blue;
            cTB1.TrackColor = Color.White;
            cTB1.Value = 1;
            cTB1.Click += cTB1_Click;
            // 
            // cTB2
            // 
            cTB2.BackColor = Color.LightGray;
            cTB2.CircleColor = Color.White;
            cTB2.Location = new Point(1160, 528);
            cTB2.Maximum = 360;
            cTB2.Name = "cTB2";
            cTB2.Size = new Size(121, 121);
            cTB2.SliderColor = Color.Blue;
            cTB2.TabIndex = 2;
            cTB2.Text = "circularTrackBar2";
            cTB2.TextColor = Color.Blue;
            cTB2.TrackColor = Color.White;
            cTB2.Value = 1;
            cTB2.Click += cTB2_Click;
            // 
            // cTB3
            // 
            cTB3.BackColor = Color.LightGray;
            cTB3.CircleColor = Color.White;
            cTB3.Location = new Point(1287, 528);
            cTB3.Maximum = 360;
            cTB3.Name = "cTB3";
            cTB3.Size = new Size(121, 121);
            cTB3.SliderColor = Color.Blue;
            cTB3.TabIndex = 3;
            cTB3.Text = "circularTrackBar2";
            cTB3.TextColor = Color.Blue;
            cTB3.TrackColor = Color.White;
            cTB3.Value = 1;
            cTB3.Click += cTB3_Click;
            // 
            // tB_coords
            // 
            tB_coords.AutoSize = true;
            tB_coords.Location = new Point(1055, 655);
            tB_coords.MaximumSize = new Size(70, 32);
            tB_coords.MinimumSize = new Size(70, 32);
            tB_coords.Name = "tB_coords";
            tB_coords.OffBackColor = Color.Gray;
            tB_coords.OffToggleColor = Color.Gainsboro;
            tB_coords.OnBackColor = Color.MediumSlateBlue;
            tB_coords.OnToggleColor = Color.WhiteSmoke;
            tB_coords.Size = new Size(70, 32);
            tB_coords.TabIndex = 4;
            tB_coords.Text = "toggleButton1";
            tB_coords.UseVisualStyleBackColor = true;
            tB_coords.CheckedChanged += tB1_CheckedChanged;
            // 
            // coords_lb
            // 
            coords_lb.AutoSize = true;
            coords_lb.Location = new Point(1146, 660);
            coords_lb.Name = "coords_lb";
            coords_lb.Size = new Size(165, 20);
            coords_lb.TabIndex = 5;
            coords_lb.Text = "Включить координаты";
            // 
            // tB_dots
            // 
            tB_dots.AutoSize = true;
            tB_dots.Location = new Point(1055, 490);
            tB_dots.MaximumSize = new Size(70, 32);
            tB_dots.MinimumSize = new Size(70, 32);
            tB_dots.Name = "tB_dots";
            tB_dots.OffBackColor = Color.Gray;
            tB_dots.OffToggleColor = Color.Gainsboro;
            tB_dots.OnBackColor = Color.MediumSlateBlue;
            tB_dots.OnToggleColor = Color.WhiteSmoke;
            tB_dots.Size = new Size(70, 32);
            tB_dots.TabIndex = 6;
            tB_dots.Text = "tB2";
            tB_dots.UseVisualStyleBackColor = true;
            tB_dots.CheckedChanged += tB2_CheckedChanged;
            // 
            // dots_lb
            // 
            dots_lb.AutoSize = true;
            dots_lb.Location = new Point(1146, 495);
            dots_lb.Name = "dots_lb";
            dots_lb.Size = new Size(197, 20);
            dots_lb.TabIndex = 7;
            dots_lb.Text = "Включить точки координат";
            // 
            // tB_letter
            // 
            tB_letter.AutoSize = true;
            tB_letter.Location = new Point(1055, 452);
            tB_letter.MaximumSize = new Size(70, 32);
            tB_letter.MinimumSize = new Size(70, 32);
            tB_letter.Name = "tB_letter";
            tB_letter.OffBackColor = Color.Gray;
            tB_letter.OffToggleColor = Color.Gainsboro;
            tB_letter.OnBackColor = Color.MediumSlateBlue;
            tB_letter.OnToggleColor = Color.WhiteSmoke;
            tB_letter.Size = new Size(70, 32);
            tB_letter.TabIndex = 8;
            tB_letter.Text = "toggleButton1";
            tB_letter.UseVisualStyleBackColor = true;
            tB_letter.CheckedChanged += tB_letter_CheckedChanged;
            // 
            // tB_freecamera
            // 
            tB_freecamera.AutoSize = true;
            tB_freecamera.Location = new Point(1055, 414);
            tB_freecamera.MaximumSize = new Size(70, 32);
            tB_freecamera.MinimumSize = new Size(70, 32);
            tB_freecamera.Name = "tB_freecamera";
            tB_freecamera.OffBackColor = Color.Gray;
            tB_freecamera.OffToggleColor = Color.Gainsboro;
            tB_freecamera.OnBackColor = Color.MediumSlateBlue;
            tB_freecamera.OnToggleColor = Color.WhiteSmoke;
            tB_freecamera.Size = new Size(70, 32);
            tB_freecamera.TabIndex = 9;
            tB_freecamera.Text = "toggleButton1";
            tB_freecamera.UseVisualStyleBackColor = true;
            tB_freecamera.CheckedChanged += tB_freecamera_CheckedChanged;
            // 
            // letter_lb
            // 
            letter_lb.AutoSize = true;
            letter_lb.Location = new Point(1146, 457);
            letter_lb.Name = "letter_lb";
            letter_lb.Size = new Size(136, 20);
            letter_lb.TabIndex = 10;
            letter_lb.Text = "Буква вместо куба";
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // tB_anim
            // 
            tB_anim.AutoSize = true;
            tB_anim.Location = new Point(1055, 376);
            tB_anim.MaximumSize = new Size(70, 32);
            tB_anim.MinimumSize = new Size(70, 32);
            tB_anim.Name = "tB_anim";
            tB_anim.OffBackColor = Color.Gray;
            tB_anim.OffToggleColor = Color.Gainsboro;
            tB_anim.OnBackColor = Color.MediumSlateBlue;
            tB_anim.OnToggleColor = Color.WhiteSmoke;
            tB_anim.Size = new Size(70, 32);
            tB_anim.TabIndex = 12;
            tB_anim.Text = "toggleButton1";
            tB_anim.UseVisualStyleBackColor = true;
            tB_anim.CheckedChanged += tB_anim_CheckedChanged;
            // 
            // anim_lb
            // 
            anim_lb.AutoSize = true;
            anim_lb.Location = new Point(1146, 381);
            anim_lb.Name = "anim_lb";
            anim_lb.Size = new Size(82, 20);
            anim_lb.TabIndex = 13;
            anim_lb.Text = "Анимация";
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(1330, 11);
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(130, 56);
            trackBar1.TabIndex = 14;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // trackBar2
            // 
            trackBar2.Location = new Point(1330, 73);
            trackBar2.Name = "trackBar2";
            trackBar2.Size = new Size(130, 56);
            trackBar2.TabIndex = 15;
            trackBar2.Scroll += trackBar2_Scroll;
            // 
            // trackBar3
            // 
            trackBar3.Location = new Point(1330, 197);
            trackBar3.Name = "trackBar3";
            trackBar3.Size = new Size(130, 56);
            trackBar3.TabIndex = 17;
            trackBar3.Scroll += trackBar3_Scroll;
            // 
            // trackBar4
            // 
            trackBar4.Location = new Point(1330, 135);
            trackBar4.Name = "trackBar4";
            trackBar4.Size = new Size(130, 56);
            trackBar4.TabIndex = 16;
            trackBar4.Scroll += trackBar4_Scroll;
            // 
            // trackBar5
            // 
            trackBar5.Location = new Point(1330, 321);
            trackBar5.Name = "trackBar5";
            trackBar5.Size = new Size(130, 56);
            trackBar5.TabIndex = 19;
            trackBar5.Scroll += trackBar5_Scroll;
            // 
            // trackBar6
            // 
            trackBar6.Location = new Point(1330, 259);
            trackBar6.Name = "trackBar6";
            trackBar6.Size = new Size(130, 56);
            trackBar6.TabIndex = 18;
            trackBar6.Scroll += trackBar6_Scroll;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightGray;
            ClientSize = new Size(1528, 695);
            Controls.Add(trackBar5);
            Controls.Add(trackBar6);
            Controls.Add(trackBar3);
            Controls.Add(trackBar4);
            Controls.Add(trackBar2);
            Controls.Add(trackBar1);
            Controls.Add(anim_lb);
            Controls.Add(tB_anim);
            Controls.Add(letter_lb);
            Controls.Add(tB_freecamera);
            Controls.Add(tB_letter);
            Controls.Add(dots_lb);
            Controls.Add(tB_dots);
            Controls.Add(coords_lb);
            Controls.Add(tB_coords);
            Controls.Add(cTB3);
            Controls.Add(cTB2);
            Controls.Add(cTB1);
            Controls.Add(circularTrackBar1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar2).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar3).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar4).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar5).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar6).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CircularTrackBar circularTrackBar1;
        private CircularTrackBar cTB1;
        private CircularTrackBar cTB2;
        private CircularTrackBar cTB3;
        private ToggleButton tB_coords;
        private Label coords_lb;
        private ToggleButton tB_dots;
        private Label dots_lb;
        private ToggleButton tB_letter;
        private ToggleButton tB_freecamera;
        private Label letter_lb;
        private System.Windows.Forms.Timer timer1;
        private ToggleButton tB_anim;
        private Label anim_lb;
        private TrackBar trackBar1;
        private TrackBar trackBar2;
        private TrackBar trackBar3;
        private TrackBar trackBar4;
        private TrackBar trackBar5;
        private TrackBar trackBar6;
    }
}
