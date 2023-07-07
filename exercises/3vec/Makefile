Out.txt : main.exe
	mono main.exe > Out.txt 
	cat Out.txt

main.exe : main.cs vec.dll
	mcs main.cs -reference:vec.dll -target:exe -out:main.exe

vec.dll : vec.cs
	mcs vec.cs -target:library -out:vec.dll

clean : 
	rm -f Out.txt main.exe 
