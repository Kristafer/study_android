using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    public class Maximin
    {
        private const int MIN_KPOINTS_COUNT = 10;
        private const int MAX_KPOINTS_COUNT = int.MaxValue;
        private const int MIN_KLASTER_COUNT = 2;
        private const int MAX_KLASTER_COUNT = 256;
        private const int DEFAULT_KLASTER = 0;
        private const int INDENT = 10;
        private Object lockObject = new Object();

        private int kpoints_count, k_count, xWidth, yHeight = 0;
        private bool isMaximin;
        private Color[] colors;
        private KPoint[] kPoints;
        private Kernel[] OldKernels, NewKernels;
        private Random rand;
        private BufferedGraphics bufferedGraphics;
        public Maximin(BufferedGraphics bufferedGraphics)
        {
            this.bufferedGraphics = bufferedGraphics;
            rand = new Random();
        }

        public void KInit(int kpoint, int width, int height)
        {
            kpoints_count = kpoint;
            k_count = 1;
            isMaximin = true;
            xWidth = width;
            yHeight = height;
            colors = new Color[100];
            OldKernels = new Kernel[100];
            NewKernels = new Kernel[100];
            kPoints = new KPoint[kpoints_count];

            for (var i = 0; i < kpoints_count; i++)
            {
                kPoints[i] = new KPoint();
                kPoints[i].X = INDENT + rand.Next(xWidth - 2 * INDENT);
                kPoints[i].Y = INDENT + rand.Next(yHeight - 2 * INDENT);
                kPoints[i].Klaster = DEFAULT_KLASTER;
            }

            for (var i = 0; i < k_count; i++)
            {
                colors[i] = NewColor();
                OldKernels[i] = NewKernels[i] = new Kernel();
                OldKernels[i].KPoint = NewKernels[i].KPoint = kPoints[rand.Next(kpoints_count)];
                OldKernels[i].Color = NewKernels[i].Color = colors[i];
                OldKernels[i].Klaster = NewKernels[i].Klaster = i;
            }

            Draw();
        }

        public void ClearBox()
        {
            bufferedGraphics.Graphics.Clear(Color.Black);
            bufferedGraphics.Render();
        }

        public void MaxMinDo()
        {
            if (isMaximin)
            {
                double[] maxDistances = new double[kpoints_count];
                int[] newKernelIndexes = new int[kpoints_count];
                double maxDistance = 0;
                int newKernelIndex = 0;


                for (var i = 0; i< k_count; i++)
                {
                    double maxDistanceInClass = 0;
                    int newKernelIndexInClass = 0;

                    for (int j = 0; j < kpoints_count; j++)
                            if (kPoints[j].Klaster == i &&
                                EvklidDistance(kPoints[j], NewKernels[i].KPoint) > maxDistanceInClass)
                            {
                                maxDistanceInClass = EvklidDistance(kPoints[j], NewKernels[i].KPoint);
                                newKernelIndexInClass = j;
                            }

                    maxDistances[i] = maxDistanceInClass;
                    newKernelIndexes[i] = newKernelIndexInClass;
                    if (maxDistances[i] > maxDistance)
                    {
                        maxDistance = maxDistances[i];
                        newKernelIndex = newKernelIndexes[i];
                    }
                }
                double sumPairsDistance = 0;
                int pair_count = 0;
                for(int i = 0; i< k_count; i++)
                {
                    for (int j = 0; j < k_count; j++)
                    {
                        if (i != j)
                        {
                            sumPairsDistance+= EvklidDistance(NewKernels[i].KPoint, NewKernels[j].KPoint);
                            pair_count++;
                        }

                    }
                }
                if ((maxDistance> 0 && (sumPairsDistance == 0 || pair_count ==0)) || maxDistance > (sumPairsDistance / (pair_count * 2))){

                    colors[k_count] = NewColor();
                    OldKernels[k_count] = NewKernels[k_count] = new Kernel();
                    OldKernels[k_count].KPoint = NewKernels[k_count].KPoint = kPoints[newKernelIndex];
                    OldKernels[k_count].Color = NewKernels[k_count].Color = colors[k_count];
                    OldKernels[k_count].Klaster = NewKernels[k_count].Klaster = k_count;

                    k_count++;
                    ResetPoints();
                    Draw();
                }
                else
                {
                    ShowError();
                }
            }
            else
            {
                ShowError();
            }
        }

        private void ShowError()
        {
            MessageBox.Show("Алгоритм не может добавить новую точку", "Алгоритм", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void ResetPoints()
        {
            Dictionary<Kernel, List<KPoint>> kernelPoints = new Dictionary<Kernel, List<KPoint>>();
            for (var i = 0; i < k_count; i++)
            {
                OldKernels[i] = (Kernel)NewKernels[i].Clone();
                kernelPoints.Add(NewKernels[i], new List<KPoint>());
            }

            Parallel.ForEach(kPoints,
            new ParallelOptions() { MaxDegreeOfParallelism = kPoints.Count() },
            (kPoint) =>
            {
                KDistance[] kDistances = new KDistance[k_count];
                KDistance Kmin = null;

                int count = 0;
                foreach (var kernel in kernelPoints.Keys)
                {
                    kDistances[count] = new KDistance();
                    kDistances[count].Kernel = kernel;
                    kDistances[count].Distance = EvklidDistance(kPoint, NewKernels[count].KPoint);

                    if (Kmin == null)
                    {
                        Kmin = kDistances[count];
                    }
                    else if (Kmin.Distance > kDistances[count].Distance)
                    {
                        Kmin = kDistances[count];
                    }
                    count++;
                }


                kPoint.Klaster = Kmin.Kernel.Klaster;
                lock (lockObject)
                {
                    kernelPoints[Kmin.Kernel].Add(kPoint);
                }
            });
        }

        private void Draw()
        {
            bufferedGraphics.Graphics.Clear(Color.Black);
            for (var i = 0; i < kpoints_count; i++)
            {
                bufferedGraphics.Graphics.FillRectangle(new SolidBrush(NewKernels[kPoints[i].Klaster].Color),
                    kPoints[i].X, kPoints[i].Y, 1, 1);
            }

            for (var i = 0; i < k_count; i++)
            {
                bufferedGraphics.Graphics.FillEllipse(new SolidBrush(NewKernels[i].Color),
                    NewKernels[i].KPoint.X, NewKernels[i].KPoint.Y, 10, 10);
            }

            bufferedGraphics.Render();
        }

        private Color NewColor()
        {
            return Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
        }

        private double EvklidDistance(KPoint a, KPoint b)
        {
            return Math.Sqrt(Math.Pow((a.X - b.X), 2) + Math.Pow((a.Y - b.Y), 2));
        }
    }
}
