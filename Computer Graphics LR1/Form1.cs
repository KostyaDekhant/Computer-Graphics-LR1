using System.Drawing;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

using System.Threading;
using System.Threading.Tasks;
using System;
using System.Reflection;

namespace Computer_Graphics_LR1
{
    public partial class Form1 : Form
    {
        private Matrix matrix;
        private Matrix camera;
        private float tx, ty, tz; //перемещение
        private float sx, sy, sz; //масштабирование
        private float rx, ry, rz; //поворот
        private float Rx, Ry, Rz; //доп. переменные поворота для камеры (не реализована)
        private System.Windows.Forms.TrackBar[] trackBars;
        static private PictureBox picBox;

        private int offset_x = 700;
        private int offset_y = 600;

        private const int axisX = -1000, axisY = 1000, axisZ = 1000;
        private int scale = 1;

        //Стартовое положение камеры
        const float cameraRX = (float)(Math.PI - (Math.PI / 16)),
                cameraRY = (float)(Math.PI + Math.PI / 16),
                cameraRZ = 0;

        //Наш объект (либо куб, либо буква)
        private Point3[] cubeVertices;
        //Доп объект, чтобы можно было отрисовать координаты
        Point3[] cubeTemp = new Point3[8];

        //Выставляем координаты куба
        private void initCube(out Point3[] cubeVertices)
        {
            cubeVertices = new Point3[8];
            cubeVertices[0] = new Point3(-100, -100, -100);
            cubeVertices[1] = new Point3(100, -100, -100);
            cubeVertices[2] = new Point3(100, 100, -100);
            cubeVertices[3] = new Point3(-100, 100, -100);
            cubeVertices[4] = new Point3(-100, -100, 100);
            cubeVertices[5] = new Point3(100, -100, 100);
            cubeVertices[6] = new Point3(100, 100, 100);
            cubeVertices[7] = new Point3(-100, 100, 100);
        }

        //Выставляем координаты буквы
        private void initLetter(out Point3[] letter)
        {
            letter = new Point3[32];

            // Верхний ряд
            letter[0] = new Point3(-60, 100, -100); //l
            letter[1] = new Point3(60, 100, -100);  //r
            letter[2] = new Point3(-60, 100, 100);  //l b
            letter[3] = new Point3(60, 100, 100);   //r b

            //левый и правый бок (части верха)
            letter[4] = new Point3(-60, -20, -100); //l
            letter[5] = new Point3(60, -20, -100);  //r
            letter[6] = new Point3(-60, -20, 100);  //l b
            letter[7] = new Point3(60, -20, 100);   //r b


            //левый и правый бок (части боковины, верхний край)
            letter[8] = new Point3(-100, -20, -100); //l
            letter[9] = new Point3(100, -20, -100);  //r
            letter[10] = new Point3(-100, -20, 100);  //l b
            letter[11] = new Point3(100, -20, 100);   //r b

            //левый и правый бок (части боковины, нижний край)
            letter[12] = new Point3(-100, -100, -100); //l
            letter[13] = new Point3(100, -100, -100);  //r
            letter[14] = new Point3(-100, -100, 100);  //l b
            letter[15] = new Point3(100, -100, 100);   //r b


            //левый и правый бок внутренней части (части боковины, нижний край)
            letter[16] = new Point3(-60, -100, -100); //l
            letter[17] = new Point3(60, -100, -100);  //r
            letter[18] = new Point3(-60, -100, 100);  //l b
            letter[19] = new Point3(60, -100, 100);   //r b

            //левый и правый бок внутренней части (части боковины, верхний край)
            letter[20] = new Point3(-60, -60, -100); //l
            letter[21] = new Point3(60, -60, -100);  //r
            letter[22] = new Point3(-60, -60, 100);  //l b
            letter[23] = new Point3(60, -60, 100);   //r b

            //внутр. верх
            letter[24] = new Point3(-20, 60, -100); //l
            letter[25] = new Point3(20, 60, -100);  //r
            letter[26] = new Point3(-20, 60, 100);  //l b
            letter[27] = new Point3(20, 60, 100);   //r b

            //внутр. низ
            letter[28] = new Point3(-20, -20, -100); //l
            letter[29] = new Point3(20, -20, -100);  //r
            letter[30] = new Point3(-20, -20, 100);  //l b
            letter[31] = new Point3(20, -20, 100);   //r b

        }

        string[] lbs = { "Перемещение по X", "Перемещение по Y", "Перемещение по Z",
            "Масштабирование по X", "Масштабирование по Y", "Масштабирование по Z" };
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            initCube(out cubeVertices);
            matrix = new Matrix();
            trackBars = new System.Windows.Forms.TrackBar[6];
            trackBars[0] = trackBar1;
            trackBars[1] = trackBar2;
            trackBars[2] = trackBar3;
            trackBars[3] = trackBar4;
            trackBars[4] = trackBar5;
            trackBars[5] = trackBar6;
            for (int i = 0; i < 6; i++)
            {
                //Текст возле trackbar
                Label lb = new Label();
                lb.Text = lbs[i];
                lb.Location = new Point(1700 - 50 - 150, 0 + 70 * i);
                lb.Size = new Size(150, 50);

                //Иниализация trackbar
                //trackBars[i] = new System.Windows.Forms.TrackBar();
                if (i < 3) trackBars[i].Minimum = -20000;
                else trackBars[i].Minimum = -5000;
                trackBars[i].Maximum = 20000;
                trackBars[i].Value = 0;
                //trackBars[i].ValueChanged += TrackBar_ValueChanged;
                //trackBars[i].ValueChanged += TrackBar_ValueChanged;
                trackBars[i].Location = new Point(1700 - 50, 0 + 70 * i);
                trackBars[i].Size = new Size(250, 50);
                Controls.Add(trackBars[i]);
                Controls.Add(lb);
            }

            //Тоже текст возле круглых trackbar
            Label lb1 = new Label();
            lb1.Text = "Вращение по X";
            lb1.Location = new Point(1500, 833);
            lb1.Size = new Size(120, 50);
            Controls.Add(lb1);

            Label lb2 = new Label();
            lb2.Text = "Вращение по Y";
            lb2.Location = new Point(1500 + 140, 833);
            lb2.Size = new Size(120, 50);
            Controls.Add(lb2);

            Label lb3 = new Label();
            lb3.Text = "Вращение по Y";
            lb3.Location = new Point(1500 + 280, 833);
            lb3.Size = new Size(120, 50);
            Controls.Add(lb3);

            //Кнопка восстановления состояния
            System.Windows.Forms.Button button = new System.Windows.Forms.Button();
            button.Location = new Point(1780, 670 + 30);
            button.Size = new Size(100, 50);
            button.Click += button_click;
            button.Text = "Reset";
            Controls.Add(button);

            //picBox Для отрисовки картинки
            picBox = new PictureBox();
            picBox.BackColor = Color.LightGray;
            picBox.Location = new Point(0, 0);
            picBox.Size = new Size(1700, 1080);
            Controls.Add(picBox);


            cTB1.ValueChanged += TrackBar_ValueChanged;
            cTB2.ValueChanged += TrackBar_ValueChanged;
            cTB3.ValueChanged += TrackBar_ValueChanged;
            cTB1.Location = new Point(1500, 870);
            cTB2.Location = new Point(1650, 870);
            cTB3.Location = new Point(1800, 870);

            tB_coords.Location = new Point(1500, 795 - 50);
            tB_dots.Location = new Point(1500, 830 - 50);
            tB_letter.Location = new Point(1500, 760 - 50);
            coords_lb.Location = new Point(1580, 833 - 50);
            dots_lb.Location = new Point(1580, 798 - 50);
            letter_lb.Location = new Point(1580, 763 - 50);
            tB_anim.Location = new Point(1500, 670);
            anim_lb.Location = new Point(1580, 670 + 4);

            this.Paint += Form1_Paint;
            this.WindowState = FormWindowState.Maximized;

            rx = cameraRX;
            ry = cameraRY;
            rz = cameraRZ;

            setCamera(scale, cameraRX, cameraRY, cameraRZ);
            button_click(button, null);
            tB_freecamera.Visible = false;

            initTrackBarVals();
            timer1.Interval = 1;
            trackBars[0].Value += 1;
            trackBars[0].Value -= 1;
            Invalidate();
        }

        private void button_click(object sender, EventArgs e)
        {
            for (int i = 0; i < 6; i++)
            {
                trackBars[i].Value = 0;
            }
            cTB1.Value = 0;
            cTB2.Value = 0;
            cTB3.Value = 0;
            Invalidate();
            //if (tB_letter.Checked) trackBars[5].Value = -4500;
        }

        private int letter_scale = 10;
        private void TrackBar_ValueChanged(object sender, EventArgs e)
        {
            if (tB_letter.Checked) letter_scale = 10;
            else letter_scale = 1;

            tx = trackBars[0].Value / 50f;
            ty = trackBars[1].Value / 50f;
            tz = -400 + trackBars[2].Value / 50f * letter_scale;
            sx = trackBars[3].Value / 5000f + 1;
            sy = trackBars[4].Value / 5000f + 1;
            sz = trackBars[5].Value / 5000f + 1;

            rx = -(cTB1.Value - 1) * 2 * 57 / 60 * (float)Math.PI * (float)Math.PI / 180 / 6;
            ry = (cTB2.Value - 1) * 2 * 57 / 60 * (float)Math.PI * (float)Math.PI / 180 / 6;
            rz = (cTB3.Value - 1) * 2 * 57 / 60 * (float)Math.PI * (float)Math.PI / 180 / 6;
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = picBox.CreateGraphics();
            g.Clear(Color.LightGray);
            DrawCoordinateSystem(g);
            DrawFigure(g, cubeVertices);

        }

        private void setCamera(float scale, float rx, float ry, float rz)
        {
            camera = matrix.ApplyTranslation(tx, ty, tz);
            camera = matrix.MultyMatrix(camera, matrix.ApplyRotationX(rx));
            camera = matrix.MultyMatrix(camera, matrix.ApplyRotationY(ry));
            camera = matrix.MultyMatrix(camera, matrix.ApplyRotationZ(rz));
            camera = matrix.MultyMatrix(camera, matrix.ApplyScaling(scale, scale, scale));

        }
        private void DrawCoordinateSystem(Graphics g)
        {
            Pen[] pens = { new Pen(Color.Green, 2), new Pen(Color.Blue, 2), new Pen(Color.Red, 2) };
            Point3[] axis = {
                new Point3(axisX, 0, 0),
                new Point3(0, axisY, 0),
                new Point3(0, 0, axisZ)
                };

            for (int i = 0; i < axis.Length; i++)
            {
                // Применяем трансформацию
                Point3 transformedPoint = matrix.ApplyTransformation(camera, axis[i]);
                Point2 projectedPoint = matrix.perspProj(transformedPoint, 1000);

                g.DrawLine(pens[i], offset_x, offset_y, offset_x + (int)projectedPoint.x, offset_y + (int)projectedPoint.y);
            }

        }

        private void DrawFigure(Graphics g, Point3[] cubeVertices)
        {
            if (tB_letter.Checked) { initLetter(out cubeVertices); initLetter(out cubeTemp); }
            else { initCube(out cubeVertices); initCube(out cubeTemp); }

            if (tB_anim.Checked) cubeVertices[anim_index] = tempP;

            Matrix translationToOrigin = matrix.ApplyTranslation(-tx, -ty, -tz);
            Matrix rotationXMatrix = matrix.ApplyRotationX(rx);
            Matrix rotationYMatrix = matrix.ApplyRotationY(ry);
            Matrix rotationZMatrix = matrix.ApplyRotationZ(rz);
            Matrix scalingMatrix = matrix.ApplyScaling(sx, sy, sz);
            Matrix translationBack = matrix.ApplyTranslation(tx, ty, tz);

            Matrix transformationMatrix = matrix.MultyMatrix(translationBack, scalingMatrix);
            transformationMatrix = matrix.MultyMatrix(transformationMatrix, rotationZMatrix);
            transformationMatrix = matrix.MultyMatrix(transformationMatrix, rotationYMatrix);
            transformationMatrix = matrix.MultyMatrix(transformationMatrix, rotationXMatrix);
            transformationMatrix = matrix.MultyMatrix(transformationMatrix, translationToOrigin);

            Matrix translationMatrix = matrix.ApplyTranslation(tx, ty, tz);
            transformationMatrix = matrix.MultyMatrix(transformationMatrix, translationMatrix);

            for (int i = 0; i < cubeVertices.Length; i++)
            {
                // Применение трансформаций
                cubeVertices[i] = matrix.ApplyTransformation(transformationMatrix, cubeVertices[i]);
                cubeVertices[i] = matrix.ApplyTransformation(camera, cubeVertices[i]);
                Point2 projectedPoint = matrix.perspProj(cubeVertices[i], 1000);
                cubeVertices[i] = new Point3(projectedPoint.x, projectedPoint.y, 0);

            }

            Pen pen = new Pen(Color.Black, 2);
            int size = 1;

            int t1 = offset_x;
            int t2 = offset_y;
            offset_x = 700;
            offset_y = 600;

            Point3[] cubeVertices1 = cubeVertices;
            for (int i = 0; i < cubeVertices.Length; i++)
            {
                cubeVertices1[i].x *= size;
                cubeVertices1[i].y *= size;
                cubeVertices1[i].z *= size;
            }

            if (tB_letter.Checked) DrawLetter(g, pen, cubeVertices1, size);
            else DrawCube(g, pen, cubeVertices1, size);

            if (tB_coords.Checked) DrawCoords(g, cubeVertices1);
            if (tB_dots.Checked) DrawDots(g, cubeVertices1);

            offset_x = t1;
            offset_y = t2;
        }


        //Рёбра 3д буквы
        private void DrawLetter(Graphics g, Pen pen, Point3[] cubeVertices1, int size)
        {
            //верх
            DrawEdge(g, pen, cubeVertices1[0], cubeVertices1[1], size);
            DrawEdge(g, pen, cubeVertices1[1], cubeVertices1[3], size);
            DrawEdge(g, pen, cubeVertices1[3], cubeVertices1[2], size);
            DrawEdge(g, pen, cubeVertices1[2], cubeVertices1[0], size);

            //основание верха
            DrawEdge(g, pen, cubeVertices1[0], cubeVertices1[4], size);
            DrawEdge(g, pen, cubeVertices1[1], cubeVertices1[5], size);
            DrawEdge(g, pen, cubeVertices1[2], cubeVertices1[6], size);
            DrawEdge(g, pen, cubeVertices1[3], cubeVertices1[7], size);

            //верх боковин
            DrawEdge(g, pen, cubeVertices1[4], cubeVertices1[8], size);
            DrawEdge(g, pen, cubeVertices1[5], cubeVertices1[9], size);
            DrawEdge(g, pen, cubeVertices1[6], cubeVertices1[10], size);
            DrawEdge(g, pen, cubeVertices1[7], cubeVertices1[11], size);

            //низ боковин
            DrawEdge(g, pen, cubeVertices1[8], cubeVertices1[12], size);
            DrawEdge(g, pen, cubeVertices1[9], cubeVertices1[13], size);
            DrawEdge(g, pen, cubeVertices1[10], cubeVertices1[14], size);
            DrawEdge(g, pen, cubeVertices1[11], cubeVertices1[15], size);

            //низ внутри боковин
            DrawEdge(g, pen, cubeVertices1[12], cubeVertices1[16], size);
            DrawEdge(g, pen, cubeVertices1[13], cubeVertices1[17], size);
            DrawEdge(g, pen, cubeVertices1[14], cubeVertices1[18], size);
            DrawEdge(g, pen, cubeVertices1[15], cubeVertices1[19], size);

            //верх внутри боковин
            DrawEdge(g, pen, cubeVertices1[20], cubeVertices1[16], size);
            DrawEdge(g, pen, cubeVertices1[21], cubeVertices1[17], size);
            DrawEdge(g, pen, cubeVertices1[22], cubeVertices1[18], size);
            DrawEdge(g, pen, cubeVertices1[23], cubeVertices1[19], size);

            //площадь низа
            DrawEdge(g, pen, cubeVertices1[20], cubeVertices1[21], size);
            DrawEdge(g, pen, cubeVertices1[21], cubeVertices1[23], size);
            DrawEdge(g, pen, cubeVertices1[23], cubeVertices1[22], size);
            DrawEdge(g, pen, cubeVertices1[22], cubeVertices1[20], size);

            //рёбра слева у верха и низа боковины
            DrawEdge(g, pen, cubeVertices1[8], cubeVertices1[10], size);
            DrawEdge(g, pen, cubeVertices1[9], cubeVertices1[11], size);
            DrawEdge(g, pen, cubeVertices1[12], cubeVertices1[14], size);
            DrawEdge(g, pen, cubeVertices1[13], cubeVertices1[15], size);


            //внутрь верха (верх часть)
            DrawEdge(g, pen, cubeVertices1[24], cubeVertices1[25], size);
            DrawEdge(g, pen, cubeVertices1[25], cubeVertices1[27], size);
            DrawEdge(g, pen, cubeVertices1[27], cubeVertices1[26], size);
            DrawEdge(g, pen, cubeVertices1[26], cubeVertices1[24], size);

            //внутрь верха (боковинки)
            DrawEdge(g, pen, cubeVertices1[24], cubeVertices1[28], size);
            DrawEdge(g, pen, cubeVertices1[25], cubeVertices1[29], size);
            DrawEdge(g, pen, cubeVertices1[26], cubeVertices1[30], size);
            DrawEdge(g, pen, cubeVertices1[27], cubeVertices1[31], size);

            //внутрь верха (низ)
            DrawEdge(g, pen, cubeVertices1[28], cubeVertices1[29], size);
            DrawEdge(g, pen, cubeVertices1[29], cubeVertices1[31], size);
            DrawEdge(g, pen, cubeVertices1[31], cubeVertices1[30], size);
            DrawEdge(g, pen, cubeVertices1[30], cubeVertices1[28], size);

            //рёбра нижней части верха
            DrawEdge(g, pen, cubeVertices1[4], cubeVertices1[6], size);
            DrawEdge(g, pen, cubeVertices1[5], cubeVertices1[7], size);

            //рёбра нижней части нижней части изнутри
            DrawEdge(g, pen, cubeVertices1[16], cubeVertices1[18], size);
            DrawEdge(g, pen, cubeVertices1[17], cubeVertices1[19], size);
        }

        //Рёбра куба
        private void DrawCube(Graphics g, Pen pen, Point3[] cubeVertices1, int size)
        {
            DrawEdge(g, pen, cubeVertices1[0], cubeVertices1[1], size);
            DrawEdge(g, pen, cubeVertices1[1], cubeVertices1[2], size);
            DrawEdge(g, pen, cubeVertices1[2], cubeVertices1[3], size);
            DrawEdge(g, pen, cubeVertices1[3], cubeVertices1[0], size);

            DrawEdge(g, pen, cubeVertices1[4], cubeVertices1[5], size);
            DrawEdge(g, pen, cubeVertices1[5], cubeVertices1[6], size);
            DrawEdge(g, pen, cubeVertices1[6], cubeVertices1[7], size);
            DrawEdge(g, pen, cubeVertices1[7], cubeVertices1[4], size);

            DrawEdge(g, pen, cubeVertices1[0], cubeVertices1[4], size);
            DrawEdge(g, pen, cubeVertices1[1], cubeVertices1[5], size);
            DrawEdge(g, pen, cubeVertices1[2], cubeVertices1[6], size);
            DrawEdge(g, pen, cubeVertices1[3], cubeVertices1[7], size);
        }

        private void DrawDots(Graphics g, Point3[] cube)
        {
            Pen pen = new Pen(Color.Orange, 4);
            for (int i = 0; i < cube.Length; i++)
            {
                g.DrawArc(pen,
                     offset_x + (int)cube[i].x - 2, offset_y + (int)cube[i].y - 2,
                     4, 4, 0, 360);
            }

        }
        private void DrawEdge(Graphics g, Pen pen, Point3 start, Point3 end, int size)
        {
            g.DrawLine(pen,
                      offset_x + (int)start.x, offset_y + (int)start.y,
                      offset_x + (int)end.x, offset_y + (int)end.y);
        }

        private void DrawCoords(Graphics g, Point3[] cube)
        {
            System.Drawing.Font font = new System.Drawing.Font("Arial", 10);
            Brush brush = Brushes.Black;

            for (int i = 0; i < cube.Length; i++)
            {
                PointF point = new PointF(offset_x + (int)cube[i].x - 50, offset_y + (int)cube[i].y - 25);
                string text = "(" + cubeTemp[i].x + " ; " + cubeTemp[i].y + " ; " + cubeTemp[i].z + ")";
                g.DrawString(text, font, brush, point);
            }
        }

        private Random random = new Random();

        private void getAnimVals()
        {
            float randomX = (float)(random.NextDouble() * 2 - 1);
            float randomY = (float)(random.NextDouble() * 2 - 1);
            float randomZ = (float)(random.NextDouble() * 2 - 1);

            //нормальная длина
            float length = (float)Math.Sqrt(randomX * randomX + randomY * randomY + randomZ * randomZ);
            randomX /= length;
            randomY /= length;
            randomZ /= length;

            float scale = 30.0f;
            randomX *= scale;
            randomY *= scale;
            randomZ *= scale;

            //запоминаем начальную позицию
            startP.x = cubeVertices[anim_index].x;
            startP.y = cubeVertices[anim_index].y;
            startP.z = cubeVertices[anim_index].z;
            //конечная позиция
            endP = new Point3(startP.x + randomX, startP.y + randomY, startP.z + randomZ);
        }

        private Point3 tempP = new Point3();
        private Point3 startP = new Point3();
        private Point3 endP;
        private int anim_index;
        Random rd = new Random();

        private float Shift(float start, float end, float t)
        {
            return start + (end - start) * t;
        }
        private int timer_count = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timer_count % 100 == 0)
            {
                if (timer_count > 0)
                {
                    cubeVertices[anim_index].x = startP.x;
                    cubeVertices[anim_index].y = startP.y;
                    cubeVertices[anim_index].z = startP.z;
                    int ind = anim_index;
                    while (ind == anim_index)
                    {
                        ind = rd.Next() % cubeVertices.Length;
                    }
                    anim_index = ind;
                }
                getAnimVals();
                timer_count = 0;
            }
            if (timer_count <= 50)
            {
                float t = timer_count / 50f;
                cubeVertices[anim_index].x = Shift(startP.x, endP.x, t);
                cubeVertices[anim_index].y = Shift(startP.y, endP.y, t);
                cubeVertices[anim_index].z = Shift(startP.z, endP.z, t);
                tempP.x = cubeVertices[anim_index].x;
                tempP.y = cubeVertices[anim_index].y;
                tempP.z = cubeVertices[anim_index].z;
            }
            else
            {
                float t = (timer_count - 50) / 50f;
                cubeVertices[anim_index].x = Shift(endP.x, startP.x, t);
                cubeVertices[anim_index].y = Shift(endP.y, startP.y, t);
                cubeVertices[anim_index].z = Shift(endP.z, startP.z, t);
                tempP.x = cubeVertices[anim_index].x;
                tempP.y = cubeVertices[anim_index].y;
                tempP.z = cubeVertices[anim_index].z;
            }
            timer_count++;
            Invalidate();
        }


        private void tB1_CheckedChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void tB2_CheckedChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void tB_letter_CheckedChanged(object sender, EventArgs e)
        {
            if (tB_letter.Checked)
            {
                initLetter(out cubeVertices);
            }
            else
            {
                initCube(out cubeVertices);
                trackBars[5].Value = 0;
            }
            Invalidate();
        }

        private void tB_freecamera_CheckedChanged(object sender, EventArgs e)
        {
            if (tB_freecamera.Checked)
            {
                rx = Rx;
                ry = Ry;
                rz = Rz;
                trackBars[3].Value = (int)rx;
                trackBars[4].Value = (int)ry;
                trackBars[5].Value = (int)rz;
            }
            else
            {
                Rx = rx;
                Ry = rx;
                Rz = rx;
            }
        }
        private void tB_anim_CheckedChanged(object sender, EventArgs e)
        {
            anim_index = 0;
            startP = new Point3();
            endP = new Point3();
            tempP = new Point3();
            if (tB_anim.Checked) timer1.Start();
            else timer1.Stop();
        }

        private void initTrackBarVals()
        {
            tx = -trackBars[0].Value / 50f;

            ty = trackBars[1].Value / 50f;
           
            if (tB_letter.Checked) letter_scale = 10;
            else letter_scale = 1;
            tz = -400 + trackBars[2].Value / 50f * letter_scale;

            sx = trackBars[3].Value / 5000f + 1;
            sy = trackBars[4].Value / 5000f + 1;
            sz = trackBars[5].Value / 5000f + 1;
            rx = -(cTB1.Value - 1) * 2 * 57 / 60 * (float)Math.PI * (float)Math.PI / 180 / 6;
            ry = (cTB2.Value - 1) * 2 * 57 / 60 * (float)Math.PI * (float)Math.PI / 180 / 6;
            rz = (cTB3.Value - 1) * 2 * 57 / 60 * (float)Math.PI * (float)Math.PI / 180 / 6;
            Invalidate();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            tx = -trackBars[0].Value / 50f;
            Invalidate();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            ty = trackBars[1].Value / 50f;
            Invalidate();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            if (tB_letter.Checked) letter_scale = 10;
            else letter_scale = 1;
            tz = -400 + trackBars[2].Value / 50f * letter_scale;

            Invalidate();
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            sx = trackBars[3].Value / 5000f + 1;
            Invalidate();
        }
        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            sy = trackBars[4].Value / 5000f + 1;
            Invalidate();
        }
        private void trackBar6_Scroll(object sender, EventArgs e)
        {
            sz = trackBars[5].Value / 5000f + 1;
            Invalidate();
        }

        private void cTB1_Click(object sender, EventArgs e)
        {
            rx = -(cTB1.Value - 1) * 2 * 57 / 60 * (float)Math.PI * (float)Math.PI / 180 / 6;
            Invalidate();
        }

        private void cTB2_Click(object sender, EventArgs e)
        {
            ry = (cTB2.Value - 1) * 2 * 57 / 60 * (float)Math.PI * (float)Math.PI / 180 / 6;
            Invalidate();
        }

        private void cTB3_Click(object sender, EventArgs e)
        {
            rz = (cTB3.Value - 1) * 2 * 57 / 60 * (float)Math.PI * (float)Math.PI / 180 / 6;
            Invalidate();
        }
    }
}