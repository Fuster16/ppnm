using System;
using static System.Console;
using static System.Math;

public static class main2{
	
	public static void Main(){ // Similar to main1, however the numbers are read by "ReadLine()" from the standard input

		char[] split_delimiters = {' ','\t','\n'}; // syntax of the given numbers are of blanks, tabs and newlines
		var split_options = StringSplitOptions.RemoveEmptyEntries;

		for( string line = ReadLine(); line != null; line = ReadLine() ){ // block of code is executed while there are lines in the standard input

			var numbers = line.Split(split_delimiters,split_options); // the numbers (for the given line) are retreived into a string[]

			foreach(var number in numbers){

				double x = double.Parse(number); // each number is parsed to double value like in main1
				Error.WriteLine($"{x} {Sin(x)} {Cos(x)}");

                	}
        	}

} //Main
	
} //main
