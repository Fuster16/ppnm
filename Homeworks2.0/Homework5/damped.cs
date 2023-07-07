using System;
using static System.Console;
using static System.Math;
using System.IO;

public static class damped{

	public static void Main(){
	
	// RKF45

	matrix A = new matrix($" 0.25 0 0 0 0 ; {3.0/32} {9.0/32} 0 0 0 ; {1932.0/2197} {-7200.0/2197} {7296.0/2197} 0 0 ; {439.0/216} -8 {3680.0/513} {-845.0/4104} 0 ; {-8.0/27} 2 {-3544.0/2565} {1859.0/4104} {-11.0/40} "); // Unsure if matrix can be written with /n

	//string A_string = calcs.MTS(A, "A = ");
	//WriteLine(A_string);

	vector c = new vector(new double[6] {0, 0.25, 3.0/8, 12.0/13, 1.0, 0.5});
	vector b1 = new vector(new double[6] {16.0/135, 0, 6656.0/12825, 28561.0/56430, -9.0/50, 2.0/55});
	vector b0 = new vector(new double[6] {25.0/216, 0, 1408.0/2565, 2197.0/4104, -1.0/5, 0});
	
	//string c_string = calcs.VTS(c, "c = ");
	//WriteLine(c_string);

	// RKF45

	Func<double,vector,double> DHM = (t,X) => -0.25*X[1] -5.0*Math.Sin(X[0]);// Damped Harmonic Motion
	Func<double,vector,vector> DHM_vectorized = (x,y) => ODE.Y(x, y, DHM); // Vectorized ODE

	
	double a = 0; // initial time
	vector ya = new vector(Math.PI  - 0.1,0); // initial vector 
	double b = 10.0; // end time

	(ODE.genlist<double> x, ODE.genlist<vector> y) t2 = ODE.driverXY(DHM_vectorized, a, ya, b, A, b1, c, b0);

	for(int i = 0; i < t2.x.size; i++){

		WriteLine($"{t2.x[i]} {(t2.y[i])[0]} {(t2.y[i])[1]}");

	}
	WriteLine("\n");

}//Main

}//damped
