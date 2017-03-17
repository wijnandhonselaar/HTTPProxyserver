>>>>>
Studentnaam:
Wijnand Honselaar
Studentnummer: 
533747
---
# Algemene beschrijving applicatie
HttpProxyServer met caching en headermanipulatie

##  Ontwerp en bouw de *architectuur* van de applicatie die HTTP-requests van een willekeurige PC opvangt en doorstuurt naar één webserver. (Teken een diagram en licht de onderdelen toe)
![alt-tag](https://github.com/wijnandhonselaar/HTTPProxyserver/blob/develop/ClassDiagram.png?raw=true)

##  Zorg voor een voorbeeld van een http-request en van een http-response. 
(Kan je globale overeenkomsten vinden tussen een request en een response?)  (Teken een diagram en licht de onderdelen toe)
![alt tag](https://raw.githubusercontent.com/wijnandhonselaar/HTTPProxyserver/develop/request.png?token=AB1pxA3GL6ojCdbW2xD4iu7mnZZBrQPqks5Y1PJbwA%3D%3D)
De overeenkomst is de scructuur. Beide beginnen met een regel met basis informatie van het bericht (request-line & status-line), gevolgd door een set header properties, met een witregel en de body (content) van het bericht.

##  TCP/IP
###  Beschrijving van concept in eigen woorden
TCP/IP is een verzamelnaam voor een reeks networkprotocollen die gebruikt worden voor de netwerkcommunicatie tussen computers. Communicatie door middel van TCP/IP vindt plaats op laag 4(Transport) van het OSI model.
###  Code voorbeeld van je eigen code
Hieronder versimplde code van het ontvangen een request over TCP/IP
```C#
_server = new TcpListener(IPAddress.Any, port);
_server.Start();
var client = await _server.AcceptTcpClientAsync();
using (var stream = client.GetStream())
{
  var buffer = new byte[bufferSize];
  await stream.ReadAsync(buffer, 0, bufferSize);
}
```
###  Alternatieven & adviezen

###  Authentieke en gezaghebbende bronnen
https://web.stanford.edu/class/cs101/network-3-internet.html

##  Bestudeer de RFC van HTTP 1.1. Bron: https://www.ietf.org/rfc/rfc2616.txt
###  Hoe ziet de globale opbouw van een HTTP bericht er uit? (Teken een diagram en licht de onderdelen toe)
![alt tag](https://github.com/wijnandhonselaar/HTTPProxyserver/blob/develop/HTTP.png?raw=true)
#### Protocol Parameters
Bevat informatie over het bericht en gebruikte protocol. Bijvoorbeeld de HTTP Versie, DateTime van de het bericht en Media types (Zie alle hoofdstukken 3.x van [RFC van HTTP 1.1](https://www.ietf.org/rfc/rfc2616.txt"Hypertext Transfer Protocol -- HTTP/1.1")
#### HTTP Message
De HTTP Message bevat informatie informatie over het bericht. In de headers worden de protocol parameters meegegeven, met daar nog aan toegevoegd de hoofdstukken onder 4.x [RFC van HTTP 1.1](https://www.ietf.org/rfc/rfc2616.txt"Hypertext Transfer Protocol -- HTTP/1.1")
In het kort een Header, gevolgd door een witregel en de body (oftewel content).

###  Uit welke componenten bestaan een HTTP bericht.  (Teken een diagram en licht de onderdelen toe)
Head
witregel
Body
Zie omschrijving HTTP Message hierboven voor toelichting
###  Hoe wordt de content in een bericht verpakt? (Teken een diagram en licht de onderdelen toe)
De content van een httpbericht wordt verpakt in packages, opgebouwd uit bytes.
###  Streaming content??

##  Kritische reflectie op eigen werk (optioneel, maar wel voor een 10)
###  Wat kan er beter? Geef aan waarom?
Ik heb heel de codebase geschreven om te voldoen aan de requirements van de opdracht. Tijdens het uitvoeren van de opdracht had ik meerdere opdrachten tegelijk, zoals het Project, voorbereiding van presentaties & daarmee heb ik tijdens de twee weken van deze opdracht zeker 50+ uur per week gevuld. Ik heb het gros van de requirements voldaan en daar is mijn code dan ook op toegespitst.

Redirect werkt bijvoorbeeld niet. Waneer je naar Nu.nl gaat en de cookies accepteerd, wordt de post verzonden en zou je redirected moeten worden naar nu.nl. Echter gebeurt deze laatste stap niet.
###  Wat zou je een volgende keer anders doen?
Eerder naar medestudenten stappen en meer samenwerken. Ik ben alles behalve trots op de geleverde kwaliteit en door eerder met andere studenten samen te werken zou de code kwaliteit (dus meer de standaarden gevolgd bijvoorbeeld) een stuk beter zijn.
###  Hoe zou de opdracht anders moeten zijn om er meer van te leren?
Iets leukers dan het zelf schrijven van een proxy in C# met depricated code waardoor je van huidige programming standards af moet wijken. 

# Kritische reflectie op eigen beroepsproduct

### Definieer kwaliteit in je architectuur, design, implementatie. 
Zoals hiervoor beschreven, ben ik niet trots op de geleverde kwaliteit. Ik ben om standaarden heen gegaan en niet aan 100% van de requirements voldaan.

Wel heb ik ervoor gekozen om voor stream using te gebruiken. Hierdoor hoef je je geen zorgen te maken over het sluiten van de stream als je klaar bent.

Gebruik van Tasks tegenover los thread beheer. Gebruik van Task is op dit moment de standaard binnen C#. Thread management is iets wat Microsoft zelf geprobeerd heeft goed op te lossen en wat na meerdere pogingen uit is gekomen op Task. Het wordt ZWAAR afgeraden om zelf met Threads te werken en de management van Threads in eigen handen te nemen. Tasks maken gebruik van een individuele thread of threadpool, afhankelijk van wat er in de task gebeurt. Dit hele proces wordt uit handen genomen door het gebruik van Task tegenover Thread.

Wel heb ik code gescheiden in een aantal services om zo het overzicht nog enigzins te bewaren.
### Geef voorbeelden.
```C#
// Using & stream
using (var resStream = webResponse.GetResponseStream())
{
    if (resStream == null) return null;
    // if ContentType is image, use BinaryReader
    // 
    if (webResponse.ContentType.Contains("image"))
    {
        using (var reader = new BinaryReader(resStream))
            responseBody = reader.ReadBytes((int)webResponse.ContentLength);
    }
    else
    {
        using (var reader = new StreamReader(resStream))
            responseBody = Encoding.UTF8.GetBytes(reader.ReadToEnd());
    }
}

// Task in combinatie met await.
private async Task<byte[]> GetResponse(HttpWebResponse webResponse)
{
  var responseBody = await Task.Run(() => _comService.GetResponseBody(webResponse, head));
  return responseBody;
}
```
### Wat kan er beter, waarom? 
Meerdere punten: 
Door het hele systeem te bouwen op basis van dezelfde classes. Dus alles met TCP, WebRequest of HttpListener. Dit voorkomt een hoop gesjoemel met data-types.

Gebruik maken van een design pattern. Ik heb functies geschreven die meerdere dingen uitvoeren. Des te meer dit voorkomt, des te lastiger het wordt om een goed leesbare codebase te houden.

Gebruik maken van enums om te kijken naar bijvoorbeeld Method van een request.

Door het omzetten van een request naar een Dictionary worden alle values opgehaald op basis van keys. Deze keys zouden kunnen worden omgezet naar een propertiy van een object. Zo voorkom je dat code kan crashen door het maken van een typfout. 

Daarnaast voer ik niet overal null-checks uit op waardes uit de Dictionary. Waardes als User-Agent en Accept zijn altijd aanwezig in een request. Echter moet je hier in de code niet vanuit gaan en ook hier een passende methode gebruiken. Door bijvoorbeeld met TryGetValue te gebruiken in plaats van yourDictionary["key"].

<<<<<
