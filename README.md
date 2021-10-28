# larpApp
larpApp

#Założenia projektu:
Głównym założeniem pracy jest zaprojektowanie aplikacji na telefony mobilne wspierającej gry terenowe typu LARP (Live Action Role Play). Aplikacja będzie wyposażona w następujące funkcjonalności:
• Spis użytkowników z przydzielonymi unikalnymi indeksami na serwerze;
• Chat – indywidualny lub grupowy;
• Lista znajomych;
• Udostępnianie użytkownikom dodatkowych informacji za pomocą kodów QR – hermetyzacja tych informacji – tylko użytkownik z odpowiednią umiejętnością, może odczytać poprawnie kod;
• Możliwość organizowania wydarzeń w oparciu o geolokalizację i Plugin do MapGoogle;
• Możliwość przechowywania kart postaci typu ttRPG, charakterystyk i ekwipunku;
• W panelu rozpoczętego wydarzenia możliwość proszenia o weryfikujący umiejętności uczestnika wirtualny rzut kośćmi.

#Wykorzystane technologie
Aplikacja jest napisana w języku C# z elementami XAML, z wykorzystaniem framework’u Xamarin Forms umożliwiającego jednoczesne programowanie dla trzech różnych platform z pośród których wybrałem:
• Android
• IOS
Aplikacja będzie również wykorzystywać bazę danych Mongo oraz Realm, które będą synchronizowane za pomocą SDK od Mongo. Wykorzystywany jest plugin do map Google umożliwiający lokalizację i krecję eventów

#Jak działa Mongo i Realm?
![image](https://user-images.githubusercontent.com/66923772/139326953-235f453c-6e91-4de8-8c75-0485bc5c47b0.png)

#Na jakim etapie znajduje się projekt
![Uploading image.png…]()
