A: AfunctionPlot.svg g1Plot.svg g2Plot.svg B

B: Anti_derivative.svg derivative.svg 2derivative.svg

AfunctionPlot.svg: Out.txt Makefile
	echo '\
	set term svg;\
	set out "$@";\
	set title "g(x) = Cos(5*x-1)*Exp(-x*x)";\
	set xlabel "x";\
	set ylabel "y";\
	plot [][]\
	 "Responses.txt" index 0 using 1:2 pointsize 1 title "Table Data"\
	,"Responses.txt" index 0 using 1:3 with lines title "Fit"\
	'|gnuplot

g1Plot.svg: Out.txt Makefile
	echo '\
	set term svg;\
	set out "$@";\
	set title "g1(x) = Exp(-x*x)";\
	set xlabel "x";\
	set ylabel "y";\
	plot [][]\
	 "Responses.txt" index 0 using 1:4 pointsize 1 title "Table Data"\
	,"Responses.txt" index 0 using 1:5 with lines title "Fit"\
	'|gnuplot

g2Plot.svg: Out.txt Makefile
	echo '\
	set term svg;\
	set out "$@";\
	set title "g2(x) = 5";\
	set xlabel "x";\
	set ylabel "y";\
	plot [][]\
	 "Responses.txt" index 0 using 1:6 pointsize 1 title "Table Data"\
	,"Responses.txt" index 0 using 1:7 with lines title "Fit"\
	'|gnuplot


Anti_derivative.svg: Out.txt Makefile
	echo '\
	set term svg;\
	set out "$@";\
	set title "Integral of g(x) from 0 to x of ";\
	set xlabel "x";\
	set ylabel "y";\
	plot [][]\
	 "Responses.txt" index 1 using 1:2 pointsize 1 title "Table Data"\
	,"Responses.txt" index 1 using 1:3 with lines title "Fit"\
	'|gnuplot

derivative.svg: Out.txt Makefile
	echo '\
	set term svg;\
	set out "$@";\
	set title "first derivative of g(x)";\
	set xlabel "x";\
	set ylabel "y";\
	plot [][]\
	 "Responses.txt" index 1 using 1:4 pointsize 1 title "Table Data"\
	,"Responses.txt" index 1 using 1:5 with lines title "Fit"\
	'|gnuplot

2derivative.svg: Out.txt Makefile
	echo '\
	set term svg;\
	set out "$@";\
	set title "second derivative of g(x)";\
	set xlabel "x";\
	set ylabel "y";\
	plot [][]\
	 "Responses.txt" index 1 using 1:6 pointsize 1 title "Table Data"\
	,"Responses.txt" index 1 using 1:7 with lines title "Fit"\
	'|gnuplot

Out.txt : main.exe Homework10.txt
	mono main.exe -output:Responses.txt > Out.txt 
	cat Out.txt

main.exe : main.cs math.dll
	mcs main.cs -reference:math.dll -target:exe -out:main.exe

math.dll : ann.cs matrices.cs vectors.cs minimize2.cs adaptive.cs
	mcs $^ -target:library -out:math.dll

clean : 
	rm -f Out.txt main.exe 
