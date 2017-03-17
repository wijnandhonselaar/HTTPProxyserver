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
  
3
  
  
