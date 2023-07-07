using System;
using static System.Console;
using static System.Math;

public static class main{ // re-used main2 from IO exercise
	
	public static void Main(){ // Similar to main1, however the numbers are read by "ReadLine()" from the standard input

		var table = new generics.genlist<double[]>(); // use generic list to make a table (list of lists).

		char[] delimiters = {' ','\t'}; // usual syntax for input file
		var options = StringSplitOptions.RemoveEmptyEntries; // usual split options

		for(string line = ReadLine(); line!=null; line = ReadLine()){ // "ReadLine()" reading from standard input while lines are provided
									      // Block is executed for each line

			var words = line.Split(delimiters,options); // separates line by given "syntax" delimiters
			
			int n = words.Length; 
			var numbers = new double[n]; // initializes a double array of size words.Length

			for(int i=0;i<n;i++) numbers[i] = double.Parse(words[i]); // parsing text as doubles into "numbers"
			table.add(numbers); // Whole line is now on the form of "double[]" and can be added to table. 
					    // This is where generic lists shine!! We don't know beforehand how many lines are in the inputfile.

       		}

		for(int i=0;i<table.size;i++){ // 

			var numbers = table[i]; // picks out the i'th "double[]" array
			foreach(var number in numbers) Write($"{number : 0.00e+00;-0.00e+00} "); // scienctific exonetnial format. The additional format ";-0.00e+00" is such that the minus sign "-" is connected to the number
			WriteLine(); // prepares new line for next block itteration.

		}

} //Main
	
} //main
