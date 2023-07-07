using System;
using static System.Console;
using static System.Math;

public static class main1{
	
	public static int Main(string[] args){ // similar to main1 --> We "read" from the command line. Syntax: "mono main.exe -input:input.txt -output:out.txt"

		string infile=null,outfile=null;

		foreach(var arg in args){ // sets infile and outfile string values to the given data sheets.

			var words=arg.Split(':'); // Splits the command-line string[] at ":"

			if(words[0]=="-input") infile = words[1]; // If "-input" is given, then the infile name must be words[1]
			if(words[0]=="-output") outfile = words[1]; // If "-output" is given, then the infile name must be words[1]

		}

		if( infile==null || outfile==null) {

			Error.WriteLine("wrong filename argument"); // error message if no filenames are given

			return 1; // Main must be of "int" return value
		}

		char[] split_delimiters = {' ','\t','\n',','}; // syntax of the given numbers are of blanks, tabs and newlines
		var split_options = StringSplitOptions.RemoveEmptyEntries;


		var instream =new System.IO.StreamReader(infile); // opens instream
		var outstream=new System.IO.StreamWriter(outfile,append:false); // opens outstream

		for(string line=instream.ReadLine();line!=null;line=instream.ReadLine()){ // similar to main2. Block of code is executed while there are lines in the standard inputstream

			var numbers = line.Split(split_delimiters,split_options); // the numbers (for the given line) are retreived into a string[]
			
			foreach(var number in numbers){

				double x=double.Parse(number); 
				outstream.WriteLine($"{x} {Sin(x)} {Cos(x)}");

			}

        	}

		instream.Close(); // Important
		outstream.Close(); // Important

		return 0; // Main must return some integer.

} //Main
	
} //main
