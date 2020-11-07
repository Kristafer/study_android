using System;
using System.Collections.Generic;
using System.Text;

namespace Neiron
{
	public class NeironLogic
	{

		protected double GetSigmoidResult(double sum)
		{
			return 1.0 / (1.0 + Math.Exp(-sum));
		}
	}
}
