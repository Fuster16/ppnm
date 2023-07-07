using System;
using static System.Console;
using static System.Math;
using System.IO;

public static class ODE{

	public static vector Y(double x, vector y, Func<double,vector,double> ODE){
		
		vector y_new = new vector(y.size);

		for(int i = 0;i<y.size-1 ; i++){
			y_new[i] = y[i+1];
		}

		y_new[y.size-1] = ODE(x,y); // The ODE is a function Y(t,X), where X is the vector comprising of (x,x',x'',..., x^(k-1)).

		return y_new;
}//f

	public static (vector,vector) rkstep12(
		Func<double,vector,vector> f, // ODE expressed as it's associated first-order vector differential equation. Func<double,vector,vector> f takes (double, vector) and spits out vector.
		
		double x, // current "time" 
		vector y, // current "space", "velocity", "acceleration"...
		double h // step-size

		){

		vector k0 = f(x,y); // Initial value yields initial change
		vector k1 = f(x + h/2, y + k0*(h/2)); //half_step change given initial change

		vector yh = y + k1*h; // take full step (from initial value) according to mid-step info
		vector er = (k1-k0)*h; // Error. rkstep12 is of order 2. Comparison with Euler step (of order 1) yields gained local precision.

		return (yh,er); // returns the step together with local error in step.

}//rkstep12

	public class genlist<T>{               /* "T" is the type parameter */
   		
		private T[] data;                   /* we keep items in the array "data" */
   		public int size => data.Length;     /* I think that "size" sounds better than "Length" */
   		// public T this[int i] => data[i];     /* we get items from our list using [i] notation */
   		public genlist(){ data = new T[0]; }  /* constructor creates empty list */
   		
		public genlist(int n){data = new T[n];} // added additional constructor

		public T this[int i]{ // A combined get and setter for the list

			get => data[i]; // returns the i'th element when read
			set => data[i] = value; // sets value if statement

		}

		public void add(T item){              /* add item of the type "T" to the list */
      			T[] newdata = new T[size+1];   /* we need a larger array (inefective but uses minimal memory) */
      			Array.Copy(data,newdata,size); /* here you do O(size) operations */
      			newdata[size]=item;            /* add the item at the end of the list */
      			data=newdata;                  /* old data should be garbage collected, no worry here */

}//genlist<T>

}//genlist taken from "generics in C-sharp" from course notes - Documentation was included.

	public static (genlist<double>,genlist<vector>) driver12( // Stores solution vectors as it goes.

		Func<double,vector,vector> f, // The ODE

		double a, // initial "time"
		vector ya, // initial "vector". k-order ODE's need k-initial conditions for a unique solution
		double b, // End "time" of routine

		double h = 0.01, // step-size
		double acc = 0.01, // absolute precision
		double eps = 0.01 // relative precision 

		){

		if(a>b) throw new ArgumentException("driver: a>b");

		double x = a; vector y=ya.copy(); // preparing for driver

		var xlist=new genlist<double>(); xlist.add(x); // Makes generic lists and adds startvalue
		var ylist=new genlist<vector>(); ylist.add(y);

		do{ // while loop

			if(x>=b) return (xlist, ylist); // while loop stops after return.
			if(x+h>b) h = b-x; // makes step_size such that we end at b, if next step surpasses
			
			var (yh, erv) = rkstep12(f,x,y,h); // stepper for current driver

			double tol = (acc + eps*yh.norm())*Sqrt(h/(b-a)); // calculates tolerance 
			double err = erv.norm();

			if(err<= tol){ // if error is below tolerance, the step is taken
				x+= h; y = yh;
				xlist.add(x);
				ylist.add(y);
			}

			h*= Min( Pow(tol/err , 0.25) ,2 );

		}while(true);
}//driver12

public static (vector,vector) rkstepXY( // A general Runge-Kutta stepper

		Func<double,vector,vector> f, // ODE expressed as it's associated first-order vector differential equation. Func<double,vector,vector> f takes (double, vector) and spits out vector.
		double x, // current "time" 
		vector y, // current "space", "velocity", "acceleration"...
		double h, // step-size

		matrix A, // a general set of runge-kutta a_ij coeffecients
		vector b1, // a general set of b_i coeffecients
		vector c, // a general set of c_i coeffecients

		vector b0 // a general set of b0_i coefficients. This set is for the error estimation by embedded method.

		){

		var Klist = new genlist<vector>(b1.size); // The k-vectors are kept in a list

		vector k = f(x,y); // redundant

		Klist[0] = k; 
		
		for(int i = 0; i<b1.size-1;i++){
			
			vector ks = y/h;

			for(int j = 0; j<=i;j++){
				
				ks += A[i,j]*Klist[i];

			}

			k = f(x + h*c[i+1], h*ks);  // redundant?
			Klist[i+1] = k;

		}

		vector Sum_b = new vector(y.size);
		vector Sum_b0 = new vector(y.size);

		for(int n = 0;n<b1.size; n++){

			Sum_b += b1[n]*Klist[n];
			Sum_b0 += b0[n]*Klist[n];
		}

		vector yh = y + Sum_b*h; // take full step (from initial value) according to mid-step info

		vector er = (Sum_b-Sum_b0)*h; // Error. rkstep12 is of order 2. Comparison with Euler step (of order 1) yields gained local precision.

		return (yh,er); // returns the step together with local error in step.

}//rkstepXY

	public static (genlist<double>, genlist<vector>) driverXY( // A general Runge-Kutta driver --> is the driver12 if no values are changed.

		Func<double,vector,vector> f, // The ODE

		double a, // initial time
		vector ya, // initial "vector". k-order ODE's need k-initial conditions for a unique solution
		double b, // End time of routine

		matrix A = null, // a general set of runge-kutta a_ij coeffecients
		vector b1 = null, // a general set of b_i coeffecients
		vector c = null, // a general set of c_i coeffecients

		vector b0 = null, // a general set of b0_i coefficients. This set is for the error estimation by embedded method.

		double h = 0.01, // step-size
		double acc = 0.01, // absolute precision
		double eps = 0.01 // relative precision 

		){

		if(A == null){
			
			A = new matrix("0.5"); // rkstep12 A_matrix
			b1 = new vector(0,1); // rkstep12 b_i coeffecients
			c = new vector(0,0.5); // rkstep12 c_i coeffecients

			b0 = new vector(1,0); // rkstep12 b0_i coefficients.

		}

		if(a>b) throw new ArgumentException("driver: a>b");

		if(c.size != b1.size || b0.size != c.size) throw new ArgumentException("rkstepXY: Bad coefficient lengths");
		if(A.size1 != c.size - 1) throw new ArgumentException("rkstepXY: Bad A dont fit coefficient lengths");
		

		double x=a; vector y=ya.copy(); // preparing for driver

		var xlist=new genlist<double>(); xlist.add(x); // Makes generic lists and adds startvalue
		var ylist=new genlist<vector>(); ylist.add(y);

		do{ // while loop

			if(x>=b) return (xlist, ylist); // while loop stops after return.
			if(x+h>b) h = b-x; // makes step_size such that we end at b, if next step surpasses
			
			var (yh, erv) = rkstepXY(f,x,y,h,A,b1,c,b0); // stepper for current driver

			double tol = (acc + eps*yh.norm())*Sqrt(h/(b-a)); // calculates tolerance 
			double err = erv.norm();

			if(err<= tol){ // if error is below tolerance, the step is taken
				x+= h; y = yh;
				xlist.add(x);
				ylist.add(y);
			}

			h*= Min( Pow(tol/err , 0.25) ,2 ); // err might be well below tol (if locally smooth) --> times 2 is set as highest multiplier however

		}while(true);
}//driverXY

	public static vector driver( // A general Runge-Kutta driver --> is the driver12 if no values are changed.

		Func<double,vector,vector> f, // The ODE

		double a, // initial time
		vector ya, // initial "vector". k-order ODE's need k-initial conditions for a unique solution
		double b, // End time of routine

		genlist<double> xlist=null, genlist<vector> ylist=null, // improved driver can fill initial lists if given

		matrix A = null, // a general set of runge-kutta a_ij coeffecients
		vector b1 = null, // a general set of b_i coeffecients
		vector c = null, // a general set of c_i coeffecients

		vector b0 = null, // a general set of b0_i coefficients. This set is for the error estimation by embedded method.

		double h = 0.01, // step-size
		double acc = 0.01, // absolute precision
		double eps = 0.01 // relative precision 

		){

		if(A == null){
			
			A = new matrix("0.5"); // rkstep12 A_matrix
			b1 = new vector(0,1); // rkstep12 b_i coeffecients
			c = new vector(0,0.5); // rkstep12 c_i coeffecients

			b0 = new vector(1,0); // rkstep12 b0_i coefficients.

		}

		if(a>b) throw new ArgumentException("driver: a>b");

		if(c.size != b1.size || b0.size != c.size) throw new ArgumentException("rkstepXY: Bad coefficient lengths");
		if(A.size1 != c.size - 1) throw new ArgumentException("rkstepXY: Bad A dont fit coefficient lengths");
		

		double x=a; vector y=ya.copy(); // preparing for driver

		vector tol = new vector(y.size); // preparing tolerance vector

		do{ // while loop

			if(x>=b) return y; // while loop stops after returning.
			if(x+h>b) h = b-x; // makes step_size such that we end at b, if next step surpasses
			
			var (yh, erv) = rkstepXY(f,x,y,h,A,b1,c,b0); // stepper for current driver

			for(int i=0;i<y.size;i++){
				tol[i]=(acc+eps*Abs(yh[i]))*Sqrt(h/(b-a)); // fills tol vector according to theory
			}

			bool ok=true;
			for(int i=0;i<y.size;i++){
				if(!(Abs(erv[i]) < tol[i])) ok=false; // if for any error, it is not below its prescribed tolerance ok is set to false
			}

			if(ok){ // if ok is true the step is taken
				x+= h; y = yh;

				if(xlist != null && ylist != null){ // fills lists if both are given as per Homework requirement
					xlist.add(x);
					ylist.add(y);
				}
			}
			
			double factor = tol[0]/Abs(erv[0]); // initial value
			for(int i=1;i<y.size;i++) factor=Min(factor,tol[i]/Abs(erv[i])); // loops through list to find min value

			h*= Min( Pow(factor , 0.25) ,2 ); // erv might be well below tol (if locally smooth) --> times 2 is set as highest multiplier however

		}while(true); 
}//driver


}//ODE
