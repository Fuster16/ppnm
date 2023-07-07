using System;
using static System.Math;

public static class minimize{

	public static vector qnewton(Func<vector,double> f, vector y0, double MaxItt = 100, bool count = false, double acc = 0.1){

		// initials
		double fx = f(y0);
		vector gfx = grad(f,y0);
		matrix B0 = matrix.id(y0.size);
		vector dx = -B0*gfx;

		vector x = y0.copy();
		matrix B = B0;

		for(int i = 0; i<MaxItt; i++){ 

			gfx = grad(f,x);
			if(gfx.dot(gfx) < Pow(acc,2)){ // immediately checks gradient at x after i'th update. The accuracy is on the dot product --> avoid square roots.
				
				if( count == true ){ // The minimizer was a success and we might want to count the nr. Of itterations 
	
					vector c = new vector(x.size + 1);
					for(int k = 0; k<x.size;k++) c[k] = x[k];
					c[x.size] = i;	

					x = c;

				}

				break; 
			}
			
			fx = f(x);
			dx = -B*gfx;

			for(int j = 0; j< 10; j++){

				if( f(x + dx) < fx + 1e-4*gfx.dot(dx) ){ // If satisfied within 9 halfings --> update accordingly
					
					vector y = grad(f, x + dx) - gfx;
					vector u = dx - B*y;
					B += matrix.outer(u,u)/u.dot(y);
					break;
				}

				dx /= 2; // updating dx directly instead of having an extra variable
			
			}

			if( f(x + dx) > fx + 1e-4*gfx.dot(dx) ) B = B0; // --> Set to identity if linesearch fails

			x += dx; // step is taken 
		
		}

		return x;

} // qnewton

	public static vector grad(Func<vector,double> f, vector x){ 	// spits out gradient of a real valued function with n-dim arguments (n >= 1)
					 			    	// by finite difference method at x.
		
		double fx = f(x);					// used x.size # of times throughout
		vector g = new vector(x.size);				// initialize a gradient vector 

		matrix Step = new matrix(Pow(2,-26)*x.map(Abs));	// Matrix for organizing the x.size nr. of partial differentiations
		
		for(int i = 0; i<x.size;i++){ // filling of gradient 

			g[i] = (f(x + Step[i]) - f(x))/Step[i,i]; 

		}
		
		return g;

} //grad

}// minimize