All: Erf.svg ErfRes.svg Out.txt

Erf.svg : erf.txt Makefile
	echo '\
	set term svg;\
	set out "$@";\
	set title "Error function comparission ";\
	set xlabel "x data";\
	set ylabel "y data";\
	plot [-0.5:4][-0.5:1.5]\
	 "erf.txt" index 0 using 1:2 pointsize 1 title "Wikipedia"\
	 ,"erf.txt" index 0 using 1:3 pointsize 1 title "Integrate method"\
	 ,"erf.txt" index 0 using 1:4 pointsize 0.5 title "Exercise method"\
	'|gnuplot

ErfRes.svg : erf.txt Makefile
	echo '\
	set term svg;\
	set out "$@";\
	set title "Residuals comparrison";\
	set xlabel "x data";\
	set ylabel "y data";\
	plot [-0.5:4][-0.01:0.01]\
	 "erf.txt" index 0 using 1:5 pointsize 1 title "Integrate method"\
	 ,"erf.txt" index 0 using 1:6 pointsize 1 title "Exercise method"\
	'|gnuplot

erf.txt : erf.exe 
	mono erf.exe < Erf_wiki.txt > erf.txt

erf.exe : Erf.cs math.dll
	mcs Erf.cs -reference:math.dll -target:exe -out:erf.exe

Out.txt : main.exe Homework6.txt Makefile
	mono main.exe > Out.txt
	cat Out.txt

main.exe : main.cs math.dll
	mcs main.cs -reference:math.dll -target:exe -out:main.exe

math.dll : adaptive.cs vectors.cs matrices.cs
	mcs adaptive.cs vectors.cs matrices.cs -target:library -out:math.dll

clean : 
	rm -f Out.txt main.exe 
