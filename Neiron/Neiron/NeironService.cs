using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Neiron
{
	public class NeironService
	{
		private double[,] data;
		private Neiron[] _inputNeirons;
		private Neiron[] _outputNeirons;
		private const int data_size = 16;

		public NeironService()
		{
		}

		public void Init(List<string> categories)
		{
			_inputNeirons = new Neiron[50];
			_outputNeirons = new Neiron[categories.Count];
			for (var i = 0; i < _inputNeirons.Length; i++)
			{
				_inputNeirons[i] = new Neiron(data_size* data_size);
			}

			for (var i = 0; i < _outputNeirons.Length; i++)
			{
				_outputNeirons[i] = new Neiron(_inputNeirons.Length, categories[i]);
			}
		}

		public string GetCategory(double[,] data)
		{
			AlgorithmDo(data);
			var neiron = GetMaxResult();

			return $"{neiron.NameCategory} - {neiron.GetResult()}";
		}

		public void AlgorithmDo(double[,] data)
		{
			var inputData = NormalizeData(data);
			var outputData = new double[_inputNeirons.Length];
			for (var i = 0; i < _inputNeirons.GetLength(0); i++)
			{
				_inputNeirons[i].SetData(inputData);
				_inputNeirons[i].SetResult();
				outputData[i] = _inputNeirons[i].GetResult();
			}

			for (var i = 0; i < _outputNeirons.GetLength(0); i++)
			{
				_outputNeirons[i].SetData(outputData);
				_outputNeirons[i].SetResult();
			}
		}

		public Neiron GetMaxResult()
		{
			var max = -1.0;
			Neiron neiron = null;
			for (var i = 0; i < _outputNeirons.GetLength(0); i++)
			{
				if (max < _outputNeirons[i].GetResult())
				{
					max = _outputNeirons[i].GetResult();
					neiron = _outputNeirons[i];
				}
			}

			return neiron;
		}


		public double[] NormalizeData(double[,] data)
		{
			var inputData = new double[data.GetLength(0)* data.GetLength(1)];
			var count = 0;
			for (var i = 0; i < data.GetLength(0); i++)
			{
				for (var j = 0; j < data.GetLength(1); j++)
				{
					inputData[count++] = data[i, j];
				}
			}

			return inputData;
		}
	}
}
