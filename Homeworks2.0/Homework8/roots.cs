using System;
using static System.Math;
using static System.Console;

public static class roots{ //Ordinary Least Squares fit

	public static vector newton(Func<vector,vector>f, vector x, double Itt = 100, double acc = 1.0/100, double pow = -26){

		double eps = Pow(2,pow);

		int k = 0;
		vector fl;
		vector dx;
		double la;

		do{ // while loop has upper limit

			k += 1;

			//// The newtons step

			matrix J = finiteJ(f, x, eps); // Calculate Jacobian of f at x numerically

			(matrix Q, matrix R) D = QRGS.decomp(J);
			dx = QRGS.solve(D.Q, D.R, -f(x)); // solves for dx

			//// The newtons step


			//// Backtracking

			la = SimpleBtrack(f, x, dx);

			//// Backtracking
		
			fl = f(x + la*dx);

			x += la*dx; // advances towards root
			//WriteLine($"Loop {k}");

		}while( k < Itt && Sqrt(fl.dot(fl)) > acc && la*Sqrt(dx.dot(dx)) > Sqrt(x.dot(x))*eps); // Ass soon as any of the given requirements are false, the while loop stops

		return x; // continues if step not below accuracy

} // newton

	public static matrix finiteJ(Func<vector,vector>f, vector x, double eps = 2e-26){ // Finite difference Jacobi calculation of the function f at x given descrete step del.

		vector fx = f(x); // necessary? Unsure but works
		// double xnorm = Sqrt(x.dot(x)) // I don't like the weirdness of the vectors.cs files's norm method
		
		matrix J = new matrix(fx.size, x.size); // (#rows, #columns) --> J_ik = (d/dx_k)f_i(x)

		for(int k = 0; k< x.size; k++){ // goes over each column

			vector xk = x; // re-defines for precision
			xk[k] += Abs(xk[k])*eps; // // descrete step in k'th indice

			J[k] = (f(xk) - fx)/(Abs(xk[k])*eps); // k'th column of Jacobian is evaluated

		}

		return J;

} // finiteJ

	public static double Btrack(Func<vector,vector>f, vector x, vector dx, double l = 1.0, int count = 0){ // takes function, evaluation point and the newton stepsize together with a trial l (lambda) ---> spits out next lambda to use newtons method on

		count += 1;

		vector fl = f(x + l*dx);
		vector f0 = f(x);

		double phi_l = fl.dot(fl);
		double phi_0 = f0.dot(f0);

		if( count > 20 || Sqrt(phi_l) < (1.0 - l/2)*Sqrt(phi_0) ) return l;

		return Btrack(f, x, dx, phi_0*l*l/(2*l*phi_0 + phi_l - phi_0),count); // follow from the book

} // Btrack

	public static double SimpleBtrack(Func<vector,vector>f, vector x, vector dx, double l = 1.0){

		vector fl = f(x + l*dx);
		vector f0 = f(x);

		double phi_l = fl.dot(fl);
		double phi_0 = f0.dot(f0);

		int k = 0;

		while(Sqrt(phi_l) > (1.0 - l/2)*Sqrt(phi_0) && k < 10){
		
			k += 1;

			l /= 2;
			fl = f(x + l*dx);
			phi_l = fl.dot(fl);
			//WriteLine($"{k}");
		}

		return l;


} // SimpleBtrack

	public static vector M(vector E, double r_min, vector F0, double r_max, matrix A, vector b1, vector c, vector b0, double h, double acc, double eps){

		Func<double,vector,vector> Y_vectorized = (r,F) => Aux(r,F,E); // Vectorized ODE
		
		vector O = new vector(1);

		//vector OD = ODE.driver(Y_vectorized, r_min, F0, r_max, null, null, A, b1, c, b0, h, acc, eps);

		
		O[0] = ODE.driver(Y_vectorized, r_min, F0, r_max, null, null, A , b1, c, b0, h, acc, eps)[0]; //Sqrt(OD.dot(OD))
		
	
		return O;

} // M

	public static vector Aux(double r, vector F,vector E){

		vector y = new vector(2);

		y[0] = F[1]; 
		y[1] = -2*(E[0] + 1.0/r)*F[0];

		return y;

} //Aux


}//roots