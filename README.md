>>>>>
Studentnaam:
Wijnand Honselaar
Studentnummer: 
533747
---
# Algemene beschrijving applicatie
HttpProxyServer met caching en headermanipulatie

##  Ontwerp en bouw de *architectuur* van de applicatie die HTTP-requests van een willekeurige PC opvangt en doorstuurt naar één webserver. (Teken een diagram en licht de onderdelen toe)


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

Zoals het utivoeren van caching. In de opdracht staat beschreven dat er caching moet zijn en je zelf de timeout in moet kunnen stellen. Op het moment dat een bericht binnenkomt en deze gecached wordt, wordt een datetime aangemaakt en als property bij het gecachde object geplaatst. Deze wordt vervolgs gebruikt om te bepalen of het pakket opnieuw moet worden opgehaald of niet. Het volgt niet de standaarden, maar dit is de simpelste manier om te voldoen aan de requiremt van de opdracht.
###  Wat zou je een volgende keer anders doen?
Eerder naar medestudenten stappen en meer samenwerken. Ik ben alles behalve trots op de geleverde kwaliteit en door eerder met andere studenten samen te werken zou de code kwaliteit (dus meer de standaarden gevolgd bijvoorbeeld) een stuk beter zijn.
###  Hoe zou de opdracht anders moeten zijn om er meer van te leren?

# Kritische reflectie op eigen beroepsproduct

### Definieer kwaliteit in je architectuur, design, implementatie. 

### Geef voorbeelden.
### Wat kan er beter, waarom? 



<<<<<
