using System;
using static System.Console;
using static System.Math;

public static class sfuns{

	public static double gamma(double x){ // returns a double and takes a double variable

		if(x<0)return PI/Sin(PI*x)/gamma(1-x);
		if(x<9)return gamma(x+1)/x;

		double gammaln = x*Log(x + 1 / (12*x - 1/x/10)) - x + Log(2*PI/x)/2;
		return Exp(gammaln);
	} //gamma

	public static double lngamma(double x)
	{
		if(x <= 0) return double.NaN;
		if(x < 9) return lngamma(x+1) - Log(x); // gamma(x+1)= x*gamma(x). A property of the gamma func.
		
		double lg = x*Log(x + 1/(12*x - 1/x/10)) - x + Log(2*PI/x)/2;
		return lg;
	} //lngamma
} //sfuns
