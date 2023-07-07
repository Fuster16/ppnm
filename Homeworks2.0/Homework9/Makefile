All: HiggsFit.svg Out.txt

HiggsFit.svg: Fit.txt higgs.data.txt Makefile
	echo '\
	set term svg;\
	set out "$@";\
	set title "Higgs Fit";\
	set xlabel "Energy";\
	set ylabel "Signal";\
	plot [][]\
	 "higgs.data.txt" using 1:2:3 with errorbars pointsize 0.5 title "Data"\
	,"Fit.txt" using 1:2 with lines title "Fit"\
	'|gnuplot

Fit.txt : fit.exe higgs.data.txt
	mono fit.exe < higgs.data.txt > Fit.txt 

fit.exe : fit.cs math.dll
	mcs fit.cs -reference:math.dll -target:exe -out:fit.exe

Out.txt : main.exe higgs.data.txt Homework9.txt
	mono main.exe < higgs.data.txt > Out.txt 
	cat Out.txt

main.exe : main.cs math.dll
	mcs main.cs -reference:math.dll -target:exe -out:main.exe

math.dll : minimize.cs matrices.cs vectors.cs calcs.cs
	mcs $^ -target:library -out:math.dll

clean : 
	rm -f Out.txt main.exe 
