using System;
using static System.Console;
using static System.Math;

public static class sfun{
	
	public static vector gradRosenbrock(vector x){ // The gradient of the Rosenbrock function as given in the Homework description

		vector v = new vector(2);

		v[0] = 2*(x[0]*( 1 + 200*(x[1] - x[0]*x[0]) ) - 1);
		v[1] = 200*(x[1] - x[0]*x[0]);

		return v;

} //gradRosenbrock

	public static vector gradsfun(vector x){ // the gradient of a simple function: f(x,y) = -(x^2 + y^2) + x*y

		vector v = new vector(2);
		
		v[0] = x[1] -2*x[0];
		v[1] = x[0] -2*x[1];

		return v;

} // gradParabola

	public static vector sine(vector x){ //

		vector y = new vector(1);

		y[0] =  Sin(x[0]);

		if(x[0] > PI && x[0] < 0) y[0] = 2;

		return y;

} // sine

} //sfun
