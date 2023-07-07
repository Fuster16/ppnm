
all: dr.svg rmax.svg Eigenfuncs.svg Out.txt

Out.txt: main.exe Makefile
	mono main.exe -rmax:10 -dr:0.1 > Out.txt 
	cat Out.txt

dr.svg: B1dr.txt Makefile
	echo '\
	set term svg;\
	set out "$@";\
	set title "Hydrogen, matrix, E_0 convergence with dr";\
	set xlabel "dr, Bohr radius";\
	set ylabel "E_{0}, Hartree";\
	plot [0:1.1][-0.6:-0.4]\
	 "$<" using 3:1 with linespoints pointtype 5 title "calculated"\
	,-0.5 with lines title "exact"\
	'|gnuplot

rmax.svg: B1rmax.txt Makefile
	echo '\
	set term svg;\
	set out "$@";\
	set title "Hydrogen, matrix, E_0 convergence with rmax";\
	set xlabel "rmax, Bohr radius";\
	set ylabel "E_{0}, Hartree";\
	plot [0:11][-0.6:0]\
	 "$<" using 2:1 with linespoints pointtype 5 title "calculated"\
	,-0.5 with lines title "exact"\
	'|gnuplot

Eigenfuncs.svg: B2.txt Makefile
	echo '\
	set term svg;\
	set out "$@";\
	set xlabel "r, Bohr radius";\
	set ylabel "f(r), arb. u";\
	set title "Hydrogen atom radial wave-functions for n=1,2,3 and l=0";\
	plot [0:30][-2:2]\
	 "$<" using 1:2 with points pointtype 7 pointsize 0.5 title "Nummerical n=1"\
	,"$<" using 1:3 with points pointtype 7 pointsize 0.5 title "Nummerical n=2"\
	,"$<" using 1:4 with points pointtype 7 pointsize 0.5 title "Nummerical n=3"\
	,"$<" using 1:5 with lines title "Analytical n=1"\
	,"$<" using 1:6 with lines title "Analytical n=2"\
	,"$<" using 1:7 with lines title "Analytical n=3"\
	'| gnuplot

B1dr.txt: B1.exe Makefile
	>$@
	for dr in 0.1 0.2 0.3 0.4 0.5 0.6 0.7 0.8 0.9 1; do \
	echo $$dr; mono B1.exe -rmax:10 -dr:$$dr >>$@ \
	; done

B1rmax.txt: B1.exe Makefile
	>$@
	for R in 1 2 3 4 5 6 7 8 9 10; do \
	echo $$R; mono B1.exe -rmax:$$R -dr:0.1 >>$@ \
	; done

B2.txt: B2.exe Makefile
	mono $< -rmax:30 -dr:0.1 > $@

B1.exe: B1.cs math.dll
	mcs B1.cs -reference:math.dll -target:exe -out:B1.exe

B2.exe: B2.cs math.dll
	mcs B2.cs -reference:math.dll -target:exe -out:B2.exe

main.exe: main.cs math.dll
	mcs main.cs -reference:math.dll -target:exe -out:main.exe

math.dll: vectors.cs matrices.cs jacobi.cs Hatom.cs calcs.cs
	mcs vectors.cs matrices.cs jacobi.cs Hatom.cs calcs.cs -target:library -out:math.dll

clean: 
	rm -f Out.txt main.exe 
