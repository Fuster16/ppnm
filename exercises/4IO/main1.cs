using System;
using static System.Console;
using static System.Math;

public static class main1{
	
	public static void Main(string[] args){ // The main executable takes a string denoted "args" as argument from the command line

		foreach(var arg in args){ // loops over arguments in "args"

			var words = arg.Split(':'); // each argument is split by ":" (returns a string[] with indices being the between the splits)

			if(words[0]=="-numbers"){ // if the first index is "-numbers" proceed

				var numbers=words[1].Split(','); // splits the numbers given in the command line at ","

				foreach(var number in numbers){ // loop over each number

					double x = double.Parse(number); // parse number from string to double
					WriteLine($"{x} {Sin(x)} {Cos(x)}"); // calculate things with numbers

				}
			}
		}
}
	
} //main
