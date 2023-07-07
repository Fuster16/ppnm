using System;
using System.IO;
using static System.Math;
using static System.Console;

public static class spline{

	public static double[] linterp(double[] x, double[] y, double[] z){ // decided on z also being an (assumed ordered) array with first and last elements within the bounds of x
		
		if(!(x[0] <= z[0] && z[z.Length - 1] <= x[x.Length-1])) throw new Exception("z not bounded by x");
		
		double[] Lin = new double[z.Length];
		int i = binsearch(x,z[0]); // binary search --> finds which interval z[0] lies in
		
		for(int j = 0; j<z.Length; j++){
			
			while(!(z[j] <= x[i+1])) i+=1; // z is assumed ordered
			
        		double dx=x[i+1]-x[i]; 
     	  		double dy=y[i+1]-y[i];

			if(!(dx>0)) throw new Exception("x not sorted");

        		Lin[j] = y[i]+dy/dx*(z[j]-x[i]); // If dy and dx are (int) with dx>dy will falsly return y[i]! --> make sure array is doubles!!
		}
		return Lin;
}//linterp

	public static int binsearch(double[]x, double z){

		if(!(x[0]<=z && z<=x[x.Length-1])) throw new Exception("binsearch: z out of bounds");
		
		int i=0, j=x.Length-1; // first and last element index in array of interrest

		while(j-i>1){ // while more than two elements in array search --> continue searching

			int mid=(i+j)/2; // yields integer, since "/" operation on only integers. Operation always rounds down.
			if(z>x[mid]) i=mid; else j=mid; // If the given number z is above x[mid], mid becomes new lower bound of the array search.
		}

		return i; // z lies between x[i] and x[i+1].

}//binsearch

	public static double[] linterpInteg(double[] x, double[] y, double[] z){ // yields integral value of linterp from x[0] to z[]
										 // this function also assumes "double[] z" to be ordered such that we only need 1 use of bin-search.

		if(!(x[0] <= z[0] && z[z.Length - 1] <= x[x.Length-1])) throw new Exception("z not bounded by x"); 

		double[] Int = new double[z.Length];

		int i = binsearch(x,z[0]); // binary search of x --> we need to know where z[0] is

		int j = 0;
		for(int k = 0; k<z.Length; k++){

			while(!(z[k] <= x[i+1])){

				i+=1; // z is assumed ordered --> if z[k] does not lie in given interval go to the next.
			}

			while(j <i){ // while interval j is below i run "Int[n] +=" where k <= n.
					
				double Interval = 0.5*(y[j] + y[j+1])*(x[j+1] - x[j]);

				for(int n = k; n<z.Length; n++){

					Int[n] += Interval; // Analytic calculation. Integrates from x[j] to x[i] and adds to all subsequent array-elements of Int
						          //For k=0 we have x[j=0] to x[i] <= z[0] and for k=1 we have x[j=i] to x[i++] <= z[1] (x[i++] is the biggest x element below z[1])

				}

				j += 1; // proceed to next interval.
			}

			double dx = x[j+1]-x[j];
     	  		double dy = y[j+1]-y[j];
			double dz = z[k] - x[j];

			if(!(dx>0)) throw new Exception("x not sorted");

			Int[k] += (y[j] + 0.5*dy*dz/dx)*dz; // Analytic calculation. Adds the integral from x[i] to z[k], where x[i] is the biggest element in x below z[k].
		}

		return Int; // Fingers crossed

}//linterpInteg

	public static double[] qcoeff(double[] x, double[] y, double cq = 0, int q = 0){ // yields the coefficients for a quadratic spline

		if(!(x.Length >2)) throw new Exception("qcoeff --> Insufficient amount of points"); 

		double[] c = new double[x.Length -1];
		c[q] = cq;

		for(int i = q; i<c.Length -1;i++){
			
			double dx = x[i+1]-x[i];
     	  		double dy = y[i+1]-y[i];

			double dx2 = x[i+2]-x[i+1];
     	  		double dy2 = y[i+2]-y[i+1];

			c[i+1] = (dy2/dx2 - dy/dx - c[i]*dx)/dx2;

		}

		for(int j = q ; j>0 ; j--){
			
			double dx = x[j+1]-x[j];
     	  		double dy = y[j+1]-y[j];

			double dx2 = x[j]-x[j-1];
     	  		double dy2 = y[j]-y[j-1];

			c[j-1] = (dy/dx - dy2/dx2 - c[j]*dx)/dx2;
		}

		return c;
	
}//qcoeff

	public static double[] qinterp(double[] x, double[] y, double[] z, double cq = 0, int q = 0){
		
		if(!(x[0] <= z[0] && z[z.Length - 1] <= x[x.Length-1])) throw new Exception("z not bounded by x"); 
		
		/////////// Building the c coefficients

		double[] c = qcoeff(x,y,cq,q);

		if(cq == 0 && q == 0){

			double[] v = qcoeff(x,y,0.5*c[x.Length-2],x.Length-2);
			for(int m =0; m<v.Length;m++) c[m] = v[m];

		}

		/////////// Building the c coefficients

		double[] Quad = new double[z.Length];
		int i = binsearch(x,z[0]); // binary search --> finds which interval z[0] lies in
		
		for(int j = 0; j<z.Length; j++){
			
			while(!(z[j] <= x[i+1])) i+=1; // z is assumed ordered
			
        		double dx=x[i+1]-x[i]; 
     	  		double dy=y[i+1]-y[i];

			if(!(dx>0)) throw new Exception("x not sorted");

        		Quad[j] = y[i]+(dy/dx + c[i]*(z[j] - x[i+1]))*(z[j]-x[i]); // If dy and dx are (int) with dx>dy will falsly return y[i]! --> make sure array is doubles!!
		}
		return Quad;

}//qinterp

	public static double[] qinterpInteg(double[] x, double[] y, double[] z, double cq = 0, int q = 0){ // qinterp has an arbitrary choice of one c coefficient. 
												      // "double cq" indicate the value and "double q" indicate which of the (x.Length -1) nr.
												      // the coefficient is fixed for --> the rest are then uniquely determined.
		
		if(!(x[0] <= z[0] && z[z.Length - 1] <= x[x.Length-1])) throw new Exception("z not bounded by x"); 

		/////////// Building the c coefficients

		double[] c = qcoeff(x,y,cq,q);

		if(cq == 0 && q == 0){

			double[] v = qcoeff(x,y,0.5*c[x.Length-2],x.Length-2);
			for(int m =0; m<v.Length;m++) c[m] = v[m];

		}

		/////////// Building the c coefficients

		/////////// Integration routine --> Similar to linterpInteg

		double[] Int = new double[z.Length];

		int i = binsearch(x,z[0]); // binary search of x --> we need to know where z[0] is

		int j = 0;
		for(int k = 0; k<z.Length; k++){

			while(!(z[k] <= x[i+1])){

				i+=1; // z is assumed ordered --> if z[k] does not lie in given interval go to the next.
			}
			double dx;
			while(j <i){ // while interval j is below i run "Int[n] +=" where k <= n.
			
				dx = (x[j+1] - x[j]);				
				double Interval = (0.5*(y[j] + y[j+1]) - c[j]*dx*dx/6)*dx;

				for(int n = k; n<z.Length; n++){

					Int[n] += Interval; // Analytic calculation. Integrates from x[j] to x[i] and adds to all subsequent array-elements of Int
						          //For k=0 we have x[j=0] to x[i] <= z[0] and for k=1 we have x[j=i] to x[i++] <= z[1] (x[i++] is the biggest x element below z[1])

				}

				j += 1; // proceed to next interval.
			}
			dx = (x[j+1] - x[j]);
     	  		double dy = y[j+1]-y[j];
			double dz = z[k]-x[j];

			if(!(dx>0)) throw new Exception("x not sorted");
			
			Int[k] += (y[j] + 0.5*(dy/dx + c[j]*(2*dz/3 - dx))*dz)*dz; // Analytic calculation. Adds the integral from x[i] to z[k], where x[i] is the biggest element in x below z[k].
		}

		return Int; // Fingers crossed
		
		/////////// Integration routine --> Similar to linterpInteg

}//qinterpInteg

	public static double[] qinterpDiff(double[] x, double[] y, double[] z, double cq = 0, int q = 0){

		if(!(x[0] <= z[0] && z[z.Length - 1] <= x[x.Length-1])) throw new Exception("z not bounded by x"); 

		/////////// Building the c coefficients

		double[] c = qcoeff(x,y,cq,q);

		if(cq == 0 && q == 0){

			double[] v = qcoeff(x,y,0.5*c[x.Length-2],x.Length-2);
			for(int m =0; m<v.Length;m++) c[m] = v[m];

		}

		/////////// Building the c coefficients

		if(!(x[0] <= z[0] && z[z.Length - 1] <= x[x.Length-1])) throw new Exception("z not bounded by x");
		
		double[] Diff = new double[z.Length];
		int i = binsearch(x,z[0]); // binary search --> finds which interval z[0] lies in
		
		for(int j = 0; j<z.Length; j++){
			
			while(!(z[j] <= x[i+1])) i+=1; // z is assumed ordered
			
        		double dx=x[i+1]-x[i]; 
     	  		double dy=y[i+1]-y[i];

			if(!(dx>0)) throw new Exception("x not sorted");

        		Diff[j] = dy/dx + c[i]*(2*z[j]-x[i]-x[i+1]);
		}
		return Diff;

}//qinterpDiff
	
}//Spline