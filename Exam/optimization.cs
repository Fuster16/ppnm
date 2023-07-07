using System;
using static System.Math;

public static class optimization{

	public static (vector, int) PSO(

		Func<vector,double> f, // Function to optimize

		vector a, vector b, // Vectors defining initial n-cube interval

		optimization.genlist<int> xlist = null, optimization.genlist<matrix> ylist = null, // PSO can fill initial genlists with global best index and personal bests vectors

		int N = 20, 
		int MaxItt = 100, 
		double acc = 0.1,
		double w = 0.72
		
		){

		int n = a.size;
		matrix Mx = new matrix(n,N);
		matrix Mv = new matrix(n,N);
		matrix Mp;

		vector fp = new vector(N); // gets it's own vector --> values are expected to be called often and changed less than often
		int k_min = 0;
		int count = MaxItt;

		//// Initial values are set
		vector ab = b - a;
		var rnd = new Random();
		Func<double,double> U = t => t*rnd.NextDouble();

		for(int i = 0; i<N; i++){
			Mx[i] = a + ab.map(U);
			Mv[i] = -0.5*ab + ab.map(U);

			fp[i] = f(Mx[i]); 
			if(fp[i] < fp[k_min]){ 
				k_min = i;
			}
		}
		Mp = Mx.copy();

		for(int i = 1; i <= MaxItt; i++){
			
			//// Fills lists
			if(xlist != null && ylist != null){ 
				xlist.add(k_min);
				ylist.add(Mp);
			}
			
			//// Checks for convergence after i'th loop
			bool convergence = true;
			for(int j = 0; j < N; j++){
				if( (Mp[j]  - Mp[k_min]).norm() > acc ){ // If any j'th best point is outside of an n-sphere of radius acc from the global best --> convergence is false
					convergence = false; 
					break;
				} 
			}
			if(convergence == true){ 
				count = i; 
				break;
			}
			
			//// Particle updates
			for(int j = 0; j<N; j++){ // The position matrix is updated coloum-wise together with function evaluations
				Mx[j] = Mx[j] + Mv[j];

				if( f(Mx[j]) < fp[j] ){ // updates j'th best positions
					Mp[j] = Mx[j];
					if( f(Mx[j]) < fp[k_min] ){ k_min = j;} // updates global best position index
				}
				
				Mv[j] = w*Mv[j] + rnd.NextDouble()*(Mp[j] - Mx[j]) + rnd.NextDouble()*(Mp[k_min] - Mx[j]); // updates j'th velocity
				
			}

		}

		return (Mp[k_min], count);

} // PSO

	public static (vector, int) BBPSO(Func<vector,double> f, vector a, vector b, genlist<int> xlist=null, genlist<matrix> ylist=null, int N = 20, int MaxItt = 100, double acc = 0.1){

		int n = a.size;
		matrix Mx = new matrix(n,N);
		matrix Mp;

		vector fp = new vector(N); // gets it's own vector --> values are expected to be called often and changed less than often
		int k_min = 0;
		int count = MaxItt;


		//// Initial values are set
		vector ab = b - a;
		var rnd = new Random();
		Func<double,double> U = t1 => t1*rnd.NextDouble();
		Func<double,double> Normal = t2 => t2*Sqrt(-2*Log(1 - rnd.NextDouble()))*Cos(2*PI*rnd.NextDouble()); // The Box-Muller transform. Here it is important that the logarithm takes "1 - rnd.NextDouble()", since rnd.NextDouble() is a uniform distribution on [0,1), NOT (0,1] which the Box-Muller transform requires
		Func<double,double> BB = t3 => Normal(Abs(t3)); // to be used in the updates

		for(int i = 0; i<N; i++){
			Mx[i] = a + ab.map(U);

			fp[i] = f(Mx[i]); if(fp[i] < fp[k_min]){ k_min = i;}
		}
		Mp = Mx.copy();

		for(int i = 1; i <= MaxItt; i++){

			//// Fills lists
			if(xlist != null && ylist != null){ 
				xlist.add(k_min); 
				ylist.add(Mp);
			}

			//// Checks for convergence after i'th loop
			bool convergence = true;
			for(int j = 0; j < N; j++){
				if( (Mp[j]  - Mp[k_min]).norm() > acc ){ convergence = false; break;} // If any j'th best point is outside of an n-sphere of radius acc from the global best --> convergence is false
			}
			if(convergence == true){ count = i; break;}

			//// Particle updates
			for(int j = 0; j<N; j++){ // The position matrix is updated coloum-wise together with function evaluations

				Mx[j] = 0.5*(Mp[k_min] + Mp[j]) + (Mp[k_min] - Mp[j]).map(BB); // Here x_{ij} = (p_{ij} + g_i)/2 + |p_{ij} - g_i|*N(0,1) as i have deduced from the book and various sources

				if( f(Mx[j]) < fp[j] ){ // updates j'th best positions
					Mp[j] = Mx[j];
					if( f(Mx[j]) < fp[k_min] ){ k_min = j;} // updates global best position index
				}
				
			}

		}

		return (Mp[k_min], count);

} // BBPSO

	public static (vector, int) QuasiPSO(

		Func<vector,double> f, // Function to optimize

		vector a, vector b, // Vectors defining initial n-cube interval

		vector xprimes, vector vprimes, // a set of primes used for the Halton

		genlist<int> xlist=null, genlist<matrix> ylist=null, // PSO can fill initial genlists with global best index and personal bests vectors

		int N = 20, // particle swarm size
		int MaxItt = 100, // maximum nr. of itterations
		double acc = 0.1, // connected to the convergence criteria
		double w = 0.72 // damping parameter
		){

		int n = a.size;
		matrix Mx = new matrix(n,N);
		matrix Mv = new matrix(n,N);
		matrix Mp;

		vector fp = new vector(N); // gets it's own vector --> values are expected to be called often and changed less than often
		int k_min = 0;
		int count = MaxItt;

		//// Initial values are set

		vector ab = b - a;
		var rnd = new Random(); // still needed for stochastic process in update
		for(int j = 0; j < N; j++){

			vector H1 = Halton(j+1,xprimes);
			vector H2 = Halton(j+1,vprimes);

			for(int i = 0; i < n; i++){
				Mx[i,j] = a[i] + ab[i]*H1[i];
				Mv[i,j] = -0.5*ab[i] + ab[i]*H2[i];
			}

			fp[j] = f(Mx[j]); if(fp[j] < fp[k_min]){ k_min = j;}
		}

		Mp = Mx.copy();

		for(int i = 1; i <= MaxItt; i++){
	
			//// Fills lists
			if(xlist != null && ylist != null){ 
				xlist.add(k_min); 
				ylist.add(Mp);
			}
			
			//// Checks for convergence after i'th loop
			bool convergence = true;
			for(int j = 0; j < N; j++){
				if( (Mp[j]  - Mp[k_min]).norm() > acc ){ convergence = false; break;} // If any j'th best point is outside of an n-sphere of radius acc from the global best --> convergence is false
			}
			if(convergence == true){ count = i; break;}
			
			//// Particle updates
			for(int j = 0; j<N; j++){ // The position matrix is updated coloum-wise together with function evaluations
				Mx[j] += Mv[j];

				if( f(Mx[j]) < fp[j] ){ // updates j'th best positions
					Mp[j] = Mx[j];
					if( f(Mx[j]) < fp[k_min] ){ k_min = j;} // updates global best position index
				}
				
				Mv[j] = w*Mv[j] + rnd.NextDouble()*(Mp[j] - Mx[j]) + rnd.NextDouble()*(Mp[k_min] - Mx[j]); // updates j'th velocity
				
			}

		}

		return (Mp[k_min], count);

} // QuasiPSO

	public static double corput(int n, int b){ // Takes n (in base 10) and b as desired base for corput
	
		double q = 0, bk = 1.0/b; // The sum (q) starts at 0 and bk is 1/b for the first iteration (k=1)

		while(n>0){ // continues 
			
			q += (n % b)*bk; // % is the modulo operator (It takes the highest natural number j where n > j*b --> The modulo is then n - j*b). 
					 // The result inside the parentheses gives, itteratively, the base representation of the given number (we give a natural number n in base 10)
			n /= b; 	 // division between integers ---> Goes to next number in base (ignores remainder)
			bk /= b;	 // Updates bk for next number in base.
		
		}
		
		return q;

} // corput

	public static vector Halton(int n, vector b){ // Halton which takes and outputs vectors

		vector H = new vector(b.size);

		for(int i = 0; i < b.size; i++){
			
			H[i] = corput(n,(int)b[i]);

		}

		return H;

} // Halton 

	public class genlist<T>{               /* "T" is the type parameter */
   		
		private T[] data;                   /* we keep items in the array "data" */
   		public int size => data.Length;     /* I think that "size" sounds better than "Length" */
   		//public T this[int i] => data[i];     /* we get items from our list using [i] notation */
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
		}

}//genlist<T> taken from "generics in C-sharp" from course notes - Documentation was included.


}// optimization