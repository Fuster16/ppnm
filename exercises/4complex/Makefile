HOME = /home/jensr/repos
DIR = $(HOME)/ppnm/matlib/complex

Out.txt : main.exe
	mono main.exe > Out.txt 
	cat Out.txt

main.exe : main.cs cmath.dll
	mcs main.cs -reference:cmath.dll -target:exe -out:main.exe

cmath.dll : $(DIR)/cmath.cs $(DIR)/complex.cs
	mcs $^ -target:library -out:./cmath.dll

clean : 
	rm -f Out.txt main.exe 
