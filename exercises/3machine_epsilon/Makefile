Out.txt : main.exe
	mono main.exe > Out.txt
	cat Out.txt

sfuns.dll : sfuns.cs
	mcs sfuns.cs -target:library -out:sfuns.dll

main.exe : main.cs sfuns.dll
	mcs main.cs -target:exe -out:main.exe -reference:sfuns.dll
