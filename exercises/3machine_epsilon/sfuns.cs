using System;
using static System.Console;
using static System.Math;

public static class sfuns{

	public static int whilemax(int i){

		while(i+1>i)
		{
			i++;
		};

		return i;

} //whilemax
	
	public static int whilemin(int i){

		while(i-1< i)
		{
			i--;
		};

		return i;

} //whilemin

	public static bool approx(double a, double b, 
				  double acc = 1e-9, 
				  double eps = 1e-9
				 )
	
	{ 
		if(Abs(b-a) < acc) return true;
		else if(Abs(b-a) < Max(Abs(a),Abs(b))*eps) return true;

		else return false;

} // approx

} //sfuns
