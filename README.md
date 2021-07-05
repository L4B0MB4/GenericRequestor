# GenericRequestor

A programm written to change from a certain stucture e.g. json/yaml into a differnt structure with keeping and mapping the data accordingly.

## Example

FROM

```yaml

All: 
    confirmed: number
    recovered: number
    country: string
Baden-Wurttemberg: 
    lat: number
    long: number
    deaths: string
```

TO
```yaml
Alle:
    bestätigt: All.confirmed
    genesen: All.recovered
    land: 
        name: All.country
        bestätigtInObj: All.confirmed
BW:
    lati: Baden-Wurttemberg.lat
    looong: Baden-Wurttemberg.long
    tode: Baden-Wurttemberg.deaths

```

## How to run
Just run the Program.cs for now. There is an example in its code as well altough not polished
