using System;
using static System.Console;
using static System.Math;
using System.Threading;

public static class main{
	
	public static void Main(string[] args){ // The main executable takes a string denoted "args" as argument from the command line

		int nthreads = 1, nterms = (int)1e8; // default values (if not getting any from the cmd-line)


		///// Reads from cmd-line

		foreach(var arg in args){ // loops over arguments in "args" from cmd-line

			var words = arg.Split(':'); // each argument is split by ":" (returns a string[] with indices being the between the splits)

			if(words[0] == "-threads"){ nthreads = int.Parse(words[1]);} 	// if the first index is "-threads", nthreads is set to new value
			if(words[0] == "-terms"){ nterms = (int)float.Parse(words[1]);} // nterms are given in scientific notation. 
											// Thus it is parsed as a float and implicitely converted to integer
		}
		///// Reads from cmd-line

		///// division of thread intervals 

		data[] x = new data[nthreads]; // 1 obj per. Thread --> held in array of type data

		for(int i=0; i < nthreads; i++){ // loops over i'th data obj associated with i'th thread

			x[i]= new data(); // initialize 
			x[i].a = 1 + nterms/nthreads*i; 	// nterms and nthreads are of type integer --> always rounded down. Thus. If nterms divides exactly by nthreads, everything is fine.
			x[i].b = 1 + nterms/nthreads*(i+1); 	// If nterms isn't divisible, there might be lost 1,2, ... , n < nthreads values for the very last thread.
								// The way the for-loop in "harm.cs" is set up works with the above "overlapping" intervals
		}

		x[x.Length-1].b = 1 + nterms; // the end value of the last interval is set manually

		///// division of thread intervals 


		///// Using Threading

		var threads = new Thread[nthreads]; // creates array of Threads?? Oki

		for(int i=0; i < nthreads; i++) { // loop over nr. Of threads to be created

			threads[i] = new Thread(harm.harmonic); // creates new thread to run the harmonic function
			threads[i].Start(x[i]); // starts newly created thread with associated interval through x

		}

		double total = 0;

		for(int i=0; i < nthreads; i++){ // joins threads to master and adds partial harmonic sums to total

			threads[i].Join();
			total += x[i].sum;
		}

		///// Using Threading

		WriteLine($"{total} with {nthreads} threads");

} //Main
	
} //main
