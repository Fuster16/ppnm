using System;
using static System.Math;

public static class minimize2{

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

			g[i] = (f(x + Step[i]) - fx)/Step[i,i]; 

		}
		
		return g;

} //grad

	public static vector simplex(Func<vector,double> f, vector x, int MaxItt=1000, double acc = 0.001, vector Size0 = null){

		//// Initializing the polytope

		int n = x.size;
		matrix polytope = new matrix(n, n+1); // i like to use matrices for structure
		polytope[0] = x.copy(); // initial vertex (guess)

		if(Size0 == null){ 
			Size0 = x.map(t => 0.1); // default polytope distance (the map creates a new vector, so x is just as good)
		}

		matrix Polydist = new matrix(Size0);
		for(int i=0;i<n;i++){
			polytope[i+1] = x + Polydist[i]; // Each dimension is probed by Size0
		}

		//// Initializing the polytope


		int high = 0, low = 0; // preparing for loop
		for(int j = 1; j <= MaxItt; j++){

			//// Immediately checks for convergence after j'th loop

			vector centroid = polytope[0].copy();
			for(int i = 1; i < n+1; i++){ centroid += polytope[i];} // centroid (times n+1) of all points

			bool convergence = true;

			for(int i = 0; i < n+1; i++){
				if( (centroid -  polytope[i]*(n+1)).norm() > acc*(n+1)) convergence = false; break; // If any vertex is outside of an n-sphere of radius acc*(n+1) about the polytopes center of mass --> convergence is false
			}
			if(convergence == true) break;

			//// Immediately checks for convergence after j'th loop



			//// finding the highest , lowest , and centroid points of the simplex

			for(int i=0; i < n+1; i++){ // reminicent of the min() and max() methods from "vectors.cs"

				if( f(polytope[i]) > f(polytope[high]) ) high = i;
				if( f(polytope[i]) < f(polytope[low]) ) low = i;

			}

			centroid = (centroid-polytope[high])/n; // The centroid of the polytope excluding the vertex with the highest value.
			
			//// finding the highest , lowest , and centroid points of the simplex


			
			////// Simplex operations
			
			vector vec_hc = centroid - polytope[high]; // vector from vertex with highest value to centroid
			vector reflection = centroid + vec_hc;

			if( f(reflection) < f(polytope[low]) ){ // checks value of reflected vertex

				if( f(reflection + vec_hc) < f(reflection) ) polytope[high] = reflection + vec_hc; // doubles the reflected distance if true

				else polytope[high] = reflection; // else accepts reflection
			}

			else if( f(reflection) < f(polytope[high]) ) polytope[high] = reflection;

			else{ // everything was bad --> contract

				vector contraction = 0.5*(centroid + polytope[high]);

				if( f(contraction) < f(polytope[high]) ) polytope[high] = contraction;
				else for(int k=0; k < n+1; k++){ if(k == low){ continue;} polytope[k] = 0.5*(polytope[k]+polytope[low]); };
			}
		
			////// Simplex operations
			
			
		}

		return polytope[low]; // returns lowest point given MaxItt
	
} // simplex

}// minimize2