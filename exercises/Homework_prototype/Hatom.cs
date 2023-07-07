using System;
using static System.Math;

public static class Hatom{
	
public static (vector, matrix) Rdiff(double lenght, double step, double acc = 1e-6){ // the Radial differential eq for l=0 of classical QM for hydrogen

	int npoints = (int)(lenght/step)-1; // number of cuts: Check with rmax = 10 and dr = 5 ---> 10/5 = 2 --> npoints to divide the interval into 2 is 1 --> 1 cut is needed
					    // npoints keeps track of the internal points of the interval. The explicit casting (int) always rounds down, making
					    // f((int)(lenght/step) * step) = 0 and not f(lenght) = 0.
	
	vector r = rvalues(lenght,step);

	matrix H = new matrix(npoints,npoints); // Initializing the corresponding Matrix

	double factor = -0.5/step/step;

	for(int i=0;i<npoints-1;i++) // missing -2*factor at the end of diagonal
	{
	   H[i,i]   = -(2*factor + 1/r[i]); 
	   H[i,i+1] =  factor; // elements just above diagonal
	   H[i+1,i] =  factor; // elements just below 
	}

	for(int p=0;p<npoints-2;p++){ // Maybe needed, maybe not ---> zeroes/initializes the remaining indices
			
			for(int q=p+2;q<npoints;q++){
			
			H[q,p] = 0;
			H[p,q] = 0;
	
			}
	}

	H[npoints-1,npoints-1] = -(2*factor + 1/r[npoints-1]); // adds missing element

	return jacobi.cyclic(H, acc);

} //Rdiff

public static vector Swave(int n, double lenght, double step){ // assumes lenght and step be in units bohr.

	int npoints = (int)(lenght/step)-1; // number of cuts ---> descrete values the analytic expression will be computed at
	vector R = new vector(npoints); // Initialize corresponding vector
	
	double a = 0.529*1e-10; // Bohr radius in meters
	
	if(n == 1){
		for(int i=0;i<npoints;i++)
		{
			R[i]= a*step*(i+1)*2*Pow(a,-3/2)*Exp(-step*(i+1)); //filling the i'th step with it's value
		}
		return R;
	}

	if(n == 2){
		for(int i=0;i<npoints;i++)
		{
			R[i]= a*step*(i+1)*Pow(a,-3/2)*(1 - step*(i+1)/2)*Exp(-step*(i+1)/2)/Sqrt(2);
		}
		return R;
	}

	if(n == 3){
		for(int i=0;i<npoints;i++)
		{
			R[i]= a*step*(i+1)*2*Pow(a,-3/2)*(1 - 2*step*(i+1)/3 + 2*Pow(step*(i+1),2)/27)*Exp(-step*(i+1)/3)/Sqrt(3)/3;
		}
		return R;
	}

	for(int i=0;i<npoints;i++) // If no 1,2 or 3 are not passed, return trivial solution.
		{
			R[i]= 0;
		}
	
	return R;

} //Swave

public static vector rvalues(double lenght, double step){

	int npoints = (int)(lenght/step)-1; 

	vector r = new vector(npoints); // vector with number of cuts
	
	for(int i=0;i<npoints;i++)
	{
		r[i]= step*(i+1); //filling the i'th step with it's value -- i=0 corresponds to the first internal point 
	}
	return r;

} //rvalues
	
} //Hatom