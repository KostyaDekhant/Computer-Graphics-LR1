using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Graphics_LR1
{
    public class Matrix
    {
        const int size = 4;
        public float[] Vertices { get; set; }

        public float[][] data;
        public Matrix()
        {
            data = new float[size][];
            for (int i = 0; i < size; i++)
            {
                data[i] = new float[size];
            }
        }

        public Matrix(int r, int c)
        {
            data = new float[size][];
            for (int i = 0; i < r; i++)
            {
                data[i] = new float[c];
            }
        }

        public Matrix ApplyTranslation(float tx, float ty, float tz)
        {
            Matrix mt = new Matrix(4,4);
            mt.data[0][0] = 1;
            mt.data[1][1] = 1;
            mt.data[2][2] = 1;
            mt.data[3][3] = 1;
            mt.data[0][3] = tx;
            mt.data[1][3] = ty;
            mt.data[2][3] = tz;
            return mt;
        }

        public Matrix MultyMatrix(Matrix tm1, Matrix tm2) { 
            Matrix mt = new Matrix(tm1.data.Length, tm2.data.Length);
            for (int i = 0; i < tm1.data.Length; i++){
                for (int j = 0; j < tm2.data[0].Length; j++){
                    for (int k = 0; k < tm1.data[0].Length; k++){
                        mt.data[i][j] += tm1.data[i][k] * tm2.data[k][j];
                    }
                }
            }
            return mt;
        }


        public Matrix ApplyRotationX(float angle)
        {
            Matrix mt = new Matrix(4, 4);
            mt.data[0][0] = 1;

            mt.data[1][1] = (float)Math.Cos(angle);
            mt.data[2][1] = (float)Math.Sin(angle);
            mt.data[1][2] = -(float)Math.Sin(angle);
            mt.data[2][2] = (float)Math.Cos(angle);

            mt.data[3][3] = 1;
            return mt;
        }

        public Matrix ApplyRotationY(float angle)
        {
            Matrix mt = new Matrix(4, 4);
            mt.data[0][0] = (float)Math.Cos(angle);
            mt.data[2][0] = -(float)Math.Sin(angle);
            mt.data[1][1] = 1;
            mt.data[0][2] = (float)Math.Sin(angle);
            mt.data[2][2] = (float)Math.Cos(angle);
            mt.data[3][3] = 1;
            return mt;
        }
        public Matrix ApplyRotationZ(float angle)
        {
            Matrix mt = new Matrix(4, 4);
            mt.data[0][0] = (float)Math.Cos(angle);
            mt.data[1][0] = (float)Math.Sin(angle);
            mt.data[0][1] = -(float)Math.Sin(angle);
            mt.data[1][1] = (float)Math.Cos(angle);

            mt.data[2][2] = 1;
            mt.data[3][3] = 1;
            return mt;
        }

        public Matrix ApplyScaling(float sx, float sy, float sz)
        {
            Matrix mt = new Matrix(4, 4);
            mt.data[0][0] = sx;
            mt.data[1][1] = sy;
            mt.data[2][2] = sz;
            mt.data[3][3] = 1;
            return mt;
           
        }

        public Point3 ApplyTransformation(Matrix mx, Point3 pt3)
        {
            Matrix v = new Matrix(4, 1);
            v.data[0][0] = pt3.x;
            v.data[1][0] = pt3.y;
            v.data[2][0] = pt3.z;
            v.data[3][0] = 1;
            Matrix res = MultyMatrix(mx, v);
            return new Point3(res.data[0][0], res.data[1][0], res.data[2][0]);
        }

        public Matrix perspectProj(double c)
        {
            Matrix mt = new Matrix(4, 4);
            mt.data[0][0] = 1;
            mt.data[1][1] = 1;
            mt.data[2][2] = 1;
            mt.data[3][3] = 1;
            mt.data[3][2] = (float)(-1 / c);
            return mt;
        }

        public Point2 perspProj(Point3 pt3, double c) 
        {
            Matrix vector = new Matrix(4, 1);
            vector.data[0][0] = pt3.x;
            vector.data[1][0] = pt3.y;
            vector.data[2][0] = pt3.z;
            vector.data[3][0] = 1;
            Matrix result = MultyMatrix(perspectProj(c), vector);
            Point2 pt2 = new Point2( result.data[0][0] / result.data[3][0], result.data[1][0] / result.data[3][0]);
            return pt2;
        }

    }
}
