# HTTPProxyserver

1.  Kan je globale overeenkomsten vinden tussen een request en een response?
  Beide maken gebruik van de general-header.
  

2.a Hoe ziet de globale opbouw van een HTTP bericht er uit? Uit welke componenten bestaan een HTTP bericht. Licht dit toe aan de hand van een tekening met uitleg.
HTTP-request
 - Request-line: request-method URI http-version
 - general-header: 
  - Host
  - Cache-Control
  - Connection
 - request-header
  - Accept-Language: xx-xx
  - Content-length: nnnn
 - entity-header
  - Content-Language: xx-xx
  - Content-length: nnnn
  
Body

  ...
  
HTTP-response
 - Status Line: http version & response code (200, 500 etc)
 - general-header: 
 - response header
  - Content-Length: nnnn
  - Accept-Language: xx-xx
 - entity-header
   - Metainformation about body contents
   
Body

 ...
 
 --------------
 
 Request-line bevat een URI (Uniform Resouce Identifier), de meest gebruikte standaard voor een URI is de URL (Uniform Resource Locator). Met daarbij een methode specificatie als POST of GET.
 General header bevat de optionele opties, als caching en connectie proprties.
 Request header bestaat uit essentiële data om de request af te handelen. Dit is de grootste collectie van opties die meegegeven worden.
 Entity header gaat over de content van het bericht. Hierin staat informatie als de content-length, en content-type.
 
 --------------
 

2.b Hoe wordt de content in een bericht verpakt?
  De content van een HTTP bericht begint na een witregel na de entity-header:
  Head
  
  Body
  
>>>>>
Studentnaam:

Studentnummer: 

---
# Algemene beschrijving applicatie
HttpProxyServer met caching en headermanipulatie

##  Ontwerp en bouw de *architectuur* van de applicatie die HTTP-requests van een willekeurige PC opvangt en doorstuurt naar één webserver. (Teken een diagram en licht de onderdelen toe)


##  Zorg voor een voorbeeld van een http-request en van een http-response. 
(Kan je globale overeenkomsten vinden tussen een request en een response?)  (Teken een diagram en licht de onderdelen toe)


##  TCP/IP
###  Beschrijving van concept in eigen woorden
###  Code voorbeeld van je eigen code
###  Alternatieven & adviezen
###  Authentieke en gezaghebbende bronnen


##  Bestudeer de RFC van HTTP 1.1.
###  Hoe ziet de globale opbouw van een HTTP bericht er uit? (Teken een diagram en licht de onderdelen toe)
###  Uit welke componenten bestaan een HTTP bericht.  (Teken een diagram en licht de onderdelen toe)
###  Hoe wordt de content in een bericht verpakt? (Teken een diagram en licht de onderdelen toe)
###  Streaming content 

##  Kritische reflectie op eigen werk (optioneel, maar wel voor een 10)
###  Wat kan er beter? Geef aan waarom?
###  Wat zou je een volgende keer anders doen?
###  Hoe zou de opdracht anders moeten zijn om er meer van te leren?

# Test cases

### Case naam
### Case handeling
### Case verwacht gedrag

# Kritische reflectie op eigen beroepsproduct

### Definieer kwaliteit in je architectuur, design, implementatie. 
### Geef voorbeelden.
### Wat kan er beter, waarom? 



<<<<<
