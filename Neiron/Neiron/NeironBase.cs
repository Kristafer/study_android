using System;
using System.Collections.Generic;
using System.Text;

namespace Neiron
{
	public class NeironBase: NeironLogic
	{
		private const double E = 0.15;
		private const double A = 0.65;
		protected double[] Data;
		protected double[] Weight;
		protected double Result;
		protected double SumResult;
		protected double Error;
		protected int Size;
		public string NameCategory;

		protected NeironBase(int size)
		{
			Size = size;
			Data = new double[size];
			Weight = new double[size];
		}

		protected NeironBase(int size, string nameCategory)
		{
			Size = size;
			NameCategory = nameCategory;
			Data = new double[size];
			Weight = new double[size];
		}

		public void SetData(double[] data)
		{
			for (var i = 0; i < Size; i++)
			{
				Data[i] = data[i];
			}
		}

		public void SetResult()
		{
			SumResult = 0;
			for (var i = 0; i < Size; i++)
			{
				SumResult += Data[i] * Weight[i];
			}

			Result = GetSigmoidResult(SumResult);
		}

		public double GetResult()
		{
			return Result;
		}

		public void SetError(double resultIdeal)
		{
			Error = resultIdeal - Result;
		}

		public void SetError(double[] errorPrevious, double[] weight)
		{
			Error = 0;
			for (var i = 0; i < errorPrevious.Length; i++)
			{
				Error += errorPrevious[i] * weight[i];
			}
		}
		public void InitWeight(Random r)
		{
			for (var i = 0; i < Weight.Length; i++)
			{
				Weight[i] = r.NextDouble() * 0.3 + 0.005;
			}
		}

		public void UpdateWeight()
		{
			for (var i = 0; i < Size; i++)
			{
				var grad = Data[i] * Error;
				Weight[i] += (E * grad);
			}
		}
	}
}
