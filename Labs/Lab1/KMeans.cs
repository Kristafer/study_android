using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class KMeans
    {
        private const int MIN_KPOINTS_COUNT = 10;
        private const int MAX_KPOINTS_COUNT = int.MaxValue;
        private const int MIN_KLASTER_COUNT = 2;
        private const int MAX_KLASTER_COUNT = 256;
        private const int DEFAULT_KLASTER = 0;
        private const int INDENT = 10;
        private Object lockObject = new Object();

        private int kpoints_count, k_count, xWidth, yHeight = 0;
        private Color[] colors;
        private KPoint[] kPoints;
        private Kernel[] OldKernels, NewKernels;
        private Random rand;
        private BufferedGraphics bufferedGraphics;
        public KMeans(BufferedGraphics bufferedGraphics)
        {
            this.bufferedGraphics = bufferedGraphics;
            rand = new Random();
        }

        public void KInit(int kpoint, int k, int width, int height)
        {
            kpoints_count = kpoint;
            k_count = k;
            xWidth = width;
            yHeight = height;
            colors = new Color[k_count];
            OldKernels = new Kernel[k_count];
            NewKernels = new Kernel[k_count];
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

        public void KmeansDo()
        {
            bool isKlasterChanged = true;

            while (isKlasterChanged)
            {
                isKlasterChanged = false;
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

                Parallel.ForEach(kernelPoints.Keys,
                new ParallelOptions() { MaxDegreeOfParallelism = kernelPoints.Keys.Count() },
                (kernel) =>
                {
                    int kpointNumber = -1;
                    double minAvrgDistance = int.MaxValue;
                    var listPoints = kernelPoints[kernel];
                    for (var i = 0; i < listPoints.Count(); i++)
                    {
                        double sumSquareDistance = 0, avrgDistances;
                        for (var j = 0; j < listPoints.Count(); j++)
                        {
                            sumSquareDistance += Math.Pow(EvklidDistance(listPoints[i], listPoints[j]), 2);
                        }
                        avrgDistances = Math.Sqrt(sumSquareDistance);
                        if (avrgDistances < minAvrgDistance)
                        {
                            minAvrgDistance = avrgDistances;
                            kpointNumber = i;
                        }
                    }

                    if (kpointNumber != -1)
                    {
                        NewKernels[kernel.Klaster].KPoint = listPoints[kpointNumber];
                    }
                });

                for (int i = 0; i < k_count; i++)
                {
                    if (!OldKernels[i].Equals(NewKernels[i]))
                    {
                        isKlasterChanged = true;
                        break;
                    }
                }

                Draw();
            }
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
