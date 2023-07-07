All: Out.txt ucirc.svg ucirc_err.svg sine.svg sine_err.svg AllB

AllB: ucirc_errB.svg sine_errB.svg ucirc_actualAB.svg sine_actualAB.svg

ucirc.svg : Makefile Out.txt
	echo '\
	set term svg;\
	set out "$@";\
	set title "Unit circle area error estimate ";\
	set xlabel "N";\
	set ylabel "Error";\
	set logscale x 10;\
	plot [1:1e8][3:3.3]\
	 "plots.txt" index 0 using 1:3 with lines title "Monte-carlo integration"\
	 ,"plots.txt" index 0 using 1:9 with lines title "Value of pi from Math.PI"\
	'|gnuplot

ucirc_err.svg : Makefile Out.txt
	echo '\
	set term svg;\
	set out "$@";\
	set title "Unit circle area error estimate ";\
	set xlabel "N";\
	set ylabel "Error";\
	set logscale x 10;\
	set logscale y 10;\
	plot [8:2e8][1e-5:2]\
	 "plots.txt" index 0 using 1:4 with lines title "uncertainty in intergration"\
	 ,"plots.txt" index 0 using 1:5 with lines title "Actual error"\
	 ,"plots.txt" index 0 using 1:2 with lines title "1/sqrt(n)"\
	'|gnuplot

sine.svg : Makefile Out.txt
	echo '\
	set term svg;\
	set out "$@";\
	set title "Error in integral of sine over 1 period";\
	set xlabel "N";\
	set ylabel "Error";\
	set logscale x 10;\
	plot [1:1e8][-0.5:0.5]\
	 "plots.txt" index 0 using 1:6 with lines title "Monte-carlo integration"\
	 ,"plots.txt" index 0 using 1:($$1)*0 with lines title "zero"\
	'|gnuplot

sine_err.svg : Makefile Out.txt
	echo '\
	set term svg;\
	set out "$@";\
	set title "Error in integral of sine over 1 period";\
	set xlabel "N";\
	set ylabel "Error";\
	set logscale x 10;\
	set logscale y 10;\
	plot [8:2e8][1e-5:10]\
	 "plots.txt" index 0 using 1:7 with lines title "uncertainty in intergration"\
	 ,"plots.txt" index 0 using 1:8 with lines title "Actual error"\
	 ,"plots.txt" index 0 using 1:2 with lines title "1/sqrt(n)"\
	'|gnuplot

ucirc_errB.svg : Makefile Out.txt
	echo '\
	set term svg;\
	set out "$@";\
	set title "Quasi-MC: Unit circle area error estimate ";\
	set xlabel "N";\
	set ylabel "Error";\
	set logscale x 10;\
	set logscale y 10;\
	plot [8:2e8][1e-5:2]\
	 "plots.txt" index 0 using 1:11 with lines title "uncertainty in intergration"\
	 ,"plots.txt" index 0 using 1:12 with lines title "Actual error"\
	 ,"plots.txt" index 0 using 1:2 with lines title "1/sqrt(n)"\
	'|gnuplot

sine_errB.svg : Makefile Out.txt
	echo '\
	set term svg;\
	set out "$@";\
	set title "Error in integral of sine over 1 period";\
	set xlabel "N";\
	set ylabel "Error";\
	set logscale x 10;\
	set logscale y 10;\
	plot [8:2e8][1e-5:10]\
	 "plots.txt" index 0 using 1:14 with lines title "uncertainty in intergration"\
	 ,"plots.txt" index 0 using 1:15 with lines title "Actual error"\
	 ,"plots.txt" index 0 using 1:2 with lines title "1/sqrt(n)"\
	'|gnuplot

ucirc_actualAB.svg : Makefile Out.txt
	echo '\
	set term svg;\
	set out "$@";\
	set title "Quasi-MC: Unit circle area error estimate ";\
	set xlabel "N";\
	set ylabel "Error";\
	set logscale x 10;\
	set logscale y 10;\
	plot [8:2e8][1e-5:2]\
	 "plots.txt" index 0 using 1:5 with lines title "Plain - Actual error"\
	 ,"plots.txt" index 0 using 1:12 with lines title "Quasi - Actual error"\
	 ,"plots.txt" index 0 using 1:2 with lines title "1/sqrt(n)"\
	'|gnuplot

sine_actualAB.svg : Makefile Out.txt
	echo '\
	set term svg;\
	set out "$@";\
	set title "Error in integral of sine over 1 period";\
	set xlabel "N";\
	set ylabel "Error";\
	set logscale x 10;\
	set logscale y 10;\
	plot [8:2e8][1e-5:10]\
	 "plots.txt" index 0 using 1:8 with lines title "Plain - Actual error"\
	 ,"plots.txt" index 0 using 1:15 with lines title "Quasi - Actual error"\
	 ,"plots.txt" index 0 using 1:2 with lines title "1/sqrt(n)"\
	'|gnuplot

Out.txt : main.exe Homework7.txt
	mono main.exe -output:plots.txt > Out.txt
	cat Out.txt

main.exe : main.cs math.dll
	mcs main.cs -reference:math.dll -target:exe -out:main.exe

math.dll : vectors.cs montecarlo.cs sfun.cs
	mcs $^ -target:library -out:$@

clean : 
	rm -f Out.txt main.exe 
