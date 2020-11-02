using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace lab6
{
    public class Algorithm
    {
        private List<Group> groups;
        private List<string> namesInUI;
        private Random rand = new Random();
        private int offsetX = 1;
        private int charNext = 0;

        public Algorithm()
        {
        }

        public void SetData(int count, double[,] data)
        {
            offsetX = 1; charNext = 0;
             groups = new List<Group>();

            for (int i = 0; i < count; i++)
            {
                groups.Add(new Group());
                groups[i].Name = "x" + (i + 1);
            }

            for (int i = 0; i < groups.Count; i++)
                for (int j = 0; j < groups.Count; j++)
                    if (i != j)
                        groups[i].Distances.Add(new Distance(data[i, j], groups[j]));

        }

        public void Do(bool isMax)
        {
            var result = false;
            do
            {
                result = false;
                double minDistance = int.MaxValue;
                var groupsWithMinDistance = new List<Group>();

                foreach (Group group in groups)
                {
                    foreach (Distance distance in group.Distances)
                    {
                        if (distance.Value < minDistance)
                        {
                            minDistance = distance.Value;
                            result = true;
                            groupsWithMinDistance.Clear();
                            groupsWithMinDistance.Add(group);
                        }
                        else if(distance.Value == minDistance)
                        {
                            groupsWithMinDistance.Add(group);
                        }
                    }
                }

                if (result && groupsWithMinDistance.Any())
                    SetNewGroups(groupsWithMinDistance, minDistance, isMax);

            } while (result);
        }


        

        private char NextChar()
        {
            return (char)('a' + charNext++);
        }

        public void Draw(Chart chart)
        {
            namesInUI = new List<string> ();
            SetDefaultChart(chart);
            foreach (Group subGroup in groups)
                DrawSubGroups(subGroup, chart);
        }

        private void SetDefaultChart(Chart chart)
        {
            chart.Series.Clear();
            chart.ChartAreas[0].AxisX.ArrowStyle = chart.ChartAreas[0].AxisY.ArrowStyle = AxisArrowStyle.Lines;
            chart.ChartAreas[0].AxisX.Crossing = chart.ChartAreas[0].AxisY.Crossing = 0;
            chart.ChartAreas[0].AxisX.IsStartedFromZero = chart.ChartAreas[0].AxisY.IsStartedFromZero = true;
            chart.ChartAreas[0].AxisX.Title = chart.ChartAreas[0].AxisY.Title = "";
            chart.ChartAreas[0].AxisX.Interval = chart.ChartAreas[0].AxisY.Interval = 1;
            chart.ChartAreas[0].AxisX.LineWidth= chart.ChartAreas[0].AxisY.LineWidth = 1;
            chart.ChartAreas[0].AxisX.Minimum = chart.ChartAreas[0].AxisY.Minimum = 0;
            chart.ChartAreas[0].AxisX.Maximum = offsetX;
            chart.ChartAreas[0].AxisY.Maximum = 12;
        }

        private void DrawSubGroups(Group group, Chart chart)
        {
            bool res = true;

            foreach (Series currSeries in chart.Series)
                if (currSeries.Name == group.Name)
                    res = false;

            if (res)
            {
                var pointsSeries = new Series { ChartType = SeriesChartType.Point, IsVisibleInLegend = false };

                pointsSeries.Name = group.Name;
                pointsSeries.MarkerSize = 1;
                pointsSeries.MarkerColor = NewColor();
                pointsSeries.Points.AddXY(group.X, group.Y);

                if (chart.Series.IndexOf(pointsSeries) == -1)
                    chart.Series.Add(pointsSeries);

                foreach (Group subGroup in group.SubGroups)
                {
                    var lineSeries = new Series { ChartType = SeriesChartType.Line, IsVisibleInLegend = false };

                    lineSeries.BorderWidth = 3;
                    lineSeries.Name = group.Name + " " + subGroup.Name;
                    lineSeries.Color = pointsSeries.MarkerColor;
                    SetLabel(lineSeries, lineSeries.Points.AddXY(subGroup.X, subGroup.Y), subGroup.Name);
                    lineSeries.Points.AddXY(subGroup.X, group.Y);
                    SetLabel(lineSeries, lineSeries.Points.AddXY(group.X, group.Y), group.Name);
                    res = true;

                    foreach (Series currSeries in chart.Series)
                        if (currSeries.Name == lineSeries.Name)
                            res = false;

                    if (res)
                        chart.Series.Add(lineSeries);

                    DrawSubGroups(subGroup, chart);
                }
            }
        }

        private void SetLabel(Series series, int id, string label)
        {
            if (!namesInUI.Contains(label))
            {
                series.Points[id].Label = label;
                namesInUI.Add(label);
            }
        }

        private void SetNewGroups(List<Group> data, double minDistance, bool isMax)
        {
            var newGroup = new Group();

            newGroup.Name = NextChar().ToString();
            foreach (Group group in groups)
            {
                if (!data.Contains(group))
                {
                    Distance minDist = group.GetDistance(data[0]);

                    foreach (Group currGroup in data)
                        if (group.GetDistance(currGroup).Value < minDist.Value)
                            minDist = group.GetDistance(currGroup);

                    group.DeleteDistances(data);
                    group.Distances.Add(new Distance(minDist.Value, newGroup));
                    newGroup.Distances.Add(new Distance(minDist.Value, group));
                }
            }

            foreach (Group group in data)
                if (group.X == 0)
                {
                    group.X = offsetX;
                    offsetX++;
                }

            newGroup.SubGroups = data;
            var subGroupsPoints = new List<HPoint>();
            foreach (Group addedGroup in data)
            {
                subGroupsPoints.Add(new HPoint(addedGroup.X, addedGroup.Y));
                groups.Remove(addedGroup);
            }

            double x = 0;

            foreach (HPoint point in subGroupsPoints)
                x += point.X;

            newGroup.X = x / subGroupsPoints.Count;
            newGroup.Y = isMax ? 1.0 / minDistance: minDistance;
            groups.Add(newGroup);
        }

        private Color NewColor()
        {
            return Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
        }
    }
}
