N=1e8
TIME = time --portability --append --output $@

all: Out.txt Out1.txt

Out.txt : main.exe main1.exe Makefile
	>$@
	$(TIME) mono $< -terms:$N -threads:1 >>$@
	$(TIME) mono $< -terms:$N -threads:2 >>$@
	$(TIME) mono $< -terms:$N -threads:3 >>$@
	$(TIME) mono $< -terms:$N -threads:4 >>$@
	$(TIME) mono main1.exe >>$@

Out1.txt : main1.exe Makefile
	$(TIME) mono $< >$@

main.exe : main.cs data.dll
	mcs $< -reference:data.dll -target:exe -out:$@

main1.exe : main1.cs
	mcs $< -target:exe -out:$@

data.dll : data.cs harm.cs
	mcs $^ -target:library -out:$@

clean : 
	rm -f Out.txt main.exe 
