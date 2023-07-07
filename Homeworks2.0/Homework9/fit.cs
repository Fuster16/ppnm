using System;
using static System.Console;
using static System.Math;
using System.IO;

public static class fit{

	public static void Main(){

	////// B

	//// Fetching Higgs data

	var energy = new genlist<double>();
	var signal = new genlist<double>();
	var error  = new genlist<double>();

	var separators = new char[] {' ','\t'};
	var options = StringSplitOptions.RemoveEmptyEntries;

	do{

	        string line = ReadLine(); 	// reads line of .txt and puts into string 
	        if(line==null)break; 		// stops once there are no more lines

	        string[] words=line.Split(separators,options);

	        energy.add(double.Parse(words[0]));
	        signal.add(double.Parse(words[1]));
	        error.add(double.Parse(words[2]));

	}while(true);

	//// Doing the minimization

	Func<vector,double> D = x => Deviation(x, energy, signal, error);

	vector guess = new vector(126, 2,10);
	vector Fit = minimize.qnewton(D,guess,100, false,0.001);

	for(int i = 0; i <= 1000; i++){

		WriteLine($"{100 + i*60.0/1000} {Fit[2]/(Pow(100 + i*60.0/1000-Fit[0],2) + Fit[1]*Fit[1]/4)}");

	}

	////// B

} // Main

	public static double Deviation(vector x, genlist<double> E, genlist<double> s, genlist<double> err){

		double sum = 0;

		for(int i =0; i < E.size; i++){

			double BreitWigner = x[2]/(Pow(E[i]-x[0],2) + x[1]*x[1]/4);

			sum += Pow((BreitWigner - s[i])/err[i],2 );

		}

		return sum;

} // deviation

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
		}

}//genlist<T> taken from "generics in C-sharp" from course notes - Documentation was included.

} // Fit
