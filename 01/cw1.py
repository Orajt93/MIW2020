#1
zmienna = """Lorem Ipsum jest tekstem stosowanym jako przykładowy wypełniacz
w przemyśle poligraficznym. Został po raz pierwszy użyty w XV w. przez 
nieznanego drukarza do wypełnienia tekstem próbnej książki. Pięć wieków 
później zaczął być używany przemyśle elektronicznym, pozostając praktycznie 
niezmienionym. Spopularyzował się w latach 60. XX w. wraz z publikacją arkuszy 
Letrasetu, zawierających fragmenty Lorem Ipsum, a ostatnio z zawierającym różne 
wersje Lorem Ipsum oprogramowaniem 
przeznaczonym do realizacji druków na komputerach osobistych, jak Aldus PageMaker"""

#2
print("W tekscie jest {0} liter r oraz {1} liter i".format(zmienna.count('r'), zmienna.count('i')))

#4
print(dir(zmienna))
help(zmienna.isspace)

#5
print("Marcin Jasiukiewicz"[::-1])

#6
lista=list(range(1,11))
print(lista)
index = lista.index(6)
pierwsza_lista=lista[:index]
druga_lista=lista[index:]
print(pierwsza_lista)
print(druga_lista)

#7
lista=pierwsza_lista+druga_lista
print(lista)
lista.insert(0,0)
print(lista)
kopia_listy=lista[:]
kopia_listy.sort(reverse=True);
print(lista)
print(kopia_listy)

#8
krotka_studentow=((116794, 'Marcin Jasiukiewicz'),(222222,'Kuf Drahrepus'))
print(krotka_studentow)

#9
slownik_studentow=dict(krotka_studentow)
print(slownik_studentow)
slownik_studentow={
	116794:{
		"imie": "Marcin",
		"nazwisko" : "Jasiukiewicz",
		"wiek": 26,
		"mail": "mail@gmail.com",
		"rok": 1993,
		"adres":"Dobre Miasto Kwiatowa 7"},

	222222:{
		"imie": "Kuf",
		"nazwisko" : "Drahrepus",
		"wiek": 30,
		"mail": "mail@gmail.com",
		"rok": 1990,
		"adres":"USA Maseczusets"}
	}

#10
lista_numerow=[
	"111 111 111","444 444 444", "444 444 444",
	"111 111 111", "444 444 444", "222 222 222"]
print(lista_numerow)
lista_numerow=set(lista_numerow)
print(lista_numerow)

#11
print("od 1 do 10")
for i in range(1,11):
	print(i)

#12
print("od 100 do -20")
for i in range(100,-25,-5):
	print(i)

#13
lista_studentow13 = [
	{
		"imie": "Marcin",
		"nazwisko" : "Jasiukiewicz",
		"wiek": 26,
		"mail": "mail@gmail.com",
		"rok": 1993,
		"adres":"Dobre Miasto Kwiatowa 7"},
	{
		"imie": "Kuf",
		"nazwisko" : "Drahrepus",
		"wiek": 30,
		"mail": "mail@gmail.com",
		"rok": 1990,
		"adres":"USA Maseczusets"},
	{
		"imie": "Ninja",
		"nazwisko" : "Man",
		"wiek": 15,
		"mail": "nina@gmail.com",
		"rok": 2005,
		"adres":"Japonia Niagara"},]
for osoba in lista_studentow13:
	print("Student {0} {1}, lat: {2}, rok urodzenia:{3}, mail:{4}, adres:{5}".format(
		osoba["imie"], osoba["nazwisko"],str(osoba["wiek"]),str(osoba["rok"]),osoba["mail"],osoba["adres"]))






