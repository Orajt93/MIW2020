import pandas as pd
import numpy as np
import scipy.stats as st
import missingno as msno
import sqlite3

# Wizualizacja
import matplotlib.pyplot as plt
import seaborn as sns

# Setup
sns.set()

vg_data = pd.read_csv(
    'vgsales.csv',
    index_col=False
)
#usuwam niepelne dane
vg_data=vg_data.dropna(thresh=2)

#Rozmiar
print("Rozmiar")
print()
print()
print(f'Ilość wierszy: {vg_data.shape[0]}')
print(f'Ilość kolumn: {vg_data.shape[1]}')
print()
print()
print("Inorfmacje o zbiorze danych")
print(vg_data.info())
print()
print()
print("Podstawowe statystyki")
print(vg_data.describe())

print()
print()
print("Poczatek zbioru danych")
print(vg_data.head())

print()
print()
print("Kurtoza:")
print(vg_data.kurt())

print()
print()
print("Skosnosc:")
print(vg_data.skew())


print()
print()
grouped=vg_data.groupby('Year')[['Name']].count().sort_values('Year', ascending=True).plot()
plt.title("Ilosc gier w rankingu z podzialem na lata")
plt.show()
grouped=vg_data.groupby('Publisher')[['Name']].count().sort_values('Name', ascending=False).head(10).plot(y=['Name'], kind="bar")
plt.title("TOP 10: Najlepsze wydawnictwa z rankingu pod wzgledem ilosci tytulow")
plt.show()
grouped1=vg_data.groupby('Publisher')[['Global_Sales']].sum().sort_values('Global_Sales', ascending=False).head(10).plot(y=['Global_Sales'], kind="bar")
plt.title("TOP 10: Najlepsze wydawnictwa z rankingu pod wzgledem sprzedanych egzemplarzy")
plt.show()








